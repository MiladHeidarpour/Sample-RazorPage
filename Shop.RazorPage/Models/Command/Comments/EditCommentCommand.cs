namespace Shop.RazorPage.Models.Command.Comments;

public class EditCommentCommand
{
    public long CommentId { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
}