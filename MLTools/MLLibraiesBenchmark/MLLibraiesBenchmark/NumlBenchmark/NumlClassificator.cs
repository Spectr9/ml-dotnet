using numl.Model;
using numl.Supervised;
using numl.Supervised.DecisionTree;
using numl.Supervised.KNN;
using numl.Supervised.NaiveBayes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLLibraiesBenchmark.NumlBenchmark
{
    public class NumlClassificator<T> where T : class
    {
        private List<T> _trainingData;
        private List<T> _testingData;
        private Descriptor description = Descriptor.Create<T>();
        private Func<List<T>, IModel, double> _estimateFunction;

        public NumlClassificator(List<T> trainingData, List<T> testingData, Func<List<T>, IModel, double> estimateFunction)
        {
            _trainingData = trainingData;
            _testingData = testingData;
            _estimateFunction = estimateFunction;
        }

        public double DecisionTreeTest()
        {
            var description = Descriptor.Create<T>();
            var generator = new DecisionTreeGenerator();
            var model = generator.Generate(description, _trainingData);
            return _estimateFunction(_testingData, model);
        }

        public double KNNTest()
        {
            var description = Descriptor.Create<T>();
            var generator = new KNNGenerator(2);
            var model = generator.Generate(description, _trainingData);
            return _estimateFunction(_testingData, model);
        }

        public double NaiveBayesTest()
        {
            var description = Descriptor.Create<T>();
            var generator = new NaiveBayesGenerator(2);
            var model = generator.Generate(description, _trainingData);
            return _estimateFunction(_testingData, model);
        }
    }
}
