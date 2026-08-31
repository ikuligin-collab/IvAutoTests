using System.Text.Json.Serialization;

namespace apitest.DTO;

public class CreateUserRequestDTO
{
    public string Name {get;init;}
    public string Job {get;init;}
    
}