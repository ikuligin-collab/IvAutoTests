using System.Text.Json.Serialization;

namespace apitest.DTO.FilesDTO;

public record PaymentDTO(
    [property: JsonPropertyName("method")]
    string Method,
    [property:JsonPropertyName("status")]
    string Status,
    [property:JsonPropertyName("transactionId")]
    string TransactionId
);