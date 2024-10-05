using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Models.Command.Sellers;

public class EditSellerInventoryCommand
{
    public long SellerId { get; set; }
    public long InventoryId { get; set; }

    [Display(Name = "تعداد موجود")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Count { get; set; }

    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }

    [Display(Name = "درصد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(0, 99, ErrorMessage = "درصد تخفیف نامعتبر است")]
    public int DiscountPercentage { get; set; }
}