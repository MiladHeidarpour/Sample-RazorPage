using AutoMapper;
using Shop.RazorPage.Models.Command.UserAddresses;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.ViewModels.Users.Addresses;

namespace Shop.RazorPage.Infrastructure;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserAddressCommand, CreateUserAddressViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressViewModel > ().ReverseMap();
        CreateMap<AddressDto, EditUserAddressViewModel > ().ReverseMap();
    }
}