using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.ILoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.DeleteById;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repositoryWriteOnly;
    private readonly IExpensesReadOnlyRepository _repositoryReadOnly;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IloggedUser _loggedUser;
    public DeleteExpenseUseCase( IExpensesWriteOnlyRepository repositoryWriteOnly, IExpensesReadOnlyRepository repositoryReadOnly, IUnitOfWork umitOfWork, IloggedUser loggedUser)
    {
        _repositoryWriteOnly = repositoryWriteOnly;
        _unitOfWork = umitOfWork;
        _loggedUser = loggedUser;
        _repositoryReadOnly = repositoryReadOnly;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var expenses = await _repositoryReadOnly.GetById(loggedUser,id);

        if (expenses is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        await _repositoryWriteOnly.Delete(id);

        await _unitOfWork.Commit();
    }
}
