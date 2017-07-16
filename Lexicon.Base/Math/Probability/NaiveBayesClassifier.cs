using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Lexicon.Base.Attributes;
using Lexicon.Base.Extensions;
using System.Data;
using FastMember;
using Lexicon.Base.Math.Extensions;
using Lexicon.Base.Math.Probability;
using Newtonsoft.Json.Linq;

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

        private class RowAgg
        {
            public string Name { get; }
            public int Count { get; }

            public RowAgg(string name, int count)
            {
                Name = name;
                Count = count;
            }
        }

        private class PropValuePair
        {
            public string Name { get; }
            public dynamic Value { get; }

            public PropValuePair(string name, dynamic value)
            {
                Name = name;
                Value = value;
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
            var rows = table.Select(filter);
            foreach (var row in rows)
            {
                _itemList.Add((dynamic) row[column]);
            }

            return _itemList;
        }
    }



// TODO: Improve the interface here. 
    /// <summary>
    /// Determine Probability of Y given different catagory options  
    /// </summary>
    /// <remarks>
    /// Given P(Y|[X,...Xn]), uses Laplacian Smoothing, to determine the likely hood of a satisfying condition. 
    /// Predict a label Y
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class NaiveBayesClassifier<T>
    {
        //P(male | X) =
        //        [P(education | male) * P(right | male) * P(tall | male) * P(male)] /
        //            [PP(male | X) + PP(female | X)]

        //private decimal _prior;
        private readonly decimal[] _priors;

        private readonly T[] _dataset;
        private readonly Func<T, bool>[] _catagoryDefinitions;
        private readonly Dictionary<string, Func<T, bool>> _binDefinitions;
        private bool _useLaplacianSmoothing;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset">the data set from which to draw likely hoods.</param>
        /// <param name="withLaplacian">uses Laplacian Smoothing</param>
        /// <param name="catagoryDefinitions">definitions used to create your classifer classes</param>
        /// <remarks>
        /// <paramref name="catagoryDefinitions"/> is the dependent attribute definitions 
        /// </remarks>
        public NaiveBayesClassifier(IEnumerable<T> dataset, bool withLaplacian = true,
            params Func<T, bool>[] catagoryDefinitions)
        {
            if (dataset == null || !dataset.Any()) throw new ArgumentException();
            _catagoryDefinitions = catagoryDefinitions ?? throw new ArgumentNullException(nameof(catagoryDefinitions));
            _priors = new decimal[catagoryDefinitions.Length];
            _dataset = dataset as T[] ?? dataset.ToArray();
            _useLaplacianSmoothing = withLaplacian;
        }

        /// <summary>
        /// Convert Numerical Attribute Variable to Catagorical Variable with Decision trees,
        /// </summary>
        /// <remarks>
        /// Why would you want to do this <see cref="http://www.datasciencecentral.com/profiles/blogs/how-to-bin-or-convert-numerical-variables-to-categorical">How to bin.</see>
        /// </remarks>
        public void AddBin(string categoryName, Func<T, bool> catagoryDefintion)
        {
            if (!categoryName.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(categoryName));
            if (catagoryDefintion == null)
                throw new ArgumentNullException(nameof(catagoryDefintion));

            _binDefinitions.Add(categoryName.ToLower(), catagoryDefintion);
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


        // TODO create a single result that will give you the probablility of a single result 
        /// <summary>
        /// This method is used to define the probabilites for each catagory,
        /// </summary>
        /// <param name="attributeConditionDictionary">
        /// Dictionary of string key which is the attribute property, with its defintion to fit into that catagory. 
        /// <see href="https://msdn.microsoft.com/en-us/magazine/jj891056.aspx">Test Run Naive Bayes</see>
        /// </param>
        /// <returns>Decimal probability for each catagoryDefinition provided</returns>
        public decimal[] OverallProbabilities(Dictionary<string, Func<dynamic, bool>> attributeConditionDictionary)
        {
            var dictionary = new Dictionary<int, decimal>();

            decimal[] numerator = new decimal[_catagoryDefinitions.Length];

            for (var i = 0; i < _catagoryDefinitions.Length; i++)
            {
                var temp = _dataset.Select(_catagoryDefinitions[i].Invoke);
                _priors[i] = (decimal) temp.Count() / _dataset.Count(); // P(A) is called prior probability

                var count = (decimal) _dataset.Count(_catagoryDefinitions[i].Invoke) +
                            attributeConditionDictionary.Count;

                attributeConditionDictionary.ForEach(kvp =>
                {
                    var numOfMembersSatisfyingConditionInDataSet =
                        _dataset.Count(x => kvp.Value.Invoke(x.GetType().GetProperty(kvp.Key).GetValue(x))) + 1;
                    var probabilityOf = numOfMembersSatisfyingConditionInDataSet / count;

                    numerator[i] *= probabilityOf;
                });

                numerator[i] *= _priors[i];
            }

            for (var i = 0; i < numerator.Length; i++)
            {
                dictionary.Add(i, numerator[i] / numerator.Sum());
            }

            return dictionary.Values.ToArray();
        }


    }
}

