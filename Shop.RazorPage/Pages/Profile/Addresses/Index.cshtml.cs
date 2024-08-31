using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.UserAddresses;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Services.UserAddress;
using Shop.RazorPage.ViewModels.Users.Addresses;

namespace Shop.RazorPage.Pages.Profile.Addresses
{
    public class IndexModel : BaseRazorPage
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IRenderViewToString _renderViewToString;
        private readonly IMapper _mapper;

        public IndexModel(IUserAddressService userAddressService, IRenderViewToString renderViewToString, IMapper mapper)
        {
            _userAddressService = userAddressService;
            _renderViewToString = renderViewToString;
            _mapper = mapper;
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
                var view = await _renderViewToString.RenderToStringAsync("_Add", new CreateUserAddressViewModel(), PageContext);
                return ApiResult<string>.Success(view);
            });
        }

        public async Task<IActionResult> OnPostAddAddress(CreateUserAddressViewModel viewModel)
        {
            return await AjaxTryCatch(async () =>
            {
                var model = _mapper.Map<CreateUserAddressCommand>(viewModel);
                var result = await _userAddressService.CreateAddress(model);
                return result;
            },true);
        }
    }
}
