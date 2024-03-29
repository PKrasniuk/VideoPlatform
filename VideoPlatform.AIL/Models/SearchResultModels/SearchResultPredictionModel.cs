﻿namespace VideoPlatform.AIL.Models.SearchResultModels;

public class SearchResultPredictionModel
{
    public uint GroupId { get; set; }

    public uint Label { get; set; }

    // Prediction made by the model that is used to indicate
    // the relative ranking of the candidate search results.
    public float Score { get; set; }

    // Values that are influential in determining the relevance of a data instance.
    // This is a vector that contains concatenated columns from the underlying dataSet.
    public float[] Features { get; set; }
}