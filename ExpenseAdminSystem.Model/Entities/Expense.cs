namespace ExpenseAdminSystem.Model.Entities;

public class Expense {
    public Expense(int id){
        Id = id;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime expenseDate { get; set; }
    public int CategoryId { get; set; }
    public int CurrencyId { get; set; }
    public string Comments { get; set; }    
    public DateTime CreatedAt { get; set; }
}