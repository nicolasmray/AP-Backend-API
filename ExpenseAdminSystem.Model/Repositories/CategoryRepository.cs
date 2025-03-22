using System;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace ExpenseAdminSystem.Model.Repositories;
public class CategoryRepository : BaseRepository
{
public CategoryRepository(IConfiguration configuration) : base(configuration)
{
}
public Category GetCategoryById(int id)
{
NpgsqlConnection dbConn = null;
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from category where id = @id";
cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
if (data.Read()) //every time loop runs it reads next like from fetched rows
{
return new Category(Convert.ToInt32(data["id"]))
{
Id = (int)data["id"],
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
public List<Category> GetCategories()
{
NpgsqlConnection dbConn = null;
var categories = new List<Category>();
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from category";
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
while (data.Read()) //every time loop runs it reads next like from fetched rows
{
Category s = new Category(Convert.ToInt32(data["id"]))
{
Id = (int)data["id"],
Name = data["name"].ToString()
};
categories.Add(s);
}
}
return categories;
}
finally
{
dbConn?.Close();
}
}
//add a new student
public bool InsertCategory(Category s)
{
NpgsqlConnection dbConn = null;
try
{
dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
insert into category
(id, name)
values
(@id, @name)
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
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
public bool UpdateCategory(Category s)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
update category set
id=@id,
name=@name,
where
id = @id";
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Text, s.Name);
bool result = UpdateData(dbConn, cmd);
return result;
}
public bool DeleteCategory(int id)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
delete from category
where id = @id
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
//will return true if all goes well
bool result = DeleteData(dbConn, cmd);
return result;
}
}