using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class ReviewsRepository: IReviewsRepository
{
    private readonly string connectionString;
    
    public ReviewsRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<ReviewsDTO>> GetAllReviewsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var reviews = await db.QueryAsync<ReviewsDTO>("SELECT * FROM Reviews");
        return reviews;
    }

    public async Task<ReviewsDTO> GetReviewItemsById(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var review = await db.QueryFirstOrDefaultAsync<ReviewsDTO>("SELECT * FROM Reviews WHERE Id = @id", new { id });
        return review;
    }
}