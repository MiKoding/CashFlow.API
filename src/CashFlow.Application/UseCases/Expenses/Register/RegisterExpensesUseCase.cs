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

    public RegisterExpensesUseCase(IExpensensesRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;   
    }
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        //var dbContext = new CashFlowDbContext();
        var entity = new Expense
        {
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            Title = request.Title,
            paymentType = (Domain.Enums.PaymentType)request.paymentType,
        };

        _repository.Add(entity);  
        _unitOfWork.Commit();
        

        return new ResponseRegisteredExpenseJson();
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
