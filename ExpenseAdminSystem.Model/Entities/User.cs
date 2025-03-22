namespace ExpenseAdminSystem.Model.Entities;

public class User {

    public User(int id){
        Id = id;
    }

    public int Id { get; set; }
    public string UserName { get; set; }

    public string Email { get; set; }    

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
}