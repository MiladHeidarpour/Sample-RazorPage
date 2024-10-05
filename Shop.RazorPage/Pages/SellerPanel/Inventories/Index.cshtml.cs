using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Sellers;
using Shop.RazorPage.Models.Response.Sellers;
using Shop.RazorPage.Services.Sellers;

namespace Shop.RazorPage.Pages.SellerPanel.Inventories;

public class IndexModel : BaseRazorPage
{
    private readonly ISellerService _seller;
    private readonly IRenderViewToString _renderViewToString;
    public IndexModel(ISellerService seller, IRenderViewToString renderViewToString)
    {
        _seller = seller;
        _renderViewToString = renderViewToString;
    }

    public List<InventoryDto> Inventories { get; set; }
    public async Task<IActionResult> OnGet()
    {
        var seller = await _seller.GetCurrentSeller();
        if (seller == null)
        {
            return RedirectToPage("Index");
        }

        Inventories = await _seller.GetSellerInventories();
        return Page();
    }

    public async Task<IActionResult> OnGetEditPage(long inventoryId)
    {
        return await AjaxTryCatch(async () =>
        {
            var inventory = await _seller.GetInventoryById(inventoryId);
            if (inventory == null)
            {
                return ApiResult<string>.Success("اطلاعات نامعتبر است");
            }
            var view = await _renderViewToString.RenderToStringAsync("_Edit", new EditSellerInventoryCommand
            {
                SellerId = inventory.SellerId,
                InventoryId = inventory.Id,
                Count = inventory.Count,
                Price = inventory.Price,
                DiscountPercentage = inventory.DiscountPercentage,
            }, PageContext);
            return ApiResult<string>.Success(view);
        });
    }

    public async Task<IActionResult> OnPost(EditSellerInventoryCommand command)
    {
        return await AjaxTryCatch(async () =>await _seller.EditInventory(command));
    }
}