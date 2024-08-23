using Shop.RazorPage.Models.Response.Roles;

namespace Shop.RazorPage.Models.Command.Roles;

public class CreateRoleCommand
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}