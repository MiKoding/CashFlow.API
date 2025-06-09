using CashFlow.Domain.Entities;
using CashFlow.Domain.Services.ILoggedUser;
using Moq;

namespace CommonTestUtilities.LoggedUser;
public class LoggedUserBuilder
{
    public static IloggedUser Build(User user)
    {
        var mock = new Mock<IloggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object; 
    }
}
