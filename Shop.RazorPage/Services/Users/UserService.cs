﻿using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Models.Command.Users;
using Shop.RazorPage.Models.Response.Users;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _client;
    private const string ModuleName = "users";
    public UserService(HttpClient client)
    {
        _client = client;
    }
    public async Task<ApiResult> CreateUser(CreateUserCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditUser(EditUserCommand command)
    {
        var result = await _client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _client.PutAsJsonAsync($"{ModuleName}/ChangePassword", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<UserDto?>>($"{ModuleName}/{userId}");
        return result.Data;
    }

    public async Task<UserDto?> GetCurrentUser()
    {
        var result = await _client.GetFromJsonAsync<ApiResult<UserDto?>>($"{ModuleName}/current");
        return result.Data;
    }

    public async Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams)
    {
        var url = filterParams.GenerateBaseFilterUrl(ModuleName) +
                  $"email={filterParams.Email}&phoneNumber={filterParams.PhoneNumber}&id={filterParams.Id}";
        var result = await _client.GetFromJsonAsync<ApiResult<UserFilterResult>>(url);
        return result.Data;
    }
}