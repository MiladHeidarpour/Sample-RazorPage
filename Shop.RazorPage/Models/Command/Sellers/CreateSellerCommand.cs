namespace Shop.RazorPage.Models.Command.Sellers;

public class CreateSellerCommand
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
}