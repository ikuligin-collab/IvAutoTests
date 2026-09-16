using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class OrderItemsRepository: IOrderItemsRepository
{
    private readonly string connectionString;
    
    public OrderItemsRepository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<OrderItemsDTO>> GetAllOrderItemsAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var orderItems = await db.QueryAsync<OrderItemsDTO>("SELECT * FROM OrderItems");
        return orderItems;
    }

    public async Task<OrderItemsDTO> GetOrderItemById(int id)
    {
        using var db = new SqliteConnection(connectionString);
        var orderItem = await db.QueryFirstOrDefaultAsync<OrderItemsDTO>("SELECT * FROM OrderItems WHERE Id = @id", new { id });
        return orderItem;
    }
}