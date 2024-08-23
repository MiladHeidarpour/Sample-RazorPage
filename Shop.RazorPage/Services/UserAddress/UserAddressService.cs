﻿using Shop.RazorPage.Models.Command.UserAddresses;
using Shop.RazorPage.Models.Response.UserAddresses;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.Services.UserAddress;

public class UserAddressService : IUserAddressService
{
    private readonly HttpClient _client;
    private const string ModuleName = "userAddresse";
    public UserAddressService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateAddress(CreateUserAddressCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditAddress(EditUserAddressCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> DeleteAddress(long addressId)
    {
        var result = await _client.DeleteAsync($"{ModuleName}/{addressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<AddressDto?> GetAddressById(long id)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<AddressDto?>>(ModuleName);
        return result.Data;
    }

    public async Task<List<AddressDto>> GetUserAddresses()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<List<AddressDto>?>>(ModuleName);
        return result.Data;
    }
}