﻿using Shop.RazorPage.Models.Command.Users;
using Shop.RazorPage.Models.Response.Users;
using Shop.RazorPage.Models;
using Shop.RazorPage.Pages.Profile;

namespace Shop.RazorPage.Services.Users;

public interface IUserService
{
    Task<ApiResult> CreateUser(CreateUserCommand command);
    Task<ApiResult> EditUser(EditUserCommand command);
    Task<ApiResult> EditCurrentUser(EditUserCommand command);
    Task<ApiResult> ChangePassword(ChangePasswordCommand command);

    Task<UserDto?> GetUserById(long userId);
    Task<UserDto?> GetCurrentUser();
    Task<UserFilterResult> GetUsersByFilter(UserFilterParams filterParams);
}
