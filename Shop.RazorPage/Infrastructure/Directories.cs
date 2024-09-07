namespace Shop.RazorPage.Infrastructure;

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

    public static string GetBannerImage(string imageName)
    {
        return $"{SiteSettings.ServerPath}{BannerImages}/{imageName}";
    }

    public static string GetProductImage(string imageName)
    {
        return $"{SiteSettings.ServerPath}{ProductImages}/{imageName}";
    }

    public static string GetProductGalleryImage(string imageName)
    {
        return $"{SiteSettings.ServerPath}{ProductGalleryImage}/{imageName}";
    }
}
