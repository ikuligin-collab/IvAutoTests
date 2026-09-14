using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IProductsRepository
{
    Task<IEnumerable<ProductsDTO>> GetAllProductsAsync();
    Task<ProductsDTO> GetProductByIdAsync(int id);
}