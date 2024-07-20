using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infraestructure.DataAccess;
using System.Linq;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpensesUseCase
{
    private readonly IExpensensesRepository _repository;
    public RegisterExpensesUseCase(IExpensensesRepository repository)
    {
        _repository = repository;   
    }
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        var dbContext = new CashFlowDbContext();
        var entity = new Expense
        {
            Amount = request.Amount,
            Date = request.Date,
            Description = request.Description,
            Title = request.Title,
            paymentType = (Domain.Enums.PaymentType)request.paymentType,
        };

        _repository.Add(entity);    
        

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
