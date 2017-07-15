using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using FastMember;
using NUnit.Framework;
using Lexicon.Base.Attributes;
using Lexicon.Base.Math.Probability;

namespace Lexicon.Test.Unit
{
    public enum Sex
    {
        Male,
        Female
    }


    public class Person : IClassifiable
    {
        public string Job { get; set; }
        public string Dominance { get; set; }
        //[Bin(binNames: new[] { "short", "medium", "tall"})]
        public double Height { get; set; }
        //public Sex Sex { get; set; }
    }

    public class NaiveBayesClassifierTestSuite
    {
        private List<Person> People;

        [SetUp]
        public void CreateDataSet()
        {
            People = new List<Person>
            {
                new Person() {Job = "Developer", Dominance = "Right", Height = 56.5},
                new Person() {Job = "Developer", Dominance = "Right", Height = 65.3},
                new Person() {Job = "Teacher", Dominance = "Right", Height = 56.5},
                new Person() {Job = "Teacher", Dominance = "Left", Height = 50.23},
                new Person() {Job = "Cop", Dominance = "Right", Height = 60.2},
                new Person() {Job = "Nurse", Dominance = "Left", Height = 58.23}
            };

        }

        [Test]
        public void OveralProbabilities()
        {
            var table = new DataTable();

            using (var reader = ObjectReader.Create(People))
            {
                table.Load(reader);
            }

            var classifier = new Classifier<Person>(People);
            classifier.TrainClassifier();
            classifier.Classify(new Person { Job = "Cop", Dominance = "Left", Height =  52.6});
            //var classifierInstance = new NaiveBayesClassifier<Person>(People, true, catagoryDefinitions:);
        }
    }


}
