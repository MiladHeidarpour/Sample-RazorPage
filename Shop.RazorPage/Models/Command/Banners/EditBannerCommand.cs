using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models.Response.Banners;
using System.ComponentModel.DataAnnotations;

namespace Shop.RazorPage.Models.Command.Banners;

public class EditBannerCommand
{
    public long Id { get; set; }


    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url, ErrorMessage = "لینک نامعتبر است")]
    public string Link { get; set; }


    [Display(Name = "عکس")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }


    [Display(Name = "موقعیت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public BannerPosition Position { get; set; }
}
