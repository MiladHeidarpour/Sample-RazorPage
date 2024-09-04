using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Banners;
using Shop.RazorPage.Models.Response.Banners;
using Shop.RazorPage.Services.Banners;

namespace Shop.RazorPage.Pages.Admin.Banners;

public class IndexModel : BaseRazorPage
{
    private readonly IBannerService _banner;
    private readonly IRenderViewToString _renderView;

    public IndexModel(IBannerService banner, IRenderViewToString renderView)
    {
        _banner = banner;
        _renderView = renderView;
    }

    public List<BannerDto> Banners { get; set; }
    public async Task OnGet()
    {
        Banners = await _banner.GetList();
    }

    public async Task<IActionResult> OnGetRenderAddPage()
    {
        return await AjaxTryCatch(async () =>
        {
            var view = await _renderView.RenderToStringAsync("_Add", new CreateBannerCommand(), PageContext);
            return ApiResult<string>.Success(view);
        });
    }

    public async Task<IActionResult> OnPostCreateBanner(CreateBannerCommand command)
    {
        return await AjaxTryCatch(() =>
            _banner.CreateBanner(command));
    }

    public async Task<IActionResult> OnGetRenderEditPage(long bannerId)
    {
        return await AjaxTryCatch(async () =>
        {
            var banner = await _banner.GetBannerById(bannerId);
            if (banner == null)
                return ApiResult<string>.Error();

            var model = new EditBannerCommand()
            {
                Id = bannerId,
                Link = banner.Link,
                Position = banner.Position
            };
            var view = await _renderView.RenderToStringAsync("_Edit", model, PageContext);
            return ApiResult<string>.Success(view);
        });
    }

    public async Task<IActionResult> OnPostEditBanner(EditBannerCommand command)
    {
        return await AjaxTryCatch(() =>
            _banner.EditBanner(command));
    }

    public async Task<IActionResult> OnPostDelete(long bannerId )
    {
        return await AjaxTryCatch(() =>
            _banner.DeleteBanner(bannerId));
    }
}
