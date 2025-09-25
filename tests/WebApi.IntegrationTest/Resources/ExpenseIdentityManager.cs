using CashFlow.Domain.Entities;

namespace WebApi.IntegrationTest.Resources;
public class ExpenseIdentityManager
{
    private readonly Expense _expense;

    public ExpenseIdentityManager(Expense expense)
    {
        _expense = expense;
    }

    public long GetExpenseId() => _expense.IdExpense;  

    public DateTime GetDate() => _expense.Date;
}
