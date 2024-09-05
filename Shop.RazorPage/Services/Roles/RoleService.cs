using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Roles;
using Shop.RazorPage.Models.Response.Roles;

namespace Shop.RazorPage.Services.Roles;

public class RoleService : IRoleService
{
    private readonly HttpClient _client;
    private const string ModuleName = "Role";
    public RoleService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> CreateRole(CreateRoleCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditRole(EditRoleCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<RoleDto> GetRoleById(long roleId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<RoleDto?>>($"{ModuleName}/{roleId}");
        return result.Data;
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<RoleDto>>>(ModuleName);
        return result.Data;
    }
}
