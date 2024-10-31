using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Infrastructure.Utils;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages.Admin.Orders;

public class IndexModel : BaseRazorFilter<OrderFilterParams>
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public OrderFilterResult FilterResult { get; set; }
    public async Task OnGet(string? startDate, string? endDate)
    {
        if (string.IsNullOrWhiteSpace(startDate) == false)
            FilterParams.StartDate = startDate.ToMiladi();


        if (string.IsNullOrWhiteSpace(endDate) == false)
            FilterParams.StartDate = endDate.ToMiladi();

        FilterParams.Take = 1;
        FilterResult = await _orderService.GetOrders(FilterParams);
    }
}