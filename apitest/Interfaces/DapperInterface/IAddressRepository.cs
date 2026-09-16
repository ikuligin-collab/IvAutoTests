using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IAddressRepository
{
        Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
        Task<AddressDTO> GetAddressesByUserIdAsync(int userId); 
}