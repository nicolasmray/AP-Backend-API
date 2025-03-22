using System;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace ExpenseAdminSystem.Model.Repositories;
public class CurrencyRepository : BaseRepository
{
public CurrencyRepository(IConfiguration configuration) : base(configuration)
{
}
public Currency GetCurrencyById(int id)
{
NpgsqlConnection dbConn = null;
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from currency where id = @id";
cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
if (data.Read()) //every time loop runs it reads next like from fetched rows
{
return new Currency(Convert.ToInt32(data["id"]))
{
Id = (int)data["id"],
Code = data["code"].ToString(),
Name = data["name"].ToString()
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
public List<Currency> GetCurrencies()
{
NpgsqlConnection dbConn = null;
var currencies = new List<Currency>();
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
Currency s = new Currency(Convert.ToInt32(data["id"]))
{
Id = (int)data["id"],
Code = data["code"].ToString(),
Name = data["name"].ToString()
};
currencies.Add(s);
}
}
return currencies;
}
finally
{
dbConn?.Close();
}
}
//add a new student
public bool InsertCurrency(Currency s)
{
NpgsqlConnection dbConn = null;
try
{
dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
insert into currency
(id, code, name)
values
(@id,@code, @name)
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@code", NpgsqlDbType.Text, s.Code);
cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, s.Name);

//will return true if all goes well
bool result = InsertData(dbConn, cmd);
return result;
}
finally
{
dbConn?.Close();
}
}
public bool UpdateCurrency(Currency s)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
update currency set
id=@id,
code=@code,
name=@name,
where
id = @id";
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@code", NpgsqlDbType.Text, s.Code);
cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, s.Name);
bool result = UpdateData(dbConn, cmd);
return result;
}
public bool DeleteCurrency(int id)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
delete from currency
where id = @id
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
//will return true if all goes well
bool result = DeleteData(dbConn, cmd);
return result;
}
}
