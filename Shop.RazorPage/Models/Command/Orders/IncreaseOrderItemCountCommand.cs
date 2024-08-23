namespace Shop.RazorPage.Models.Command.Orders;

public class IncreaseOrderItemCountCommand
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Count { get; set; }
}