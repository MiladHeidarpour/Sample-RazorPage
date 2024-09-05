using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Response.Categories;
using Shop.RazorPage.Services.Categories;

namespace Shop.RazorPage.Pages.Admin.Categories;

public class IndexModel : BaseRazorPage
{
    private readonly ICategoryService _category;

    public IndexModel(ICategoryService category)
    {
        _category = category;
    }

    public List<CategoryDto> Categories { get; set; }

    public async Task OnGet()
    {
        Categories = await _category.GetCategories();
    }

    public async Task<IActionResult> OnPostDelete(long id)
    {
        return await AjaxTryCatch(() =>
        {
            return _category.DeleteCategory(id);
        });
    }
}
