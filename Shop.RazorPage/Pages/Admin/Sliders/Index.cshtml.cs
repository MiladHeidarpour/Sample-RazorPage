using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models.Response.Sliders;
using Shop.RazorPage.Services.Sliders;

namespace Shop.RazorPage.Pages.Admin.Sliders;

public class IndexModel : PageModel
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
}
