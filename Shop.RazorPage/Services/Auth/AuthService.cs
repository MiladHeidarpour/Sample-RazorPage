using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Models.Response.Auth;
using System.Net;

namespace Shop.RazorPage.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthService(HttpClient client, IHttpContextAccessor contextAccessor)
    {
        _client = client;
        _contextAccessor = contextAccessor;
    }

    public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
    {
        var result = await _client.PostAsJsonAsync("Auth/Login", command);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    }

    public async Task<ApiResult?> Register(RegisterCommand command)
    {
        var result = await _client.PostAsJsonAsync("Auth/Register", command);
        //if (result.StatusCode != HttpStatusCode.OK)
        //{
        //    return new ApiResult() { IsSuccess = false };
        //}
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = _contextAccessor.HttpContext.Request.Cookies["refreshToken"];
        var result = await _client.PostAsync($"Auth/RefreshToken?refreshToken={refreshToken}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
    }
    public async Task<ApiResult?> Logout()
    {
        try
        {
            var result = await _client.DeleteAsync("Auth/Logout");
            return await result.Content.ReadFromJsonAsync<ApiResult>();
        }
        catch (Exception e)
        {
            return ApiResult.Error();
        }
    }

}