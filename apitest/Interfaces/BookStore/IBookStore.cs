using apitest.DTO.BookStoreDTO;
using Refit;

namespace apitest.Interfaces.BookStore;

public interface IBookStore
{
    [Post(path:"/Account/v1/User")]
    Task<CreateUserBookStoreResponseDTO> CreateUserAsync([Body] CreateUserBookStoreDTO userBookStore);
    
    [Post (path: "/Account/v1/GenerateToken")]
    Task<GetTokenUserBookStoreDTO> GenerateTokenAsync([Body] CreateUserBookStoreDTO userBookStore);
    
    [Post (path:"/Account/v1/Login")]
    Task<LoginUserResponseDTO> LoginUserAsync([Body] CreateUserBookStoreDTO userBookStore);
    
    [Post (path:"/BookStore/v1/Books")]
    Task<AddBookResponseDTO> AddBookAsync([Body] AddBookRequestDTO book,
        [Header("Authorization")] string token);

    [Get("/BookStore/v1/Books")]
    Task<BooksListDto> GetAllBooks();
    
    [Get ("/BookStore/v1/Book}")]
    Task<BookDTO> GetBookByIsbnAsync([Query] string ISBN);
}