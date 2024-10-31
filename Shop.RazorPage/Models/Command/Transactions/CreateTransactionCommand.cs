namespace Shop.RazorPage.Models.Command.Transactions;

public class CreateTransactionCommand
{
    public long OrderId { get; set; }
    public string SuccessCallBackUrl { get; set; }
    public string ErrorCallBackUrl { get; set; }
}