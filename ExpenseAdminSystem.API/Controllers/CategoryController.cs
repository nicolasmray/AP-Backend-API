using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ExpenseAdminSystem.API.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
protected CategoryRepository Repository {get;}
public CategoryController(CategoryRepository repository) {
Repository = repository;
}
[HttpGet("{id}")]
public ActionResult<Category> GetCategory([FromRoute] int id)
{
Category category = Repository.GetCategoryById(id);
if (category == null) {
return NotFound();
}
return Ok(category);
}
[HttpGet]
public ActionResult<IEnumerable<Category>> GetCategories()
{
return Ok(Repository.GetCategories());
}
[HttpPost]
public ActionResult Post([FromBody] Category category) {
if (category == null)
{
return BadRequest("Student info not correct");
}
bool status = Repository.InsertCategory(category);
if (status)
{
return Ok();
}
return BadRequest();
}
[HttpPut]
public ActionResult UpdateCategory([FromBody] Category category)
{
if (category == null)
{
return BadRequest("Category info not correct");
}
Category existinCategory = Repository.GetCategoryById(category.Id);
if (existinCategory == null)
{
return NotFound($"Category with id {category.Id} not found");
}
bool status = Repository.UpdateCategory(category);
if (status)
{
return Ok();
}
return BadRequest("Something went wrong");
}
[HttpDelete("{id}")]
public ActionResult DeleteCategory([FromRoute] int id) {
Category existingCategory = Repository.GetCategoryById(id);
if (existingCategory == null)
{
return NotFound($"Category with id {id} not found");
}
bool status = Repository.DeleteCategory(id);
if (status)
{
return NoContent();
}
return BadRequest($"Unable to delete category with id {id}");
}
}
}

