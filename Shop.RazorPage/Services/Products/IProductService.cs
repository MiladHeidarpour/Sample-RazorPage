using Shop.RazorPage.Models.Command.Products;
using Shop.RazorPage.Models.Response.Products;
using Shop.RazorPage.Models;

namespace Shop.RazorPage.Services.Products;

public interface IProductService
{
    Task<ApiResult> CreateProduct(CreateProductCommand command);
    Task<ApiResult> EditProduct(EditProductCommand command);
    Task<ApiResult> AddImage(AddProductImageCommand command);
    Task<ApiResult> DeleteProductImage(DeleteProductImageCommand command);

    Task<ProductDto?> GetProductById(long productId);
    Task<ProductDto?> GetProductBySlug(string slug);
    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
    Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams);
}
