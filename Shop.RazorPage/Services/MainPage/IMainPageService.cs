using Shop.RazorPage.Models.Response.MainPage;

namespace Shop.RazorPage.Services.MainPage;

public interface IMainPageService
{
    Task<MainPageDto> GetMainPage();
}
