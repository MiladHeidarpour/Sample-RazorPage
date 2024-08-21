namespace Shop.RazorPage.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;

    public AuthService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:5001");
    }
}