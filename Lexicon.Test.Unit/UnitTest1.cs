using System;
using System.Collections.Generic;
using System.Linq;
using Lexicon.Base.Attributes;
using Lexicon.Base.Math.Probability;
using NUnit.Framework;

namespace Lexicon.Test.Unit
{
    public enum Sex
    {
        Male,
        Female
    }


    public class Person 
    {
        public string Job { get; set; }
        public string Dominance { get; set; }
        [Bin(binNames: new[] { "short", "medium", "tall"})]
        public double Height { get; set; }
        public Sex Sex { get; set; }
    }

    public class NaiveBayesClassifierTestSuite
    {
        private List<Person> People;

        [SetUp]
        public void CreateDataSet()
        {
            People = new List<Person>();
            People.Add(new Person() {Job = "Developer", Dominance = "Right", Height = 56.5, Sex = Sex.Male});
            People.Add(new Person() { Job = "Developer", Dominance = "Right", Height = 65.3, Sex = Sex.Male });
            People.Add(new Person() { Job = "Teacher", Dominance = "Right", Height = 56.5, Sex = Sex.Female });
            People.Add(new Person() { Job = "Teacher", Dominance = "Left", Height = 50.23, Sex = Sex.Female });
            People.Add(new Person() { Job = "Cop", Dominance = "Right", Height = 60.2, Sex = Sex.Male });
            People.Add(new Person() { Job = "Nurse", Dominance = "Left", Height = 58.23, Sex = Sex.Female });

        }

        [Test]
        public void OveralProbabilities()
        {
            //var classifierInstance = new NaiveBayesClassifier<Person>(People, true, catagoryDefinitions:);
        }
    }
}
