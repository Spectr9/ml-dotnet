using BenchmarkDotNet.Attributes;
using System.Linq;
using MLLibrariesBenchmark.AccordBenchmark;
using MLLibrariesBenchmark.NumlBenchmark;

namespace MLLibrariesBenchmark
{
    public class IrisBenchmark
    {
        private AccordClassificator _accordClassificator;
        private NumlClassificator<Iris> _numlClassificator;

        [Setup]
        public void Init()
        {
            var testingData = new ClassificationData();
            var trainingData = new ClassificationData();
            testingData.OpenAndParseFile("irisTesting", true);
            testingData.ProcessDataset("Iris plant");
            trainingData.OpenAndParseFile("irisTraining", true);
            trainingData.ProcessDataset("Iris plant");

            _accordClassificator = new AccordClassificator(trainingData, testingData);
            _numlClassificator = new NumlClassificator<Iris>(
                Iris.LoadData("irisTraining").ToList(),
                Iris.LoadData("irisTesting").ToList(),
                model => model.Class
            );
        }

        [Benchmark]
        public double Numl_NaiveBayes()
        {
            return _numlClassificator.NaiveBayesTest();
        }

        [Benchmark]
        public double Numl_KNN()
        {
            return _numlClassificator.KNNTest();
        }

        [Benchmark]
        public double Numl_DecisionTree()
        {
            return _numlClassificator.DecisionTreeTest();
        }

        [Benchmark]
        public double Accord_NaiveBayes()
        {
            return _accordClassificator.NaiveBayesTest();
        }

        [Benchmark]
        public double Accord_KNN()
        {
            return _accordClassificator.KNNTest();
        }

        [Benchmark]
        public double Accord_DecisionTree()
        {
            return _accordClassificator.DecisionTreeTest();
        }
    }
}
