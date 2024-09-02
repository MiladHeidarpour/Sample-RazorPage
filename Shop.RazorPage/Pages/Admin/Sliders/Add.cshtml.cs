using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models.Command.Sliders;
using Shop.RazorPage.Services.Sliders;

namespace Shop.RazorPage.Pages.Admin.Sliders;

[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly ISliderService _slider;

    public AddModel(ISliderService slider)
    {
        _slider = slider;
    }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _slider.CreateSlider(new CreateSliderCommand()
        {
            ImageFile = ImageFile,
            Link = Link,
            Title = Title
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
