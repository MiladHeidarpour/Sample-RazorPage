using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Categories;
using Shop.RazorPage.Models.Response.Categories;

namespace Shop.RazorPage.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult> CreateCategory(CreateCategoryCommand command);
    Task<ApiResult> EditCategory(EditCategoryCommand command);
    Task<ApiResult> DeleteCategory(long categoryId);
    Task<ApiResult> AddChild(AddChildCategoryCommand command);


    Task<CategoryDto?> GetCategoryById(long categoryId);
    Task<List<CategoryDto>?> GetCategories();
    Task<List<ChildCategoryDto>> GetChilds(long parentId);
}
