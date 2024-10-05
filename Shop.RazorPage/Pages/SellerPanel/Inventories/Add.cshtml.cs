using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Sellers;
using Shop.RazorPage.Services.Sellers;

namespace Shop.RazorPage.Pages.SellerPanel.Inventories;

[BindProperties]
public class AddModel : BaseRazorPage
{
    public long ProductId { get; set; }

    [Display(Name = "تعداد موجود")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Count { get; set; }

    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }

    [Display(Name = "درصد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(0,99,ErrorMessage = "درصد تخفیف نامعتبر است")]
    public int PercentageDiscount { get; set; }



    private readonly ISellerService _seller;

    public AddModel(ISellerService seller)
    {
        _seller = seller;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var seller = await _seller.GetCurrentSeller();
        if (seller == null)
            return RedirectToPage("/");

        var result = await _seller.AddInventory(new AddSellerInventoryCommand()
        {
            SellerId = seller.Id,
            ProductId = ProductId,
            Count = Count,
            Price = Price,
            PercentageDiscount = PercentageDiscount,
        });

        return RedirectAndShowAlert(result,RedirectToPage("Index"));
    }
}