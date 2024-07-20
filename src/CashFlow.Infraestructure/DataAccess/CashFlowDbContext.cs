using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess;
internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=3306;Database=cashFlowDb;Uid=root;Pwd=chicaosql;";
        var serverVersion = new MySqlServerVersion(new Version(8,0,28));


        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}
