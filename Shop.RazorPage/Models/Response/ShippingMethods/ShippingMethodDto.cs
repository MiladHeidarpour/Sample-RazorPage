namespace Shop.RazorPage.Models.Response.ShippingMethods;

public class ShippingMethodDto : BaseDto
{
    public string Title { get; set; }
    public int Cost { get; set; }
}