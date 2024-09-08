using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.ViewModels;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Products;
using Shop.RazorPage.Models.Response.Products;

namespace Shop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class EditModel : BaseRazorPage
{
    private readonly IProductService _product;

    public EditModel(IProductService product)
    {
        _product = product;
    }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }



    [Display(Name = "عکس محصول")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile? ImageFile { get; set; }



    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint($"CKEditor4")]
    public string Description { get; set; }


    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1, long.MaxValue, ErrorMessage = "دسته بندی را وارد کنید")]
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

    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await _product.GetProductById(productId);
        if (product == null)
        {
            return RedirectToPage("Index");
        }

        Title = product.Title;
        Slug = product.Slug;
        Description = product.Description;
        SeoData = SeoDataViewModel.MapSeoDataViewModel(product.SeoData);
        CategoryId = product.Category.Id;
        SubCategoryId = product.SubCategory.Id;
        SecondarySubCategoryId = product.SecondarySubCategory.Id;
        InitSpecifications(product.Specifications);
        return Page();

    }

    public async Task<IActionResult> OnPost(long productId)
    {
        var result = await _product.EditProduct(new EditProductCommand()
        {
            ProductId = productId,
            Title = Title,
            SeoData = SeoData.MapSeoData(),
            Slug = Slug,
            ImageFile = ImageFile,
            SecondarySubCategoryId = SecondarySubCategoryId,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            Description = Description,
            Specifications = ConvertSpecifications(),
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"), RedirectToPage("Edit", new { productId = productId }));
    }

    public void InitSpecifications(List<ProductSpecificationDto> specifications)
    {
        foreach (var item in specifications)
        {
            Keys.Add(item.Key);
            Values.Add(item.Value);
        }
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