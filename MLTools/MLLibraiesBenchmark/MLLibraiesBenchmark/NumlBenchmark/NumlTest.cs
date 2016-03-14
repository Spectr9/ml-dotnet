using numl.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using numl.Supervised.Regression;
using numl.Supervised.DecisionTree;
using numl.Supervised.NaiveBayes;
using System.Globalization;
using numl.Supervised;
using BenchmarkDotNet.Attributes;

namespace MLLibraiesBenchmark.NumlBenchmark
{
    public class NumlTest
    {
        Descriptor Description = Descriptor.Create<Iris>();
        private List<Iris> _trainingData;
        private List<Iris> _testingData;
        private List<Iris> _results;
        public double Error { get; private set; }

        public NumlTest()
        {
            var description = Descriptor.Create<Iris>();
            ReloadData();
        }

        public void ReloadData()
        {
            _trainingData = LoadData("irisTraining").ToList();
            _testingData = LoadData("irisTesting").ToList();
        }

        [Benchmark]
        public void DecisionTreeTest()
        {
            var description = Descriptor.Create<Iris>();
            var generator = new DecisionTreeGenerator();
            var model = generator.Generate(description, _trainingData);
            _results = PredictAll(model, _testingData);
        }

        [Benchmark]
        public void KNNTest()
        {
            var description = Descriptor.Create<Iris>();
            var generator = new numl.Supervised.KNN.KNNGenerator();
            var model = generator.Generate(description, _trainingData);
            _results = PredictAll(model, _testingData);
        }

        [Benchmark]
        public void NaiveBayesTest()
        {
            var description = Descriptor.Create<Iris>();
            var generator = new NaiveBayesGenerator(2);
            var model = generator.Generate(description, _trainingData);
            _results = PredictAll(model, _testingData);
        }

        public List<Iris> PredictAll(IModel model, List<Iris> data)
        {
            var errorsCount = 0;
            var result = new List<Iris>();
            foreach (var iris in data)
            {
                var realClass = iris.Class;
                model.Predict(iris);
                result.Add(iris);
                if (realClass != iris.Class) errorsCount++;
            }
            Error = (double)errorsCount / data.Count;
            return result;
        }

        public IEnumerable<Iris> LoadData(string fileName)
        {
            // Getting en-US culture to parse dot-separated decimal
            CultureInfo usCulture = new CultureInfo("en-US");
            NumberFormatInfo dbNumberFormat = usCulture.NumberFormat;

            var dataLines = File.ReadAllLines(fileName).Skip(1);
            foreach (var line in dataLines)
            {
                var data = line.Split(',');

                yield return new Iris
                {
                    SepalLength = decimal.Parse(data[0], dbNumberFormat),
                    SepalWidth = decimal.Parse(data[1], dbNumberFormat),
                    PetalLength = decimal.Parse(data[2], dbNumberFormat),
                    PetalWidth = decimal.Parse(data[3], dbNumberFormat),
                    // To simplify results compare: result strings are in uppercase and without any punctuation.
                    Class = data[4].ToUpper().Replace("-", "")
                };
            }
        }
    }
}
