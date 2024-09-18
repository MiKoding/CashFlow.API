using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestsRegisterExpensesJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        //var faker = new Faker();

        //var request = new RequestRegisterExpenseJson
        //{
        //    Amount = 100,
        //    Date = faker.Date.Past(),
        //    Description = "descripto",
        //    Title = faker.Commerce.Product(),
        //    paymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
        //}; Forma alternativa de buildar exemplos com o Faker Bogus

        return new Faker<RequestExpenseJson>().RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
                                                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                                                .RuleFor(r => r.Date, faker => faker.Date.Past())
                                                .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max : 100))
                                                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>());


    }
}
