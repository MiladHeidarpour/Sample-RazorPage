using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Products;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.ViewModels;

namespace Shop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class AddModel : BaseRazorPage
{

    private readonly IProductService _product;

    public AddModel(IProductService product)
    {
        _product = product;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }



    [Display(Name = "عکس محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }



    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint($"CKEditor4")]
    public string Description { get; set; }


    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1,long.MaxValue,ErrorMessage = "دسته بندی را وارد کنید")]
    public long CategoryId { get; set; }


    [Display(Name = "زیر دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1, long.MaxValue, ErrorMessage = "زیر دسته بندی را وارد کنید")]
    public long SubCategoryId { get; set; }


    [Display(Name = "دسته بندی سوم")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long? SecondarySubCategoryId { get; set; }


    [Display(Name = "Slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    public SeoDataViewModel SeoData { get; set; }
    public List<string> Keys { get; set; } = new();
    public List<string> Values { get; set; } = new();


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (SecondarySubCategoryId==0)
        {
            SecondarySubCategoryId = null;
        }

        var result = await _product.CreateProduct(new CreateProductCommand()
        {
            Title = Title,
            ImageFile = ImageFile,
            Description = Description,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            SecondarySubCategoryId = SecondarySubCategoryId,
            Slug = Slug,
            SeoData = SeoData.MapSeoData(),
            Specifications = ConvertSpecifications(),
        });

        return RedirectAndShowAlert(result,RedirectToPage("Index"));
    }
    private Dictionary<string, string> ConvertSpecifications()
    {
        var specifications = new Dictionary<string, string>();
        Keys.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
        Values.RemoveAll(r => r == null || string.IsNullOrWhiteSpace(r));
        for (var i = 0; i < Keys.Count; i++)
        {
            specifications.Add(Keys[i], Values[i]);
        }

        return specifications;
    }
}
