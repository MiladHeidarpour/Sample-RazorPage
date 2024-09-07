using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.Infrastructure.RazorUtils;


public class BaseRazorFilter<TFilterParam> : PageModel where TFilterParam : BaseFilterParam/*,new()*/
{
    [BindProperty(SupportsGet = true)]
    public TFilterParam FilterParams { get; set; }

    //public BaseRazorFilter()
    //{
    //    FilterParam=new TFilterParam();
    //}
}