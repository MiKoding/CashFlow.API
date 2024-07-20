using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensensesRepository
{
    void Add(Expense expense);
}
