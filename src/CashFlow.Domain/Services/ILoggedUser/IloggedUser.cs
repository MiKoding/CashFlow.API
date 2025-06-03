using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Services.ILoggedUser;
public interface IloggedUser
{
    Task<User> Get();
}
