using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class ProductsRepository: IProductsRepository
{
    private readonly string connectionString;
    
    public ProductsRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<ProductsDTO>> GetAllProductsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var products = await db.QueryAsync<ProductsDTO>("SELECT * FROM Products");
        return products;
    }

    public async Task<ProductsDTO> GetProductByIdAsync(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var product = await db.QueryFirstOrDefaultAsync<ProductsDTO>("SELECT * FROM Products WHERE Id = @id", new { id });
        return product;
    }
}