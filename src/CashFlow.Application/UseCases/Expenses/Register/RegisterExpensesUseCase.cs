using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infraestructure.DataAccess;
using System.Linq;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensensesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpensesUseCase(IExpensensesRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        //var dbContext = new CashFlowDbContext();
        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);  
        await _unitOfWork.Commit();
        

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    public void Validade(RequestRegisterExpenseJson request)
    {
       var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

       if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
        
    }
}
