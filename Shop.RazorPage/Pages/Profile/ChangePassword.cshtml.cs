using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Users;
using Shop.RazorPage.Services.Users;

namespace Shop.RazorPage.Pages.Profile;

[BindProperties]
[Authorize]
public class ChangePasswordModel : BaseRazorPage
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور فعلی باید بیشتر از 5 کارکتر باشد")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(5, ErrorMessage = "کلمه عبور جدید باید بیشتر از 5 کارکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")] 
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }



    private readonly IUserService _userService;

    public ChangePasswordModel(IUserService userService)
    {
        _userService = userService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.ChangePassword(new ChangePasswordCommand()
        {
            CurrentPassword = CurrentPassword,
            Password = Password,
            ConfirmPassword = ConfirmPassword,
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
