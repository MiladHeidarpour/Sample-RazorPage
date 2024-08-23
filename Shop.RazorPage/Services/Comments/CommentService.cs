using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Comments;
using Shop.RazorPage.Models.Response.Comments;

namespace Shop.RazorPage.Services.Comments;

public class CommentService : ICommentService
{
    private readonly HttpClient _client;

    public CommentService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ApiResult> AddComment(AddCommentCommand command)
    {
        var result = await _client.PostAsJsonAsync("Comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> EditComment(EditCommentCommand command)
    {
        var result = await _client.PutAsJsonAsync("Comment", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult> ChangeStatus(long commentId, CommentStatus status)
    {
        var result = await _client.PostAsJsonAsync("Comment/ChangeStatus", new { id = commentId, status = status });
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CommentFilterResult>>("Comment");
        return result?.Data;
    }

    public async Task<CommentDto?> GetCommentById(long id)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<CommentDto>>($"Comment/{id}");
        return result?.Data;
    }
}