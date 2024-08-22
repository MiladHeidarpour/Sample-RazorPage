using Shop.RazorPage.Models.Response.Banners;

namespace Shop.RazorPage.Models.Command.Banners;

public class CreateBannerCommand
{
    public string Link { get; set; }
    public  IFormFile ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}
