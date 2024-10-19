using Shop.RazorPage.Models.Response.Banners;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Models.Response.Sliders;

namespace Shop.RazorPage.Models.Response.MainPage
{
    public class MainPageDto
    {
        public List<SliderDto> Sliders { get; set; }
        public List<BannerDto> Banners { get; set; }
        public List<ProductShopDto> SpecialProduct { get; set; }
        public List<ProductShopDto> LatestProducts { get; set; }
        public List<ProductShopDto> TopVisitProducts { get; set; }

    }
}
