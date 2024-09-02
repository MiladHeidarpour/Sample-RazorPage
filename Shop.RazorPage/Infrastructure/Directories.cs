using Shop.RazorPage.Infrastructure;

namespace Shop.Application._Utilities;

public class Directories
{
    public const string ProductImages = "/images/products";
    public const string ProductGalleryImage = "/images/products/gallery";

    public const string BannerImages = "/images/banners";
    public const string SliderImages = "/images/sliders";

    public const string UserAvatars = "/images/user/avatars";

    public static string GetSliderImage(string imageName)
    {
        return $"{SiteSettings.ServerPath}{SliderImages}/{imageName}";
    }
}
