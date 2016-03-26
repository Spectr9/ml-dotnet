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
    public class AccordClassificator
    {
        private ClassificationData _testingData = new ClassificationData();
        private ClassificationData _trainingData = new ClassificationData();

        public AccordClassificator(ClassificationData trainingData, ClassificationData testingData)
        {
            _trainingData = trainingData;
            _testingData = testingData;
        }

        public double DecisionTreeTest()
        {
            List<DecisionVariable> decisionVariables = new List<DecisionVariable>();

            for (int n = 0; n < _trainingData.InputAttributeNumber; ++n)
            {
                decisionVariables.Add(
                    new DecisionVariable("variable_" + (n + 1).ToString(),
                        DecisionVariableKind.Continuous));
            }

            var ClassificationDecisionTree = new DecisionTree(decisionVariables, _trainingData.OutputPossibleValues);
            var classifierError = new C45Learning(ClassificationDecisionTree).Run(_trainingData.InputData, _trainingData.OutputData);

            var testingDataCount = _testingData.InputData.Length;
            double error = 0;
            for (int i = 0; i < testingDataCount; i++)
            {
                var input = _testingData.InputData[i];
                var result = ClassificationDecisionTree.Compute(input);
                if (result != _testingData.OutputData[i]) error++;
            }
            return error / testingDataCount;
        }

        public double NaiveBayesTest()
        {
            // Create a new Naive Bayes classifier.
            var BayesianModel = new NaiveBayes<NormalDistribution>(
                _trainingData.OutputPossibleValues,
                _trainingData.InputAttributeNumber,
                NormalDistribution.Standard);

            BayesianModel.Estimate(
                _trainingData.InputData,
                _trainingData.OutputData,
                true,
                new NormalOptions { Regularization = 1e-5 /* To avoid zero variances. */ });

            return BayesianModel.Error(_testingData.InputData, _testingData.OutputData);
        }

        public double KNNTest()
        {
            var KNN = new KNearestNeighbors(k: 2, classes: 3, inputs: _trainingData.InputData, outputs: _trainingData.OutputData);

            var testingDataCount = _testingData.InputData.Length;
            double error = 0;
            for (int i = 0; i < testingDataCount; i++)
            {
                var input = _testingData.InputData[i];
                var result = KNN.Compute(input);
                if (result != _testingData.OutputData[i]) error++;
            }
            return error / testingDataCount;
        }
    }
}
