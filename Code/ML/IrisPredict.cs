using Microsoft.ML.Runtime.Api;

namespace ML
{
    public class IrisPredict
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}