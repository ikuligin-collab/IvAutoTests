using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrdersRepository: IOrdersRepository
{
    private readonly string connectionString;
    
    public OrdersRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<OrdersDTO>> GetAllOrdersAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var orders = await db.QueryAsync<OrdersDTO>("SELECT * FROM Orders");
        return orders;
    }

    public async Task<OrdersDTO> GetOrderByUserIdAsync(int userId)
    {
        using var db = new SqliteConnection(connectionString);
        var order = await db.QueryFirstOrDefaultAsync<OrdersDTO>(
            "SELECT * FROM Orders WHERE UserId = @User_id", 
            new { User_id = userId }
        );
        return order;
    }
}