using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Categories;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.Admin.Categories;


[BindProperties]
public class AddModel : BaseRazorPage
{
    private readonly ICategoryService _category;

    public AddModel(ICategoryService category)
    {
        _category = category;
    }

    [Display(Name = "Slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    public SeoDataViewModel SeoData { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(long? parentId)
    {
        if (parentId == null)
        {
            var result = await _category.CreateCategory(new CreateCategoryCommand()
            {
                Title = Title,
                Slug = Slug,
                SeoData = SeoData.MapSeoData(),
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }

        var res = await _category.AddChild(new AddChildCategoryCommand()
        {
            ParentId = (long)parentId,
            Title = Title,
            Slug = Slug,
            SeoData = SeoData.MapSeoData(),
        });
        return RedirectAndShowAlert(res, RedirectToPage("Index"));

    }
}
