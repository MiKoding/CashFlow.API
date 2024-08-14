using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensensesRepository
{
    Task Add(Expense expense);
}
