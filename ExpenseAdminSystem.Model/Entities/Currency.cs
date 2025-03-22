namespace ExpenseAdminSystem.Model.Entities;

public class Currency {
    public Currency(int id){
        Id = id;
    }
    
    public int Id { get; set; }
    public string Code { get; set; }   
    public string Name { get; set; }   

}