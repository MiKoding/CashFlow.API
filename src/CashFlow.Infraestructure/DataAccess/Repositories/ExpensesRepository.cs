﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
       await _dbContext.Expenses.AddAsync(expense);    
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
       return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e=> e.IdExpense ==  id);
    }
    
    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
       return await _dbContext.Expenses.FirstOrDefaultAsync(e=> e.IdExpense ==  id);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.IdExpense == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);
        return true;
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
}
