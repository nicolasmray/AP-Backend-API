using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ExpenseAdminSystem.API.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
protected ExpenseRepository Repository {get;}
public ExpenseController(ExpenseRepository repository) {
Repository = repository;
}
[HttpGet("{id}")]
public ActionResult<Expense> GetExpense([FromRoute] int id)
{
Expense expense = Repository.GetExpenseById(id);
if (expense == null) {
return NotFound();
}
return Ok(expense);
}
[HttpGet]
public ActionResult<IEnumerable<Expense>> GetExpenses()
{
return Ok(Repository.GetExpenses());
}
[HttpPost]
public ActionResult Post([FromBody] Expense expense) {
if (expense == null)
{
return BadRequest("Student info not correct");
}
bool status = Repository.InsertExpense(expense);
if (status)
{
return Ok();
}
return BadRequest();
}
[HttpPut]
public ActionResult UpdateExpense([FromBody] Expense expense)
{
if (expense == null)
{
return BadRequest("Expense info not correct");
}
Expense existinExpense = Repository.GetExpenseById(expense.Id);
if (existinExpense == null)
{
return NotFound($"Expense with id {expense.Id} not found");
}
bool status = Repository.UpdateExpense(expense);
if (status)
{
return Ok();
}
return BadRequest("Something went wrong");
}
[HttpDelete("{id}")]
public ActionResult DeleteExpense([FromRoute] int id) {
Expense existingExpense = Repository.GetExpenseById(id);
if (existingExpense == null)
{
return NotFound($"Expense with id {id} not found");
}
bool status = Repository.DeleteExpense(id);
if (status)
{
return NoContent();
}
return BadRequest($"Unable to delete expense with id {id}");
}
}
}

