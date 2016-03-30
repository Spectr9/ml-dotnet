using numl.Model;
using numl.Supervised;
using numl.Supervised.DecisionTree;
using numl.Supervised.KNN;
using numl.Supervised.NaiveBayes;
using System;
using System.Collections.Generic;

namespace MLLibrariesBenchmark.NumlBenchmark
{
    public class NumlClassificator<T> where T : class
    {
        private readonly List<T> _trainingData;
        private readonly List<T> _testingData;
        private readonly Descriptor _description = Descriptor.Create<T>();
        private readonly Func<T, string> _labelFunction;

        public NumlClassificator(List<T> trainingData, List<T> testingData, Func<T, string> labelFunction)
        {
            _trainingData = trainingData;
            _testingData = testingData;
            _labelFunction = labelFunction;
        }

        public double DecisionTreeTest()
        {
            var generator = new DecisionTreeGenerator();
            var model = generator.Generate(_description, _trainingData);
            return Estimate(model);
        }

        public double KNNTest()
        {
            var generator = new KNNGenerator(2);
            var model = generator.Generate(_description, _trainingData);
            return Estimate(model);
        }

        public double NaiveBayesTest()
        {
            var generator = new NaiveBayesGenerator(2);
            var model = generator.Generate(_description, _trainingData);
            return Estimate(model);
        }

        private double Estimate(IModel model)
        {
            double error = 0;
            foreach (var data in _testingData)
            {
                var trueLabel = _labelFunction(data);
                var predictedLabel = _labelFunction(model.Predict(data));
                if (trueLabel != predictedLabel) error++;
            }
            return error / _testingData.Count;
        }
    }
}
