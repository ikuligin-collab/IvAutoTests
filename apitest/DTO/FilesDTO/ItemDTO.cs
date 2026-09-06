using System.Text.Json.Serialization;

namespace apitest.DTO.FilesDTO;

public class ItemDTO(
    [property: JsonPropertyName("productId")]
    int ProductId,
    [property:JsonPropertyName("name")]
    string Name,
    [property:JsonPropertyName("category")]
    string Category,
    [property:JsonPropertyName("quantity")]
    int Quantity,
    [property:JsonPropertyName("price")]
    decimal Price 
);