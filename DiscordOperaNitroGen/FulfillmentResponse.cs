using System.Text.Json.Serialization;

namespace DiscordOperaNitroGen;

public class FulfillmentResponse
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
}