using Microsoft.ML.Data;

namespace VideoPlatform.AIL.Models.TripModels;

public class TripModel : IModel
{
    [LoadColumn(6)] public float FareAmount;

    [LoadColumn(2)] public float PassengerCount;

    [LoadColumn(5)] public string PaymentType;

    [LoadColumn(1)] public string RateCode;

    [LoadColumn(4)] public float TripDistance;

    [LoadColumn(3)] public float TripTime;

    [LoadColumn(0)] public string VendorId;
}