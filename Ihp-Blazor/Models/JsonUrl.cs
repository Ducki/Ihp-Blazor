using System.Text.Json.Serialization;

namespace Ihp_Blazor.Models;

public record JsonUrl
{
    [JsonPropertyName("url")] public string Url { get; init; } = null!;
}
