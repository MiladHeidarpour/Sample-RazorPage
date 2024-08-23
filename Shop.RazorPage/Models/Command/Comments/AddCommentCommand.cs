namespace Shop.RazorPage.Models.Command.Comments;

public class AddCommentCommand
{
    public string Text { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }

}