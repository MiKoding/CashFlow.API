using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.ILoggedUser;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infraestructure.DataAccess;
using System.Linq;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IloggedUser _loggedUser;

    public RegisterExpensesUseCase(IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IloggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
        
    }
    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        Validade(request);

        var loggedUser = await _loggedUser.Get();

        //var dbContext = new CashFlowDbContext();
        var expense = _mapper.Map<Expense>(request);
        expense.UserId = loggedUser.Id;

        await _repository.Add(expense);  
        await _unitOfWork.Commit();
        

        return _mapper.Map<ResponseRegisteredExpenseJson>(expense);
    }

    public void Validade(RequestExpenseJson request)
    {
       var validator = new ExpenseValidator();

        var result = validator.Validate(request);

       if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
