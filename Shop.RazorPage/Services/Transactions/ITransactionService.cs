using System.Reflection.Emit;
using Shop.RazorPage.Models;
using Shop.RazorPage.Models.Command.Transactions;

namespace Shop.RazorPage.Services.Transactions;

public interface ITransactionService
{
    Task<ApiResult<string>> CreateTransaction(CreateTransactionCommand command);
}