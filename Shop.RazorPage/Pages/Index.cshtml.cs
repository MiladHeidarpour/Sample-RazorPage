using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Models.Response.MainPage;
using Shop.RazorPage.Services.Auth;
using Shop.RazorPage.Services.MainPage;

namespace Shop.RazorPage.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMainPageService _mainPageService;
    private readonly IMemoryCache _memoryCache;

    public IndexModel(ILogger<IndexModel> logger, IMainPageService mainPageService, IMemoryCache memoryCache)
    {
        _logger = logger;
        _mainPageService = mainPageService;
        _memoryCache = memoryCache;
    }

    public MainPageDto MainPageData { get; set; }
    public async Task OnGet()
    {
        MainPageData = await _memoryCache.GetOrCreateAsync("main-page", (entry) =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15);
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            return _mainPageService.GetMainPage();
        });
    }
}
