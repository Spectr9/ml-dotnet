using BenchmarkDotNet.Attributes;
using MLLibraiesBenchmark.AccordBenchmark;
using MLLibraiesBenchmark.NumlBenchmark;
using System.Linq;

namespace MLLibraiesBenchmark
{
    public class SkinBenchmark
    {

        public AccordClassificator _accordClassificator;
        public NumlClassificator<Skin> _numlClassificator;

        [Setup]
        public void Init()
        {
            var testingData = new ClassificationData();
            var trainingData = new ClassificationData();
            testingData.OpenAndParseFile("skinTesting", true);
            testingData.ProcessDataset("skin");
            trainingData.OpenAndParseFile("skinTraining", true);
            trainingData.ProcessDataset("skin");

            _accordClassificator = new AccordClassificator(trainingData, testingData);
            _numlClassificator = new NumlClassificator<Skin>(
                Skin.LoadData("skinTraining").ToList(),
                Skin.LoadData("skinTesting").ToList(),
                (list, model) =>
                {
                    double error = 0;
                    foreach (var skin in list)
                    {
                        var trueClass = skin.Label;
                        var predictedClass = model.Predict(skin).Label;
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
