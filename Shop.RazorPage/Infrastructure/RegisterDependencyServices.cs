using Shop.RazorPage.Infrastructure.CookieUtils;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Services.Auth;
using Shop.RazorPage.Services.Banners;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.Services.Comments;
using Shop.RazorPage.Services.MainPage;
using Shop.RazorPage.Services.Orders;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.Services.Roles;
using Shop.RazorPage.Services.Sellers;
using Shop.RazorPage.Services.ShippingMethods;
using Shop.RazorPage.Services.Sliders;
using Shop.RazorPage.Services.Transactions;
using Shop.RazorPage.Services.UserAddress;
using Shop.RazorPage.Services.Users;
using StackExchange.Redis;

namespace Shop.RazorPage.Infrastructure;

public static class RegisterDependencyServices
{
    const string baseAddress = "https://localhost:5001/api/";
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<HttpClientAuthorizationDelegatingHandler>();
        services.AddTransient<IRenderViewToString, RenderViewToString>();
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddScoped<IMainPageService, MainPageService>();
        services.AddCookieManager();
        services.AddScoped<ShopCardCookieManger>();

        services.AddHttpClient<IAuthService, AuthService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IBannerService, BannerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ICategoryService, CategoryService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ICommentService, CommentService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IOrderService, OrderService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IProductService, ProductService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IRoleService, RoleService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        });
        services.AddHttpClient<ISellerService, SellerService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ISliderService, SliderService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IUserAddressService, UserAddressService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IUserService, UserService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IShippingMethodService, ShippingMethodService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ITransactionService, TransactionService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return services;
    }
}
