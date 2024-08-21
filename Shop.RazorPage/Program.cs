using Shop.RazorPage.Infrastructure;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.RegisterApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
