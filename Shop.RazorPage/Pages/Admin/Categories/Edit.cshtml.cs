using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Services.Categories;
using Shop.RazorPage.ViewModels;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Categories;

namespace Shop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly ICategoryService _category;

    public EditModel(ICategoryService category)
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
    public async Task<IActionResult> OnGet(long id)
    {
        var category = await _category.GetCategoryById(id);
        if (category == null)
        {
            return RedirectToPage("Index");
        }

        Title = category.Title;
        Slug = category.Slug;
        SeoData = SeoDataViewModel.MapSeoDataViewModel(category.SeoData);
        return Page();
    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await _category.EditCategory(new EditCategoryCommand()
        {
            Id = id,
            Title = Title,
            Slug = Slug,
            SeoData = SeoData.MapSeoData(),
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

