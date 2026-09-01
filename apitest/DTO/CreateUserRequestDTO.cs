using System.Text.Json.Serialization;

namespace apitest.DTO;

public record CreateUserRequestDTO(string Name, string Job);