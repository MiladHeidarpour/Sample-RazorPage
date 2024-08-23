namespace Shop.RazorPage.Models.Command.Orders;

public class AddOrderItemCommand
{
    public long InventoryId { get; set; }
    public long UserId { get; set; }
    public int Count { get; set; }
}