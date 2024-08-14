﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesRepository
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

    public async Task<List<Expense>> GetAll(int id)
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }
}
