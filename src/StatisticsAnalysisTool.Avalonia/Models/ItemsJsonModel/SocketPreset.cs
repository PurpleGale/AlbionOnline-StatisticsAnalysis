﻿using System.Text.Json.Serialization;

namespace StatisticsAnalysisTool.Avalonia.Models.ItemsJsonModel;

public class SocketPreset
{
    [JsonPropertyName("@name")]
    public string Name { get; set; } = string.Empty;
}