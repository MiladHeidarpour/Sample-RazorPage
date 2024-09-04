using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models.Response.Banners;

namespace Shop.RazorPage.Models.Command.Banners;

public class CreateBannerCommand
{
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url,ErrorMessage = "لینک نامعتبر است")]
    public string Link { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "موقعیت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public BannerPosition Position { get; set; }
}
