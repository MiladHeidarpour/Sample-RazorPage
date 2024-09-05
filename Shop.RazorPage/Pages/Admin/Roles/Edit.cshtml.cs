using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Services.Roles;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Roles;
using Shop.RazorPage.Models.Response.Roles;

namespace Shop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly IRoleService _role;

    public EditModel(IRoleService role)
    {
        _role = role;
    }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    public List<Permission> Permissions { get; set; }
    public async Task<IActionResult> OnGet(long id)
    {
        var role = await _role.GetRoleById(id);
        if (role == null)
        {
            return RedirectToPage("Index");
        }

        Title = role.Title;
        Permissions = role.Permissions;

        return Page();
    }

    public async Task<IActionResult> OnPost(long id, List<Permission> permissions)
    {
        var result = await _role.EditRole(new EditRoleCommand()
        {
            Id = id,
            Title = Title,
            Permissions = permissions,
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"), RedirectToPage("Edit", new { id = id }));
    }
}
