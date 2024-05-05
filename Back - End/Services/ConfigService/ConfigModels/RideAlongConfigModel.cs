using System.Text.Json.Serialization;

namespace Team3.ThePollProject.ConfigService.ConfigModels;

public sealed class RideAlongConfigModel
{
    [JsonPropertyName("ConnectionStrings")]
    public required ConnectionStrings CONNECTION_STRINGS { get; set; }

}