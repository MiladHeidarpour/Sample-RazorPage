using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Infrastructure.Utils.CacheUtil;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.Services.Products;

namespace Shop.RazorPage.Pages.Admin.Products;

public class IndexModel : BaseRazorFilter<ProductFilterParams>
{
    private readonly IProductService _product;
    private readonly ICategoryService _category;
    //private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    public IndexModel(IProductService product, ICategoryService category, IDistributedCache distributedCache/*, IMemoryCache memoryCache*/)
    {
        _product = product;
        _category = category;
        _distributedCache = distributedCache;
        //_memoryCache = memoryCache;
    }


    public ProductFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        //FilterResult = await _product.GetProductByFilter(FilterParams);

        //FilterResult = await _memoryCache.GetOrCreateAsync(CacheKeys.HomePage, (entry) =>
        //{
        //    entry.AbsoluteExpirationRelativeToNow=TimeSpan.FromMinutes(5);
        //    return _product.GetProductByFilter(FilterParams);
        //});

        FilterResult = await _distributedCache.GetOrSet(CacheKeys.HomePage, () =>
        {
            return _product.GetProductByFilter(FilterParams);
        },new CacheOptions()
        {
            AbsoluteExpirationCacheFromMinutes = 1,
            ExpireSlidingCacheFromMinutes = 1,
        });
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
