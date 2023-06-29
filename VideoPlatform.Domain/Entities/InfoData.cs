using System;
using Newtonsoft.Json;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities;

public class InfoData : BaseEntity<Guid>
{
    [JsonProperty("id")] public override Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Value { get; set; }

    public InfoType Type { get; set; }
}