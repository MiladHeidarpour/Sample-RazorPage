using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Transactions;

namespace Shop.RazorPage.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _client;

    public TransactionService(HttpClient client)
    {
        _client = client;
    }

    private const string ModuleName = "Transaction";
    public async Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command)
    {
        var result = await _client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult<string>>();
    }
}