using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface ICategoriesRepository
{
    Task<IEnumerable<CategoriesDTO>> GetAllCategoriesAsync();
    Task<CategoriesDTO> GetCategoriesByIdAsync(int id);
}