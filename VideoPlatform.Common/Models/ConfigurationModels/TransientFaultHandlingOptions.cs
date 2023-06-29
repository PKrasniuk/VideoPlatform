using System;

namespace VideoPlatform.Common.Models.ConfigurationModels;

public class TransientFaultHandlingOptions
{
    public bool Enabled { get; set; }

    public TimeSpan AutoRetryDelay { get; set; }
}