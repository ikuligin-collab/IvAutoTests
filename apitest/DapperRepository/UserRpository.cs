using System.Data;
using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class UserRpository: IUserRepository
{
    private readonly string connectionString;
    
    public UserRpository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var users = await db.QueryAsync<UserDTO>("SELECT * FROM Users");
        return users;
    }
}