using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Orders;
using Shop.RazorPage.Models.Command.Transactions;
using Shop.RazorPage.Models.Response.Orders;
using Shop.RazorPage.Models.Response.ShippingMethods;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Services.Orders;
using Shop.RazorPage.Services.ShippingMethods;
using Shop.RazorPage.Services.Transactions;
using Shop.RazorPage.Services.UserAddress;

namespace Shop.RazorPage.Pages.CheckOut;

public class IndexModel : BaseRazorPage
{
    private readonly IOrderService _orderService;
    private readonly IUserAddressService _userAddressService;
    private readonly IShippingMethodService _shippingMethodService;
    private readonly ITransactionService _transactionService;

    public IndexModel(IOrderService orderService, IUserAddressService userAddressService, IShippingMethodService shippingMethodService, ITransactionService transactionService)
    {
        _orderService = orderService;
        _userAddressService = userAddressService;
        _shippingMethodService = shippingMethodService;
        _transactionService = transactionService;
    }

    public List<AddressDto> Addresses { get; set; }
    public OrderDto Order { get; set; }
    public List<ShippingMethodDto> ShippingMethods { get; set; }
    public async Task<IActionResult> OnGet()
    {
        var order = await _orderService.GetCurrentOrder();
        if (order == null)
        {
            return RedirectToPage("../Index");
        }

        Order = order;
        Addresses = await _userAddressService.GetUserAddresses();
        ShippingMethods = await _shippingMethodService.GetShippingMethods();
        if (ShippingMethods.Any() == false)
        {
            return Redirect("../Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost(long shippingMethodId)
    {
        var address = await _userAddressService.GetUserAddresses();
        var currentAddress = address.First(f => f.ActiveAddress == true);
        if (currentAddress == null)
        {
            return RedirectToPage("Index");
        }
        var result = await _orderService.CheckoutOrder(new CheckOutOrderCommand
        {
            UserId = User.GetUserId(),
            Shire = currentAddress.Shire,
            City = currentAddress.City,
            PostalCode = currentAddress.PostalCode,
            PostalAddress = currentAddress.PostalAddress,
            PhoneNumber = currentAddress.PhoneNumber,
            Name = currentAddress.Name,
            Family = currentAddress.Family,
            NationalCode = currentAddress.NationalCode,
            ShippingMethodId = shippingMethodId,
        });
        if (result.IsSuccess == true)
        {
            var currentOrder = await _orderService.GetCurrentOrder();
            var res = await _transactionService.CreateTransaction(new CreateTransactionCommand()
            {
                OrderId = currentOrder.Id,
                SuccessCallBackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/CheckOut/FinallyOrder/{currentOrder.Id}",
                ErrorCallBackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/CheckOut/FinallyOrder/{currentOrder.Id}",
            });
            if (res.IsSuccess == true)
            {
                return Redirect(res.Data);
            }
        }
        ErrorAlert(result.MetaData.Message);
        return RedirectToPage("Index");
    }
}