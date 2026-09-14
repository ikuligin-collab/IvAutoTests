using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class CategoriesRpository: ICategoriesRepository
{
    private readonly string connectionString;
    
    public CategoriesRpository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<CategoriesDTO>> GetAllCategoriesAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var categories = await db.QueryAsync<CategoriesDTO>("SELECT * FROM Categories");
        return categories;
    }

    public async Task<CategoriesDTO> GetCategoriesByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var address = await db.QueryFirstOrDefaultAsync<CategoriesDTO>("SELECT * FROM Categories WHERE Id = @id", new { id });
        return address;
    }
}