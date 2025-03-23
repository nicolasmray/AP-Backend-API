using System;
using ExpenseAdminSystem.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace ExpenseAdminSystem.Model.Repositories;
public class UserRepository : BaseRepository
{
public UserRepository(IConfiguration configuration) : base(configuration)
{
}
public User GetUserById(int id)
{
NpgsqlConnection dbConn = null;
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from \"user\" where id = @id";
cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
if (data.Read()) //every time loop runs it reads next like from fetched rows
{
return new User(Convert.ToInt32(data["id"]))
{
//Id = (int)data["id"],
UserName = data["username"].ToString(),
Email = data["email"].ToString(),
Password = data["password_hash"].ToString(),
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
public List<User> GetUsers()
{
NpgsqlConnection dbConn = null;
var users = new List<User>();
try
{
//create a new connection for database
dbConn = new NpgsqlConnection(ConnectionString);
//creating an SQL command
var cmd = dbConn.CreateCommand();
cmd.CommandText = "select * from \"user\"";
//call the base method to get data
var data = GetData(dbConn, cmd);
if (data != null)
{
while (data.Read()) //every time loop runs it reads next like from fetched rows
{
User s = new User(Convert.ToInt32(data["id"]))
{
//Id = (int)data["id"],
UserName = data["username"].ToString(),
Email = data["email"].ToString(),
Password = data["password_hash"].ToString(),
CreatedAt = Convert.ToDateTime(data["created_at"])
};
users.Add(s);
}
}
return users;
}
finally
{
dbConn?.Close();
}
}
//add a new student
public bool InsertUser(User s)
{
NpgsqlConnection dbConn = null;
try
{
dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
insert into ""user""
(username, email, password_hash, created_at)
values
(@username, @email, @password_hash, @created_at)
";
//adding parameters in a better way
//cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, s.UserName);
cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, s.Email);
cmd.Parameters.AddWithValue("@password_hash", NpgsqlDbType.Text, s.Password);
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
public bool UpdateUser(User s)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
update ""user"" set
username=@username,
email=@email,
password_hash=@password_hash,
created_at=@created_at,
where
id = @id";
//cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer,s.Id);
cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, s.UserName);
cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, s.Email);
cmd.Parameters.AddWithValue("@password_hash", NpgsqlDbType.Text, s.Password);
cmd.Parameters.AddWithValue("@created_at", NpgsqlDbType.Date, s.CreatedAt);
bool result = UpdateData(dbConn, cmd);
return result;
}
public bool DeleteUser(int id)
{
var dbConn = new NpgsqlConnection(ConnectionString);
var cmd = dbConn.CreateCommand();
cmd.CommandText = @"
delete from ""user""
where id = @id
";
//adding parameters in a better way
cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
//will return true if all goes well
bool result = DeleteData(dbConn, cmd);
return result;
}
}
