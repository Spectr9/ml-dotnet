using System;
using System.Collections.Generic;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Regression.Linear;
using BenchmarkDotNet.Attributes;

namespace MLLibraiesBenchmark.AccordBenchmark
{
    public class AccordTest
    {
        private ClassificationData _testingData = new ClassificationData();
        private ClassificationData _trainingData = new ClassificationData();
        private List<double> _results = new List<double>();
        private double _error;
        public double Error => _error;


        public AccordTest()
        {
            _testingData.OpenAndParseFile("irisTesting", true);
            _testingData.ProcessDataset("Iris plant");

            _trainingData.OpenAndParseFile("irisTraining", true);
            _trainingData.ProcessDataset("Iris plant");
        }

        [Benchmark]
        public void DecisionTreeTest()
        {
            List<DecisionVariable> decisionVariables = new List<DecisionVariable>();

            for (int n = 0; n < _trainingData.InputAttributeNumber; ++n)
            {
                decisionVariables.Add(
                    new DecisionVariable("variable_" + (n + 1).ToString(),
                        DecisionVariableKind.Continuous));
            }

            var ClassificationDecisionTree = new DecisionTree(decisionVariables, _trainingData.OutputPossibleValues);

            _error = new C45Learning(ClassificationDecisionTree).Run(_trainingData.InputData, _trainingData.OutputData);

            _results.Clear();
            foreach (double[] input in _testingData.InputData)
            {
                _results.Add(ClassificationDecisionTree.Compute(input));
            }
        }

        [Benchmark]
        public void NaiveBayesTest()
        {
            double classifierError = 0;

            // Create a new Naive Bayes classifier.
            var BayesianModel = new NaiveBayes<NormalDistribution>(
                _trainingData.OutputPossibleValues,
                _trainingData.InputAttributeNumber,
                NormalDistribution.Standard);

            // Compute the Naive Bayes model.
            classifierError = BayesianModel.Estimate(
                _trainingData.InputData,
                _trainingData.OutputData,
                true,
                new NormalOptions { Regularization = 1e-5 /* To avoid zero variances. */ });

            _results.Clear();
            foreach (double[] input in _testingData.InputData)
            {
                _results.Add(BayesianModel.Compute(input));
            }
        }

        [Benchmark]
        public void KNNTest()
        {

            var KNN = new KNearestNeighbors(k: 2, classes:3, inputs: _trainingData.InputData, outputs: _trainingData.OutputData);

            double errorsCount = 0;
            _results.Clear();
            for (int i = 0; i < _testingData.InputData.Length; i++)
            {
                var result = KNN.Compute(_testingData.InputData[i]);
                _results.Add(result);
                if (result != _testingData.OutputData[i]) errorsCount++;
            }
            _error = errorsCount / _testingData.InputData.Length;
        }
    }

}
