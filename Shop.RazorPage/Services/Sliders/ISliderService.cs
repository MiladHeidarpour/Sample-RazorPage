using Shop.RazorPage.Models.Command.Sliders;
using Shop.RazorPage.Models.Response.Sliders;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.Services.Sliders;

public interface ISliderService
{
    Task<ApiResult> CreateSlider(CreateSliderCommand command);
    Task<ApiResult> EditSlider(EditSliderCommand command);
    Task<ApiResult> DeleteSlider(long sliderId);

    Task<SliderDto?> GetSliderById(long sliderId);
    Task<List<SliderDto>> GetSliders();
}
