using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infraestructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infraestructure;
public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services) //utilizado para injetar as dependencias do ExpenseRepository
    {

        services.AddScoped<IExpensensesRepository, ExpensesRepository>();
    }
}
