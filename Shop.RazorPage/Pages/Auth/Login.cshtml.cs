using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Infrastructure.CookieUtils;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Models.Command.Orders;
using Shop.RazorPage.Services.Auth;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages.Auth;
[BindProperties]
[ValidateAntiForgeryToken]
public class LoginModel : PageModel
{
    private readonly IAuthService _authService;
    private readonly ShopCardCookieManger _shopCardCookieManger;
    private readonly IOrderService _orderService;
    public LoginModel(IAuthService authService, ShopCardCookieManger shopCardCookieManger, IOrderService orderService)
    {
        _authService = authService;
        _shopCardCookieManger = shopCardCookieManger;
        _orderService = orderService;
    }


    [DisplayName("شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string RedirectTo { get; set; }


    public IActionResult OnGet(string redirectTo)
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect("/");
        }

        RedirectTo = redirectTo;
        return Page();
    }

    public async Task<IActionResult> OnPost(string redirectTo)
    {
        var result = await _authService.Login(new LoginCommand()
        {
            Password = Password,
            PhoneNumber = PhoneNumber,
        });
        if (result.IsSuccess == false)
        {
            ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
            return Page();
        }

        var token = result.Data.Token;
        var refreshToken = result.Data.RefreshToken;
        HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
        });
        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(10),
        });

        await SyncShopCardCookie(token);

        if (!string.IsNullOrWhiteSpace(RedirectTo))
        {
            return LocalRedirect(RedirectTo);
        }
        return Redirect("/");
    }

    private async Task SyncShopCardCookie(string token)
    {
        var shopCard = _shopCardCookieManger.GetShopCard();
        if (shopCard == null || shopCard.Items.Any() == false)
        {
            return;
        }

        HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        foreach (var item in shopCard.Items)
        {
            await _orderService.AddOrderItem(new AddOrderItemCommand()
            {
                Count = item.Count,
                InventoryId = item.InventoryId,
                UserId = userId,
            });
        }
        _shopCardCookieManger.DeleteShopCard();
    }
}
