using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IUserRepository
{
    Task<IEnumerable<UserDTO>> GetAllAsync();
}