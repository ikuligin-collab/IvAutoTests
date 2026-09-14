using apitest.DTO.DapperDTO;

namespace apitest.Interfaces.DapperInterface;

public interface IReviewsRepository
{
    Task<IEnumerable<ReviewsDTO>> GetAllReviewsAsync();
    Task<ReviewsDTO> GetReviewItemsById(int id);
}