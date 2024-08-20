using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.UpdateById;
public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}
