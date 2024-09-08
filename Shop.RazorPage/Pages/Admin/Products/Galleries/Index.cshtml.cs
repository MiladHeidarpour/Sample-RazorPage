using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Services.Products;
using System.ComponentModel.DataAnnotations;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Products;

namespace Shop.RazorPage.Pages.Admin.Products.Galleries;

[BindProperties]
public class IndexModel : BaseRazorPage
{
    private readonly IProductService _product;

    public IndexModel(IProductService product)
    {
        _product = product;
    }


    public List<ProductImageDto> Images { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Sequence { get; set; }

    [Display(Name = "عکس محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر است")]
    public IFormFile ImageFile { get; set; }





    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await _product.GetProductById(productId);
        if (product == null)
        {
            return RedirectToPage("Index");
        }
        Images = product.Images;
        return Page();
    }

    public async Task<IActionResult> OnPost(long productId)
    {
        return await AjaxTryCatch(() =>
        {
            return _product.AddImage(new AddProductImageCommand()
            {
                ProductId = productId,
                ImageFile = ImageFile,
                Sequence = Sequence
            });
        });
    }

    public async Task<IActionResult> OnPostDeleteItem(long id, long productId)
    {
        Sequence = 1;
        return await AjaxTryCatch(()
            => _product.DeleteProductImage(new DeleteProductImageCommand()
                { ProductId = productId, ImageId = id }), checkModelState: false);
    }
}