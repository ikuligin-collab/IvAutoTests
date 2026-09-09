using System.Text.Json.Serialization;

namespace apitest.DTO;

public record CreateUserRequestDTO(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("job")] string Job
);