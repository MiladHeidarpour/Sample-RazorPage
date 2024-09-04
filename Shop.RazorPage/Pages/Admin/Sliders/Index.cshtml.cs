using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Response.Sliders;
using Shop.RazorPage.Services.Sliders;

namespace Shop.RazorPage.Pages.Admin.Sliders;

public class IndexModel : BaseRazorPage
{
    private readonly ISliderService _slider;

    public IndexModel(ISliderService slider)
    {
        _slider = slider;
    }

    public List<SliderDto> Sliders { get; set; }
    public async Task OnGet()
    {
        Sliders = await _slider.GetSliders();
    }

    public async Task<IActionResult> OnPostDeleteSlider(long sliderId)
    {
        return await AjaxTryCatch(() =>
        {
            return _slider.DeleteSlider(sliderId);
        });
    }
}
