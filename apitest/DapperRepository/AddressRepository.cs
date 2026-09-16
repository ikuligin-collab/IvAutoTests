using apitest.Interfaces.DapperInterface;
using apitest.DTO.DapperDTO;
using Dapper;
using Microsoft.Data.Sqlite;

namespace apitest.DapperRepository;

public class AddressRpository: IAddressRepository
{
    private readonly string connectionString;
    
    public AddressRpository(string connection)
    {
        connectionString = connection;
    }
    
    public async Task<IEnumerable<AddressDTO>> GetAllAddressesAsync()
    {
        using var db = new SqliteConnection(connectionString);
        var addresses = await db.QueryAsync<AddressDTO>("SELECT * FROM Addresses");
        return addresses;
    }

    public async Task<AddressDTO> GetAddressesByUserIdAsync(int userId)
    {
        using var db = new SqliteConnection(connectionString);
        var address = await db.QueryFirstOrDefaultAsync<AddressDTO>("SELECT * FROM Addresses WHERE UserId = @userId", new { userId });
        return address;
    }
}