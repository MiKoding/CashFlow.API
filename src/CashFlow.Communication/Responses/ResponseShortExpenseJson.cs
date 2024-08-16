namespace CashFlow.Communication.Responses
{
    public class ResponseShortExpenseJson
    {
        public long IdExpense { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
