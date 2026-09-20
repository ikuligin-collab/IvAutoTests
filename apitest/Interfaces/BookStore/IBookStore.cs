using apitest.DTO.BookStoreDTO;
using Refit;

namespace apitest.Interfaces.BookStore;

public interface IBookStore
{
    [Post(path:"/Account/v1/User")]
    Task<CreateUserBookStoreResponseDTO> CreateUserAsync([Body] CreateUserBookStoreDTO userBookStore);
    
    [Post (path: "/Account/v1/GenerateToken")]
    Task<GetTokenUserBookStoreDTO> GenerateTokenAsync([Body] CreateUserBookStoreDTO userBookStore);
}