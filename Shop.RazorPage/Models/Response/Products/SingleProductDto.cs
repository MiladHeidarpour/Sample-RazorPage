using Shop.RazorPage.Models.Response.Sellers;

namespace Shop.RazorPage.Models.Response.Products
{
    public class SingleProductDto
    {
        public ProductDto ProductDto { get; set; }
        public List<InventoryDto> InventoryDtos { get; set; }
    }
}
