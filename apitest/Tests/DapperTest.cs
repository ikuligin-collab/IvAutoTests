using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest;

public class DapperTest
{
    //[Test]
    public async Task Initialize()
    {
        var connectionString = "Data Source=marketplace.db";
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();
        await DatabaseInitializer.InitializeAsync(connection);
        
    }
}