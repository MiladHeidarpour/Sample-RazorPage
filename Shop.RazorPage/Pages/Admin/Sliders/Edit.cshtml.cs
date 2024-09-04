using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models;
using Shop.RazorPage.Services.Sliders;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Models.Command.Sliders;

namespace Shop.RazorPage.Pages.Admin.Sliders;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly ISliderService _slider;

    public EditModel(ISliderService slider)
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
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }

    public string ImageName { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var slider = await _slider.GetSliderById(id);
        if (slider == null)
        {
            return RedirectToPage("Index");
        }

        Title = slider.Title;
        Link = slider.Link;
        ImageName = slider.ImageName;

        return Page();
    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await _slider.EditSlider(new EditSliderCommand()
        {
            Id = id,
            Title = Title,
            Link = Link,
            ImageFile = ImageFile,
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}
