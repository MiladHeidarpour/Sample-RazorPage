namespace Shop.RazorPage.Models.Command.Products;

public class AddProductImageCommand
{
    public IFormFile ImageFile { get; set; }
    public long ProductId { get; set; }
    public int Sequence { get; set; }
}