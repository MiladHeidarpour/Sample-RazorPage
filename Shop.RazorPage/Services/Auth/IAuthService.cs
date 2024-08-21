using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Models.Response.Auth;

namespace Shop.RazorPage.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>?> Login(LoginCommand command);
    Task<ApiResult?> Register(RegisterCommand command);
    Task<ApiResult<LoginResponse>?> RefreshToken();
    Task<ApiResult?> Logout();
}
