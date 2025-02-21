using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Login;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;

    public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository, IAccessTokenGenerator accessTokenGenerator, IPasswordEncripter passwordEncripter)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if(user is null)
        {
            throw new InvalidLoginException();
        }

        var verifyPassword =  _passwordEncripter.Verify(request.Password, user.Password);

        if(verifyPassword is false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
        };
    }
}
