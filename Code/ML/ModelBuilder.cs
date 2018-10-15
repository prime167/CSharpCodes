using Microsoft.ML.Legacy;
using Microsoft.ML.Legacy.Data;
using Microsoft.ML.Legacy.Models;
using Microsoft.ML.Legacy.Trainers;
using Microsoft.ML.Legacy.Transforms;

namespace ML
{
    public sealed class ModelBuilder
    {
        private readonly string _trainingDataLocation;

        public ModelBuilder(string trainingDataLocation)
        {
            _trainingDataLocation = trainingDataLocation;
        }

        /// <summary>
        /// Using training data location that is passed trough constructor this method is building
        /// and training machine learning model.
        /// </summary>
        /// <returns>Trained machine learning model.</returns>
        public PredictionModel<IrisFlower, IrisPredict> BuildAndTrain()
        {
            var pipeline = new LearningPipeline
            {
                new TextLoader(_trainingDataLocation).CreateFrom<IrisFlower>(useHeader: true, separator: ','),
                new Dictionarizer("Label"),
                new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"),
                new StochasticDualCoordinateAscentClassifier(),
                new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" }
            };

            return pipeline.Train<IrisFlower, IrisPredict>();
        }

        /// <summary>
        /// Using passed testing data and model, it calculates model's accuracy.
        /// </summary>
        /// <returns>Accuracy of the model.</returns>
        public double Evaluate(PredictionModel<IrisFlower, IrisPredict> model, string testDataLocation)
        {
            var testData = new TextLoader(testDataLocation).CreateFrom<IrisFlower>(useHeader: true, separator: ',');
            var metrics = new ClassificationEvaluator().Evaluate(model, testData);
            return metrics.AccuracyMacro;
        }
    }
}