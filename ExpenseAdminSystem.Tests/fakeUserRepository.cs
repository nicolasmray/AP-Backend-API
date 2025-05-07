using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

public class FakeUserRepository : UserRepository
{
    private readonly List<User> _users = new List<User>
    {
        new User(1) { UserName = "test", Password = "123" },
        new User(2) { UserName = "admin", Password = "admin" }
    };

    public FakeUserRepository() : base(new ConfigurationBuilder().AddInMemoryCollection().Build())
    {
    }

    public override User GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public override List<User> GetUsers()
    {
        return _users;
    }

    public override bool InsertUser(User user)
    {
        _users.Add(user);
        return true;
    }

    public override bool UpdateUser(User user)
    {
        var existing = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null) return false;
        existing.UserName = user.UserName;
        return true;
    }

    public override bool DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;
        _users.Remove(user);
        return true;
    }
}
