using Shop.RazorPage.Models.Command.UserAddresses;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Models;
using Shop.RazorPage.ViewModels.Users.Addresses;

namespace Shop.RazorPage.Services.UserAddress;

public interface IUserAddressService
{
    Task<ApiResult> CreateAddress(CreateUserAddressCommand command);
    Task<ApiResult> EditAddress(EditUserAddressCommand command);
    Task<ApiResult> DeleteAddress(long addressId);
    Task<ApiResult> SetActiveAddress(long addressId);

    Task<AddressDto?> GetAddressById(long id);
    Task<List<AddressDto>> GetUserAddresses();
}
