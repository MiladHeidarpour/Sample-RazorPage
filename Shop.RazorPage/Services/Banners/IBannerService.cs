using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Banners;
using Shop.RazorPage.Models.Response.Banners;

namespace Shop.RazorPage.Services.Banners;

public interface IBannerService
{
    Task<ApiResult> CreateBanner(CreateBannerCommand command);
    Task<ApiResult> EditBanner(EditBannerCommand command);
    Task<ApiResult> DeleteBanner(long bannerId);

    Task<BannerDto?> GetBannerById(long bannerId);
    Task<List<BannerDto>> GetList();
}
