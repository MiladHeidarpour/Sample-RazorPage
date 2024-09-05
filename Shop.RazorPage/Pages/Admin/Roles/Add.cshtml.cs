using Microsoft.AspNetCore.Mvc;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Roles;
using Shop.RazorPage.Services.Roles;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.Utils;
using Shop.RazorPage.Models.Response.Roles;

namespace Shop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly IRoleService _role;

    public AddModel(IRoleService role)
    {
        _role = role;
    }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string[] permission)
    {
        var permissionModel = new List<Permission>();
        foreach (var item in permission)
        {
            try
            {
                permissionModel.Add(EnumUtils.ParseEnum<Permission>(item));
            }
            catch
            {
                // 
            }
        }
        var result = await _role.CreateRole(new CreateRoleCommand()
        {
            Title = Title,
            Permissions = permissionModel
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
