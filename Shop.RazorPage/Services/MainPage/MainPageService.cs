using Shop.RazorPage.Models.Response.MainPage;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Services.Banners;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.Services.Sliders;

namespace Shop.RazorPage.Services.MainPage;

public class MainPageService : IMainPageService
{
    private readonly ISliderService _sliderService;
    private readonly IBannerService _bannerService;
    private readonly IProductService _productService;

    public MainPageService(ISliderService sliderService, IBannerService bannerService, IProductService productService)
    {
        _sliderService = sliderService;
        _bannerService = bannerService;
        _productService = productService;
    }

    public async Task<MainPageDto> GetMainPage()
    {
        var slider = await _sliderService.GetSliders();
        var banners = await _bannerService.GetList();

        var latestProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
            SearchOrderBy = ProductSearchOrderBy.Latest,
        });
        var latestProducts = latestProductsResult.Data;

        var SpecialProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
            JustHasDiscount = true,
        });
        var specialProducts = SpecialProductsResult.Data;

        var topvisitProductsResult = await _productService.GetProductForShop(new ProductShopFilterParam()
        {
            PageId = 1,
            Take = 8,
        });
        var topvisitProducts = topvisitProductsResult.Data;

        return new MainPageDto()
        {
            Banners = banners,
            Sliders = slider,
            LatestProducts = latestProducts,
            SpecialProduct = specialProducts,
            TopVisitProducts = topvisitProducts,
        };
    }
}