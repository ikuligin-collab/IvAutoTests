using System.Text.Json.Serialization;

namespace apitest.DTO.UsersDTO;

public record UserDataDTO(
    [property: JsonPropertyName("data")] List<UserDTO> UserDto
);