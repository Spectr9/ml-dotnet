using BenchmarkDotNet.Attributes;
using MLLibraiesBenchmark.AccordBenchmark;
using MLLibraiesBenchmark.NumlBenchmark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLLibraiesBenchmark
{
    public class IrisBenchmark
    {
        public AccordClassificator _accordClassificator;
        public NumlClassificator<Iris> _numlClassificator;

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
                (list, model) =>
                {
                    double error = 0;
                    foreach (var iris in list)
                    {
                        var trueClass = iris.Class;
                        var predictedClass = model.Predict(iris).Class;
                        if (trueClass != predictedClass) error++;
                    }
                    return error / list.Count;
                }
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
