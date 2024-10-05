using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models.Response.Sellers;
using Shop.RazorPage.Services.Sellers;

namespace Shop.RazorPage.Pages.SellerPanel.Inventories;

public class IndexModel : PageModel
{
    private readonly ISellerService _seller;

    public IndexModel(ISellerService seller)
    {
        _seller = seller;
    }

    public List<InventoryDto> Inventories { get; set; }
    public async Task<IActionResult> OnGet()
    {
        var seller = await _seller.GetCurrentSeller();
        if (seller==null)
        {
            return RedirectToPage("Index");
        }

        Inventories = await _seller.GetSellerInventories();
        return Page();
    }
}