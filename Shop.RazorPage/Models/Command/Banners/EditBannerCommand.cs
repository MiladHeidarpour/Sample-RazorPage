using Shop.RazorPage.Models.Response.Banners;

namespace Shop.RazorPage.Models.Command.Banners;

public class EditBannerCommand
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}
