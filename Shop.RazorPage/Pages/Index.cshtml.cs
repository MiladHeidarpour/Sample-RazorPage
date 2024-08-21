using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models.Command.Auth;
using Shop.RazorPage.Services.Auth;

namespace Shop.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAuthService _authService;

        public IndexModel(ILogger<IndexModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public async Task OnGet()
        {
            var result = await _authService.Login(new LoginCommand()
            {
                PhoneNumber = "09111111111",
                Password = "12345678",
            });
            //var result= await _authService.Register(new RegisterCommand()
            //{
            //    PhoneNumber = "09111111111",
            //    Password = "12345678",
            //    ConfirmPassword = "12345678"
            //});
        }
    }
}
