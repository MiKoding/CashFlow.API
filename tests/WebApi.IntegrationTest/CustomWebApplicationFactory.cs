using CashFlow.Infraestructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.IntegrationTest;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests")
            .ConfigureServices( services =>
            {
                //AddEntityFrameworkInMemoryDatabase
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                   config.UseInternalServiceProvider(provider);
                }
                    );
                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();

                StartDataBase(dbContext);
            });
    }

    private void StartDataBase(CashFlowDbContext dbContext)
    {
        var user = UserBuilder.Build();

        dbContext.Users.Add(user);

        dbContext.SaveChanges();
    }
}
