using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.RazorPage.Pages.Profile;
[Authorize]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
