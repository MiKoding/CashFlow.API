using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase 
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    public RegisterUserUseCase(IMapper mapper, IPasswordEncripter passwordEncripter, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;   
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await ValidateUser(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(user.Password);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name
        };
    }

    private async Task ValidateUser(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }
        if (result.IsValid == false)
        {
            var errorMessage = result.Errors.Select(f => f.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
