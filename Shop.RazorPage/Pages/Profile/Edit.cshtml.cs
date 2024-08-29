using Microsoft.AspNetCore.Mvc;
using Shop.RazorPage.Models.Response.Users;
using System.ComponentModel.DataAnnotations;
using Common.Application.Validations.CustomValidation.IFormFile;
using Microsoft.AspNetCore.Authorization;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Users;
using Shop.RazorPage.Services.Users;

namespace Shop.RazorPage.Pages.Profile;

[BindProperties]
[Authorize]
public class EditModel : BaseRazorPage
{
    [Display(Name = "عکس پروفایل")]
    [FileImage(ErrorMessage = "تصویر پروفایل نامعتبر است")]
    public IFormFile? Avatar { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
    [MinLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
    public string PhoneNumber { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Email { get; set; }
    public Gender Gender { get; set; } = Gender.None;


    private readonly IUserService _userService;
    public EditModel(IUserService userService)
    {
        _userService = userService;
    }
    public async Task OnGet()
    {
        var user = await _userService.GetCurrentUser();
        Name = user.Name;
        Family=user.Family;
        PhoneNumber=user.PhoneNumber;
        Email=user.Email;
        Gender = user.Gender;
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userService.EditCurrentUser(new EditUserCommand()
        {
            Name = Name,
            Family = Family,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Gender = Gender,
            Avatar = Avatar,
        });
        return RedirectAndShowAlert(result, RedirectToPage("/Profile/Edit"));
    }
}
