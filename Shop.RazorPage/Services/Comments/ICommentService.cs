using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Comments;
using Shop.RazorPage.Models.Response.Comments;

namespace Shop.RazorPage.Services.Comments;

public interface ICommentService
{
    Task<ApiResult> AddComment(AddCommentCommand command);
    Task<ApiResult> EditComment(EditCommentCommand command);
    Task<ApiResult> ChangeStatus(long commentId, CommentStatus status);
    Task<ApiResult> DeleteComment(long commentId);

    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams);
    Task<CommentFilterResult> GetProductComments(int pageId, int take, long productId);
    Task<CommentDto?> GetCommentById(long id);
}
