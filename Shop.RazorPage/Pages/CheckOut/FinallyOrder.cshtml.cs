using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages.CheckOut;

public class FinallyOrderModel : PageModel
{
    private readonly IOrderService _orderService;

    public FinallyOrderModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public OrderDto OrderDto { get; set; }
    public async Task<IActionResult> OnGet(long orderId)
    {
        var order = await _orderService.GetOrderById(orderId);

        if (order == null || order.UserId != User.GetUserId())
        {
            return Redirect("/");
        }

        OrderDto = order;
        return Page();
    }
}