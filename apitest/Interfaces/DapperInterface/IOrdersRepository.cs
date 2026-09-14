using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IOrdersRepository
{
    Task<IEnumerable<OrdersDTO>> GetAllOrdersAsync();
    Task<OrdersDTO> GetOrderByUserIdAsync(int userId);
}