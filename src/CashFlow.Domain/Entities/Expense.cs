using CashFlow.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Domain.Entities;
public class Expense
{
    [Key]
    public long IdExpense { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType paymentType { get; set; }
}
