using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Requests;
using FluentValidation;
using System.Data;

namespace CashFlow.Application.UseCases.Users.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
