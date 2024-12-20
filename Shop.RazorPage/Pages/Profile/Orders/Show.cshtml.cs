using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages.Profile.Orders;

public class ShowModel : PageModel
{
    private readonly IOrderService _orderService;

    public ShowModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public OrderDto Order { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null || order.UserId != User.GetUserId())
        {
            return RedirectToPage("Index");
        }

        Order = order;
        return Page();
    }
}