using System.Text.Json.Serialization;

namespace apitest.DTO.FilesDTO;

public class DeliverytDTO(
    [property: JsonPropertyName("type")]
    string Type,
    [property:JsonPropertyName("status")]
    string Status,
    [property:JsonPropertyName("estimatedDate")]
    string EstimatedDate,
    [property:JsonPropertyName("trackingNumber")]
    string TrackingNumber
);