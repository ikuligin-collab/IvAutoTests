using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IOrderItemsRepository
{
    Task<IEnumerable<OrderItemsDTO>> GetAllOrderItemsAsync();
    Task<OrderItemsDTO> GetOrderItemsById(int id);
}