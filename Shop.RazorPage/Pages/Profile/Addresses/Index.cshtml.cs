using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Services.UserAddress;

namespace Shop.RazorPage.Pages.Profile.Addresses
{
    public class IndexModel : PageModel
    {
        private readonly IUserAddressService _userAddressService;

        public IndexModel(IUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        public List<AddressDto> Addresses { get; set; }
        public async Task OnGet()
        {
            Addresses = await _userAddressService.GetUserAddresses();
        }
    }
}
