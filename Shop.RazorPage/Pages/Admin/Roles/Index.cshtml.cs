using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Response.Roles;
using Shop.RazorPage.Services.Roles;

namespace Shop.RazorPage.Pages.Admin.Roles;

public class IndexModel : BaseRazorPage
{
    private readonly IRoleService _role;

    public IndexModel(IRoleService role)
    {
        _role = role;
    }

    public List<RoleDto> Roles { get; set; }
    public async Task OnGet()
    { 
        Roles=await _role.GetRoles();
    }
}
