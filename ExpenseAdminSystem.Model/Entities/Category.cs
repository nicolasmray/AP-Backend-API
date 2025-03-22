namespace ExpenseAdminSystem.Model.Entities;

public class Category {

    public Category(int id){
        Id = id;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }    
    public DateTime CreatedAt { get; set; }
}