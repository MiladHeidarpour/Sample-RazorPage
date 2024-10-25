using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Infrastructure.CookieUtils;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Orders;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Services.Orders;

namespace Shop.RazorPage.Pages;

public class ShopCartModel : BaseRazorPage
{
    private readonly IOrderService _orderService;
    private readonly ShopCardCookieManger _shopCardCookieManger;

    public ShopCartModel(IOrderService orderService, ShopCardCookieManger shopCardCookieManger)
    {
        _orderService = orderService;
        _shopCardCookieManger = shopCardCookieManger;
    }

    public OrderDto? OrderDto { get; set; }
    public async Task OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            OrderDto = await _orderService.GetCurrentOrder();
        }
        else
        {
            OrderDto = _shopCardCookieManger.GetShopCard();
        }
    }
    public async Task<IActionResult> OnPostAddItem(int count, long inventoryId)
    {
        if (User.Identity.IsAuthenticated)
        {
            return await AjaxTryCatch(() =>
            {
                return _orderService.AddOrderItem(new AddOrderItemCommand()
                {
                    UserId = User.GetUserId(),
                    Count = count,
                    InventoryId = inventoryId,
                });
            });
        }
        else
        {
            return await AjaxTryCatch(() =>
            {
                return _shopCardCookieManger.AddItem(inventoryId, count);
            });
        }

    }
    public async Task<IActionResult> OnPostDeleteItem(long id)
    {
        if (User.Identity.IsAuthenticated)
        {
            return await AjaxTryCatch(() =>
            {
                return _orderService.DeleteOrderItem(new DeleteOrderItemCommand()
                {
                    OrderItemId = id,
                });
            });
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                _shopCardCookieManger.DeleteOrderItem(id);
                return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnPostIncreaseItemCount(long id)
    {
        if (User.Identity.IsAuthenticated)
        {
            return await AjaxTryCatch(() => _orderService.IncreaseOrderItem(new IncreaseOrderItemCountCommand()
            {
                UserId = User.GetUserId(),
                Count = 1,
                ItemId = id,
            }));
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                _shopCardCookieManger.Increase(id);
                return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnPostDecreaseItemCount(long id)
    {
        if (User.Identity.IsAuthenticated)
        {
            return await AjaxTryCatch(() => _orderService.DecreaseOrderItem(new DecreaseOrderItemCountCommand()
            {
                UserId = User.GetUserId(),
                Count = 1,
                ItemId = id,
            }));
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                _shopCardCookieManger.Decrease(id);
                return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnGetShopCardDetail()
    {
        OrderDto? order = new OrderDto();
        if (User.Identity.IsAuthenticated)
        {
            order = await _orderService.GetCurrentOrder();
        }
        else
        {
            order = _shopCardCookieManger.GetShopCard();
        }

        return new ObjectResult(new
        {
            items = order?.Items,
            count = order?.Items.Count,
            price = $"{order?.Items.Sum(s => s.TotalPrice):#,0}",
        });
    }
}

