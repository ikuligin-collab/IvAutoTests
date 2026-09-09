using System.Text.Json.Serialization;
using apitest.DTO.FilesDTO;

namespace apitest.DTO.UsersDTO;
public record ProfileDTO(
    [property: JsonPropertyName("fullName")] string FullName,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("address")] AddressDTO Address,
    [property: JsonPropertyName("tags")] List<string> Tags
);