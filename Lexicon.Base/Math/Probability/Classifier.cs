using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FastMember;
using Lexicon.Base.Attributes;
using Lexicon.Base.Extensions;
using Lexicon.Base.Math.Extensions;

namespace Lexicon.Base.Math.Probability
{
    public class Classifier<T> where T : IClassifiable, new()
    {
        private readonly decimal[] _priors;
        private readonly IEnumerable<T> _source = new List<T>();
        private readonly Dictionary<string, Func<T, bool>> _binDefinitions;
        private bool _useLaplacianSmoothing;

        public DataSet DataSet { get; private set; } = new DataSet();

        public Classifier(IEnumerable<T> sourceData, bool withLaplacian = true)
        {
            if (sourceData == null || !sourceData.Any()) throw new ArgumentException();
            _priors = new decimal[typeof(T).GetProperties().Length];
            _source = sourceData as T[] ?? sourceData.ToArray();
            _useLaplacianSmoothing = withLaplacian;
        }

        public Classifier()
        {
        }

        public void TrainClassifier()
        {
            var gaussianDistribution = DataSet.Tables.Add("Gaussian");

            gaussianDistribution.Columns.Add("Propery Name");
            gaussianDistribution.Columns.Add("Mean");
            gaussianDistribution.Columns.Add("Variance");

            var dataSetAsTable = new DataTable();
            using (var reader = ObjectReader.Create(_source))
            {
                dataSetAsTable.Load(reader);
            }

            // calc data 
            var results = (from myRow in dataSetAsTable.Rows.Cast<DataRow>()
                group myRow by myRow.Field<string>(dataSetAsTable.Columns[0].ColumnName)
                into g
                select new {Name = g.Key, Count = g.Count()}).ToList();

            foreach (var @group in results)
            {
                var row = gaussianDistribution.Rows.Add();
                row[0] = @group.Name;

                var a = 1;
                for (var i = 1; i < dataSetAsTable.Columns.Count; i++)
                {
                    row[a] = SelectRows(dataSetAsTable, i,
                        $"{dataSetAsTable.Columns[0].ColumnName} = '{@group.Name}'").Mean();
                    row[++a] = SelectRows(dataSetAsTable, i,
                        $"{dataSetAsTable.Columns[0].ColumnName} = '{@group.Name}'").Variance();
                    a++;
                }
            }


        }

        public string Classify(T obj)
        {
            var score = new Dictionary<string, double>();
            var setAsTable = _source.AsTable();

            var results = (from myRow in setAsTable.AsEnumerable()
                group myRow by myRow.Field<string>(setAsTable.Columns[0].ColumnName)
                into g
                select new {Name = g.Key, Count = g.Count()}).ToList();

            var propArray =
                obj.GetType()
                    .GetProperties()
                    .Select(x => new {x.Name, Value = x.GetValue(obj)})
                    .ToList();

            var withCount =
            (from f in propArray
                join r in results
                on f.Name equals r.Name
                into joinResult
                from r in joinResult.DefaultIfEmpty()
                select new
                {
                    r.Name,
                    f.Value,
                    r.Count
                }).ToArray();


            for (int i = 0; i < results.Count; i++)
            {
                var subScoreList = new List<double>();
                int a = 1, b = 1;
                for (int k = 1; k < DataSet.Tables["Gaussian"].Columns.Count; k = k + 2)
                {
                    var mean = Convert.ToDouble(DataSet.Tables["Gaussian"].Rows[i][a]);
                    var variance = Convert.ToDouble(DataSet.Tables["Gaussian"].Rows[i][++a]);
                    // you are getting the 
                    var result =
                        EnumerableAggregationExtensions.NormalDist(withCount[b - 1].Count, mean, variance.SquareRoot());
                    subScoreList.Add(result);
                    a++;
                    b++;
                }

                double finalScore = 0;
                for (int z = 0; z < subScoreList.Count; z++)
                {
                    if (finalScore == 0)
                    {
                        finalScore = subScoreList[z];
                        continue;
                    }

                    finalScore = finalScore * subScoreList[z];
                }

                score.Add(results[i].Name, finalScore * 0.5);
            }

            double maxOne = score.Max(c => c.Value);
            var name = (from c in score
                where c.Value == maxOne
                select c.Key).First();

            return name;
        }

        /// TODO: I would like to know what properties are being catagorized? 
        public IEnumerable<BinData> GetBins()
        {
            // Steps - you'd have to go through the assembly or language,
            // get the items and the properties decoorated with the bin attribute. 

            // If using neo4j you're going to return a json object 
            // that json will need to be convertable to a c# type to which you can apply your attribute
            // this won't be known at compile time you're not able to make a c# class for every word 
            // you have to depend on the data store to return this object and create a generic type.. 

            return
                typeof(T)
                    .GetAttributeValues((BinAttribute bin) =>
                        new BinData()
                        {
                            PropertyName = bin.PropertyName,
                            BinCatagories = bin.BinCatagories
                        });
        }

        private IEnumerable<dynamic> SelectRows(DataTable table, int column, string filter)
        {
            var _itemList = new List<dynamic>();
            var filterColumn = filter.Split('=')[0].Replace("'","").Trim();
            var filterValue = filter.Split('=')[1].Replace("'", "").Trim();
            var rows = table.Rows.Cast<DataRow>().ToList().Where(r => string.Equals(r.Field<string>(filterColumn),filterValue,StringComparison.InvariantCultureIgnoreCase)) ;
            var enumerable = table.Rows.Cast<DataRow>().ToList().Select(r => r.Field<string>(filterColumn)).First(x => x==filterValue);
            foreach (var row in rows)
            {
                _itemList.Add((dynamic)row[column]);
            }

            return _itemList;
        }
    }
}