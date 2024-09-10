
namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
internal interface IGenerateExpenseReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
