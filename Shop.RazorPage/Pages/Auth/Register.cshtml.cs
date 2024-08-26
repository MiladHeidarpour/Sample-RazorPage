using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Services.Auth;

namespace Shop.RazorPage.Pages.Auth;

[BindProperties]
[ValidateAntiForgeryToken]
public class RegisterModel : BaseRazorPage
{

    [DisplayName("شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]

    public string PhoneNumber { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبورر یکسان نیستند")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }


    private readonly IAuthService _authService;
    public RegisterModel(IAuthService authService)
    {
        _authService = authService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _authService.Register(new RegisterCommand()
        {
            PhoneNumber = PhoneNumber,
            Password = Password,
            ConfirmPassword = ConfirmPassword
        });

        return RedirectAndShowAlert(result, RedirectToPage("Login"));

        //if (result.IsSuccess == false)
        //{
        //    ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
        //    return Page();
        //}

        //return RedirectToPage("Login");
    }
}
