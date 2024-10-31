using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages.Profile.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public OrderFilterResult FilterResult { get; set; }
    public async Task OnGet(int pageId = 1, OrderStatus? status = null)
    {
        var filterResult = await _orderService.GetUserOrders(pageId, 10, status);
        FilterResult = filterResult;
    }
}