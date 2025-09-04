using CashFlow.Exception;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.IntegrationTest.InlineData;

namespace WebApi.IntegrationTest.Expenses.Delete;
public class DeleteExpenseTest : CashFlowClassFixture
{
    private const string METHOD = "api/Expenses";

    private readonly string _token;
    private readonly long expenseId;

    public DeleteExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_Team_Member.GetToken();
        expenseId = webApplicationFactory.Expense.GetExpenseId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{METHOD}/{expenseId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        result = await DoGet(requestUri: $"{METHOD}/{expenseId}", token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string culture)
    {
        var result = await DoDelete(requestUri: $"{METHOD}/1000", token: _token, culture: culture);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);  

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString(ResourceErrorMessages.EXPENSE_NOT_FOUND, new System.Globalization.CultureInfo(culture));

        errors.Should().HaveCount(1).And.Contain(error=>error.GetString()!.Equals(expectedMessage));



    }
}
