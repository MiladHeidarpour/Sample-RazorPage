using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Categories;
using Shop.RazorPage.Models.Response.Categories;

namespace Shop.RazorPage.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> AddChild(AddChildCategoryCommand command)
    {
        var result = await _client.PostAsJsonAsync("Category/AddChild", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _client.PostAsJsonAsync("Category", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteCategory(long categoryId)
    {
        var result = await _client.DeleteAsync($"Category/{categoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _client.PutAsJsonAsync("Category", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<List<CategoryDto>?> GetCategories()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<CategoryDto>>>("Category");
        return result.Data;
    }

    public async Task<CategoryDto?> GetCategoryById(long categoryId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CategoryDto>>($"Category/{categoryId}");
        return result.Data;
    }

    public async Task<List<ChildCategoryDto>> GetChilds(long parentId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<ChildCategoryDto>>>($"Category/GetChild/{parentId}");
        return result.Data;
    }
}
