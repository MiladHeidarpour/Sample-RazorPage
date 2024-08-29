using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.UserAddresses;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Services.UserAddress;

namespace Shop.RazorPage.Pages.Profile.Addresses
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IRenderViewToString _renderViewToString;

        public IndexModel(IUserAddressService userAddressService, IRenderViewToString renderViewToString)
        {
            _userAddressService = userAddressService;
            _renderViewToString = renderViewToString;
        }

        public List<AddressDto> Addresses { get; set; }
        public async Task OnGet()
        {
            Addresses = await _userAddressService.GetUserAddresses();
        }

        public async Task<IActionResult> OnGetShowAddPage()
        {
            return await AjaxTryCatch(async () =>
            {
                var view =await _renderViewToString.RenderToStringAsync("_Add",new CreateUserAddressCommand(),PageContext);
                return ApiResult<string>.Success(view);
            });
        }
    }
}
