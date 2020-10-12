using Microsoft.ML.Data;

namespace VideoPlatform.AIL.Models.TripModels
{
    public class TripFarePredictionModel : IModel
    {
        [ColumnName("Score")]
        public float FareAmount;
    }
}