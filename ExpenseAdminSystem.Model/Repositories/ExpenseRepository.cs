using System;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace ExpenseAdminSystem.Model.Repositories;
public class ExpenseRepository : BaseRepository
{
public ExpenseRepository(IConfiguration configuration) : base(configuration)
{
}
public Expense GetExpenseById(int id)
{
NpgsqlConnection dbConn = null;
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from user where id = @id";
cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
if (data.Read()) //every time loop runs it reads next like from fetched rows
{
return new Expense(Convert.ToInt32(data["id"]))
{
//Id = (int)data["id"],
UserId = (int)data["user_id"],
Amount = (decimal)data["amount"],
ExpenseDate = Convert.ToDateTime(data["expense_date"]),
CategoryId = (int)data["category_id"],
CurrencyId = (int)data["currency_id"],
Comments = data["comments"].ToString(),
CreatedAt = Convert.ToDateTime(data["created_at"])
};
}
}
return null;
}
finally
{
dbConn?.Close();
}
}
public List<Expense> GetExpenses()
{
NpgsqlConnection dbConn = null;
var expenses = new List<Expense>();
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from expense";
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
while (data.Read()) //every time loop runs it reads next like from fetched rows
{
Expense s = new Expense(Convert.ToInt32(data["id"]))
{
//Id = (int)data["id"],
UserId = (int)data["user_id"],
Amount = (decimal)data["amount"],
ExpenseDate = Convert.ToDateTime(data["expense_date"]),
CategoryId = (int)data["category_id"],
CurrencyId = (int)data["currency_id"],
Comments = data["comments"].ToString(),
CreatedAt = Convert.ToDateTime(data["created_at"])
};
expenses.Add(s);
}
}
return expenses;
}
finally
{
dbConn?.Close();
}
}
//add a new student
public bool InsertExpense(Expense s)
{
NpgsqlConnection dbConn = null;
try
{
dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
insert into expense
(user_id, amount, expense_date, category_id, currency_id, comments, created_at)
values
(@user_id, @amount, @expense_date, @category_id, @currency_id, @comments, @created_at)
";
//adding parameters in a better way
//cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer,s.UserId);
cmd.Parameters.AddWithValue("@amount", NpgsqlDbType.Numeric,s.Amount);
cmd.Parameters.AddWithValue("@category_id", NpgsqlDbType.Integer,s.CategoryId);
cmd.Parameters.AddWithValue("@currency_id", NpgsqlDbType.Integer,s.CurrencyId);
cmd.Parameters.AddWithValue("@comments", NpgsqlDbType.Text, s.Comments);
cmd.Parameters.AddWithValue("@created_at", NpgsqlDbType.Date, s.CreatedAt);

//will return true if all goes well
bool result = InsertData(dbConn, cmd);
return result;
}
finally
{
dbConn?.Close();
}
}
public bool UpdateExpense(Expense s)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
update expense set
user_id=@user_id,
amount=@amount,
category_id=@category_id,
currency_id=@currency_id,
comments=@comments,
created_at=@created_at,
where
id = @id";
//cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer,s.UserId);
cmd.Parameters.AddWithValue("@amount", NpgsqlDbType.Numeric,s.Amount);
cmd.Parameters.AddWithValue("@category_id", NpgsqlDbType.Integer,s.CategoryId);
cmd.Parameters.AddWithValue("@currency_id", NpgsqlDbType.Integer,s.CurrencyId);
cmd.Parameters.AddWithValue("@comments", NpgsqlDbType.Text, s.Comments);
cmd.Parameters.AddWithValue("@created_at", NpgsqlDbType.Date, s.CreatedAt);
bool result = UpdateData(dbConn, cmd);
return result;
}
public bool DeleteExpense(int id)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
delete from expense
where id = @id
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
//will return true if all goes well
bool result = DeleteData(dbConn, cmd);
return result;
}
}
