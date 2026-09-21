using apitest.DTO.BookStoreDTO;
using apitest.Interfaces.BookStore;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using apitest.Helpers;

namespace apitest;

public class BookStoreTests
{
    private IBookStore api;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddRefitClient<IBookStore>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://demoqa.com");
            });
        
        var provider = services.BuildServiceProvider();
        api = provider.GetRequiredService<IBookStore>();
    }

    [Test]
    public async Task CreateUser()
    {
        var user = new CreateUserBookStoreDTO{UserName = "Ivan1", Password = "StrongPass123!"};
        var response = await api.CreateUserAsync(user);
        // Айди моего юзера 222696dd-ac7a-4787-ab2c-a0abb54340ec 
    }

    [Test]
    public async Task GettingToken()
    {
        var user = new CreateUserBookStoreDTO() {UserName = "Ivan1", Password = "StrongPass123!"};
        var token = await api.GenerateTokenAsync(user);
        token.Token.Should().NotBeEmpty();
        token.Status.Should().Be("Success");
        token.Result.Should().Contain("authorized");
    }

    [Test]
    public async Task LoginUser()
    {
        var user = new CreateUserBookStoreDTO() {UserName = "Ivan1", Password = "StrongPass123!"};
        var response = await api.LoginUserAsync(user);
        response.UserId.Should().NotBeNullOrEmpty();
        response.UserName.Should().Be(user.UserName);
    }

    [Test]

    public async Task AddBookAsync()
    {
        var token =await GetTokenAsync();
        var userId = await GetUserIdAsync();
        var newBook = new AddBookRequestDTO
        {
            UserId = userId, CollectionOfIsbns = new List<BookDTO> { new BookDTO { isbn = "9781449325862" } }
        };

        var addResonse = await api.AddBookAsync(newBook, token);
        addResonse.Isbn.Should().Be("978144932586");

    }

    [Test]
    public async Task GetAllBooks()
    {
        var response = await api.GetAllBooks();
        response.Should().NotBeNull();
        response.Books.Should().HaveCount(8);
    }


    [Test]
    public async Task GetBookByIsbn()
    {
        var getAllBooks = await api.GetAllBooks();
        var rndBook = RandomHelper.GetRandomItem(getAllBooks.Books);
        var bookById = rndBook.isbn;
        var oneBook = await api.GetBookByIsbnAsync(bookById);
        oneBook.title.Should().Be(rndBook.title);
        
    }
    private async Task<string> GetTokenAsync()
    {
        var user = new CreateUserBookStoreDTO() {UserName = "Ivan1", Password = "StrongPass123!"};
        var getToken = await api.GenerateTokenAsync(user);
        var token = $"Bearer{getToken.Token}";
        return token;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = new CreateUserBookStoreDTO() {UserName = "Ivan1", Password = "StrongPass123!"};
        var response = await api.LoginUserAsync(user);
        var userId = response.UserId;
        return userId;
    }
    
}