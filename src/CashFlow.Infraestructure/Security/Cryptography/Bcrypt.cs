using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;
namespace CashFlow.Infraestructure.Security.Cryptography;
internal class Bcrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
