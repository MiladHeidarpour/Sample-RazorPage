
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Roles;
using Shop.RazorPage.Models.Response.Roles;

namespace Shop.RazorPage.Services.Roles;

public interface IRoleService
{
    Task<ApiResult> CreateRole(CreateRoleCommand command);
    Task<ApiResult> EditRole(EditRoleCommand command);
    Task<RoleDto> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}
