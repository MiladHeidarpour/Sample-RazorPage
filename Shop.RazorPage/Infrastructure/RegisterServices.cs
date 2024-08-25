using Shop.RazorPage.Services.Auth;
using Shop.RazorPage.Services.Banners;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.Services.Comments;
using Shop.RazorPage.Services.Orders;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.Services.Roles;
using Shop.RazorPage.Services.Sellers;
using Shop.RazorPage.Services.Sliders;
using Shop.RazorPage.Services.UserAddress;
using Shop.RazorPage.Services.Users;

namespace Shop.RazorPage.Infrastructure;

public static class RegisterServices
{
    const string baseAddress = "https://localhost:5001/api/";
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {

        services.AddHttpClient<IAuthService, AuthService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IBannerService, BannerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<ICategoryService, CategoryService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<ICommentService, CommentService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IOrderService, OrderService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IProductService, ProductService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IRoleService, RoleService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<ISellerService, SellerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<ISliderService, SliderService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IUserAddressService, UserAddressService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<IUserService, UserService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });

        return services;
    }
}
