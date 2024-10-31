using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Orders;
using Shop.RazorPage.Models.Response.Orders;

namespace Shop.RazorPage.Services.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await _client.PostAsJsonAsync("order", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> CheckoutOrder(CheckOutOrderCommand command)
    {
        var result = await _client.PostAsJsonAsync("order/checkout", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DecreaseOrderItem(DecreaseOrderItemCountCommand command)
    {
        var result = await _client.PutAsJsonAsync("order/orderItem/DecreaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command)
    {
        var result = await _client.DeleteAsync($"order/orderItem/{command.OrderItemId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> SendOrder(long orderId)
    {
        var result = await _client.PutAsync($"order/sendOrder/{orderId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<OrderDto?> GetCurrentOrder()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<OrderDto?>>($"Order/Current");
        return result?.Data;
    }

    public async Task<OrderDto?> GetOrderById(long orderId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<OrderDto?>>($"order/{orderId}");
        return result?.Data;
    }

    public async Task<OrderFilterResult> GetOrders(OrderFilterParams filterParams)
    {
        var url = $"order/?pageId={filterParams.PageId}&take={filterParams.Take}";
        if (filterParams.StartDate != null)
            url += "&startDate=" + filterParams.StartDate;

        if (filterParams.EndDate != null)
            url += "&endDate=" + filterParams.EndDate;

        if (filterParams.Status != null)
            url += "&status=" + filterParams.Status;

        if (filterParams.UserId != null)
            url += "&UserId=" + filterParams.UserId;

        var result = await _client.GetFromJsonAsync<ApiResult<OrderFilterResult>>(url);
        return result?.Data;
    }

    public async Task<OrderFilterResult> GetUserOrders(int pageId, int take, OrderStatus? status)
    {
        var url = $"order/current/filter?pageId={pageId}&take={take}";
        if (status != null)
            url += $"&status={status}";
        var result = await _client
            .GetFromJsonAsync<ApiResult<OrderFilterResult>>(url);
        return result?.Data;
    }


    public async Task<ApiResult> IncreaseOrderItem(IncreaseOrderItemCountCommand command)
    {
        var result = await _client.PutAsJsonAsync("order/orderItem/IncreaseCount", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }
}
