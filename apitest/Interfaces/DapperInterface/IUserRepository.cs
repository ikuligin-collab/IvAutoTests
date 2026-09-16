using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
    Task<UserDTO> GetByIdAsync(int id);
    Task<UserDTO> GetUserByFirstAndLastName(string firstName, string lastName);
}