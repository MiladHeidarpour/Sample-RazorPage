using Shop.RazorPage.Models.Response.ShippingMethods;

namespace Shop.RazorPage.Services.ShippingMethods;

public interface IShippingMethodService
{
    Task<List<ShippingMethodDto>> GetShippingMethods();
}