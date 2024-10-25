using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.RazorPage.Infrastructure;
using Shop.RazorPage.Infrastructure.RazorUtils;
using Shop.RazorPage.Models.Command.Comments;
using Shop.RazorPage.Models.Response.Comments;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Models.Response.Sellers;
using Shop.RazorPage.Services.Comments;
using Shop.RazorPage.Services.Products;
using Shop.RazorPage.Services.Sellers;

namespace Shop.RazorPage.Pages;

public class ProductModel : BaseRazorPage
{
    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly ICommentService _commentService;

    public ProductModel(IProductService productService, ISellerService sellerService, ICommentService commentService)
    {
        _productService = productService;
        _sellerService = sellerService;
        _commentService = commentService;
    }

    public SingleProductDto SingleProduct { get; set; }
    public async Task<IActionResult> OnGet(string slug)
    {
        var product = await _productService.GetSingleProductBySlug(slug);
        if (product == null)
        {
            return NotFound();
        }

        SingleProduct = product;
        return Page();
    }

    public async Task<IActionResult> OnGetProductComments(long productId, int pageId = 1)
    {
        var commentResult = await _commentService.GetProductComments(pageId, 14, productId);
        return Partial("Shared/Products/_Comments", commentResult);
    }

    public async Task<IActionResult> OnPost(long productId, string comment, string slug)
    {
        if (User.Identity.IsAuthenticated == false)
        {
            return Page();
        }
        var result = await _commentService.AddComment(new AddCommentCommand()
        {
            UserId = User.GetUserId(),
            ProductId = productId,
            Text = comment,
        });
        if (result.IsSuccess == false)
        {
            ErrorAlert(result.MetaData.Message);
            return Page();
        }
        SuccessAlert("نظر شما با موفقیت ثبت شد.لطفا منتظر بمانید");
        return RedirectToPage("Product", new { slug = slug });
    }

    public async Task<IActionResult> OnPostDeleteComment(long id)
    {
        return await AjaxTryCatch(() => _commentService.DeleteComment(id));
    }
}