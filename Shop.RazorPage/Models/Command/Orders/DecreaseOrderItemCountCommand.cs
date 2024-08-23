namespace Shop.RazorPage.Models.Command.Orders;

public class DecreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}