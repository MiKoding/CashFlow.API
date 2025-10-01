using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Services.ILoggedUser;

namespace CashFlow.Application.UseCases.Users.Profile;
public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly IloggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserProfileUseCase(IloggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();

        return _mapper.Map<ResponseUserProfileJson>(user);  
    }
}