namespace CashFlow.Communication.Responses
{
    public class ResponseShortExpenseJson
    {
        public long IdExpenses { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
