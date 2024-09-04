namespace Shop.RazorPage.Models.Response.Banners;

public class BannerDto:BaseDto
{
    public string Link { get; set; }
    public string ImageName { get; set; }
    public BannerPosition Position { get; set; }
}

public enum BannerPosition
{
    زیر_اسلایدر,
    سمت_راست_اسلایدر,
}
