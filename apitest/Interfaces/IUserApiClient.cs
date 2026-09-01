using apitest.DTO;
using Refit;

namespace apitest.Interfaces;
[Headers ("x-api-key", "free_user_3IMkASnOWZzNwN8EVQkJPchdUZQ")]
public interface IUserApiClient
{
    [Get ("/users/{id}")]
    Task<UserResponseDTO> GetUserAsync(int id);
}