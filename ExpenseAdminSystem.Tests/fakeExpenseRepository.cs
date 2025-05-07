using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

public class FakeExpenseRepository : ExpenseRepository
{
    private readonly List<Expense> _expenses = new List<Expense>
    {
        new Expense(1) { UserId = 1, Amount = 100, ExpenseDate = DateTime.Now, CategoryId = 1, CurrencyId = 1, Comments = "Lunch", CreatedAt = DateTime.Now },
        new Expense(2) { UserId = 1, Amount = 50, ExpenseDate = DateTime.Now, CategoryId = 2, CurrencyId = 1, Comments = "Taxi", CreatedAt = DateTime.Now }
    };

    public FakeExpenseRepository() : base(new ConfigurationBuilder().AddInMemoryCollection().Build()) { }

    public override Expense GetExpenseById(int id) => _expenses.FirstOrDefault(e => e.Id == id);

    public override List<Expense> GetExpensesByUserId(int userId) =>
        _expenses.Where(e => e.UserId == userId).ToList();

    public override bool InsertExpense(Expense expense)
    {
        _expenses.Add(expense);
        return true;
    }

    public override bool UpdateExpense(Expense expense)
    {
        var existing = _expenses.FirstOrDefault(e => e.Id == expense.Id);
        if (existing == null) return false;

        existing.Amount = expense.Amount;
        existing.ExpenseDate = expense.ExpenseDate;
        existing.CategoryId = expense.CategoryId;
        existing.CurrencyId = expense.CurrencyId;
        existing.Comments = expense.Comments;
        return true;
    }

    public override bool DeleteExpense(int id)
    {
        var expense = _expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null) return false;
        _expenses.Remove(expense);
        return true;
    }
}

