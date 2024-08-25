using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Services.Auth;

namespace Shop.RazorPage.Pages.Auth;
[BindProperties]
[ValidateAntiForgeryToken]
public class LoginModel : PageModel
{
    public LoginModel(IAuthService authService)
    {
        _authService = authService;
    }

    [DisplayName("شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]

    public string PhoneNumber { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    private readonly IAuthService _authService;

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect("/");
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
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
        HttpContext.Response.Cookies.Append("token", token);
        HttpContext.Response.Cookies.Append("refreshToken", refreshToken);
        return Redirect("/");
    }
}
