namespace Shop.RazorPage.Models.Command.Products;

public class DeleteProductImageCommand
{
    public long ImageId { get; set; }
    public long ProductId { get; set; }
}