using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using CypherNet.Configuration;
using CypherNet.Graph;
using Lexicon.Base.Attributes;
using Lexicon.Base.Math.Probability;
using NUnit.Framework;

namespace Lexicon.WebAPI.Server
{
    public class DemoClass
    {
        public string Occupation { get; set; }
        public string HandDominance { get; set; }
        [Bin("tall", "short", "medium")]
        public double Height { get; set; }
        public string Sex { get; set; }
    }

    class Server
    {
        static void Main(string[] args)
        {
            var demo = new List<DemoClass>()
            {
                new DemoClass()
                {
                    Occupation = "nurse",
                    HandDominance = "right",
                    Height = 71.6,
                    Sex = "female"
                },
                new DemoClass()
                {
                    Occupation = "doctor",
                    HandDominance = "right",
                    Height = 68.2,
                    Sex = "male"
                }
            };

            var navieBayesDemoClassification = new NaiveBayesClassifier<DemoClass>(demo, true ,(dc)=> dc.Sex == "male",(dc) => dc.Sex =="female");
            var attributeToUse = new Dictionary<string, Func<dynamic, bool>>();
            attributeToUse.Add("Occupation", o => o.Occupation == "nurse");
            attributeToUse.Add("HandDominance", o => o.HandDominance == "right");
            attributeToUse.Add("Height", o => o.Height == "");
            navieBayesDemoClassification.AddBin("Tall", @class =>  @class.Height > 71.0);
            navieBayesDemoClassification.AddBin("Short", @class => @class.Height < 64.0);
            navieBayesDemoClassification.AddBin("Medium", @class => @class.Height > 64.0 && @class.Height < 71.0);

            //t.OverallProbabilities()
            //var clientFactory = Fluently.Configure("http://192.168.99.100:32769/db/data").CreateSessionFactory();
            //var cypherEndpoint = clientFactory.Create();

            //Node node1, node2;

            //using (var trans1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromDays(1)))
            //{
            //    node1 = cypherEndpoint.CreateNode(new { name = "test node1" });
            //    using (var trans2 = new TransactionScope(TransactionScopeOption.RequiresNew))
            //    {
            //        node2 = cypherEndpoint.CreateNode(new { name = "test node2" });
            //        trans2.Complete();
            //    }
            //}

            //var node1Query = cypherEndpoint.BeginQuery(s => new { node1 = s.Node })
            //                   .Start(ctx => ctx.StartAtId(ctx.Vars.node1, node1.Id))
            //                   .Return(ctx => new { ctx.Vars.node1 })
            //                   .Fetch()
            //                   .FirstOrDefault();

            //var node2Query = cypherEndpoint.BeginQuery(s => new { node2 = s.Node })
            //                               .Start(ctx => ctx.StartAtId(ctx.Vars.node2, node2.Id))
            //                               .Return(ctx => new { ctx.Vars.node2 })
            //                               .Fetch()
            //                               .FirstOrDefault();

            //Assert.IsNull(node1Query);
            //Assert.IsNotNull(node2Query);

        }
    }
}
