using Microsoft.AspNetCore.Mvc;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.Services.Products;

namespace Shop.RazorPage.Pages.Admin.Products;

public class IndexModel : BaseRazorFilter<ProductFilterParams>
{
    private readonly IProductService _product;
    private readonly ICategoryService _category;
    public IndexModel(IProductService product, ICategoryService category)
    {
        _product = product;
        _category = category;
    }


    public ProductFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult = await _product.GetProductByFilter(FilterParams);
    }

    public async Task<IActionResult> OnGetLoadChildCategories(long parentId)
    {
        var options = "<option value='0'>انتخاب کنید</option>";
        var child = await _category.GetChilds(parentId);
        foreach (var item in child)
        {
            options += $"<option value='{item.Id}'>{item.Title}</option>";
        }
        return Content(options);
    }
}
