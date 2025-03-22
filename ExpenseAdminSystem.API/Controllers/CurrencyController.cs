using ExpenseAdminSystem.Model.Entities;
using ExpenseAdminSystem.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ExpenseAdminSystem.API.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
protected CurrencyRepository Repository {get;}
public CurrencyController(CurrencyRepository repository) {
Repository = repository;
}
[HttpGet("{id}")]
public ActionResult<Currency> GetCurrency([FromRoute] int id)
{
Currency currency = Repository.GetCurrencyById(id);
if (currency == null) {
return NotFound();
}
return Ok(currency);
}
[HttpGet]
public ActionResult<IEnumerable<Currency>> GetCurrencies()
{
return Ok(Repository.GetCurrencies());
}
[HttpPost]
public ActionResult Post([FromBody] Currency currency) {
if (currency == null)
{
return BadRequest("Student info not correct");
}
bool status = Repository.InsertCurrency(currency);
if (status)
{
return Ok();
}
return BadRequest();
}
[HttpPut]
public ActionResult UpdateCurrency([FromBody] Currency currency)
{
if (currency == null)
{
return BadRequest("Currency info not correct");
}
Currency existinCurrency = Repository.GetCurrencyById(currency.Id);
if (existinCurrency == null)
{
return NotFound($"Currency with id {currency.Id} not found");
}
bool status = Repository.UpdateCurrency(currency);
if (status)
{
return Ok();
}
return BadRequest("Something went wrong");
}
[HttpDelete("{id}")]
public ActionResult DeleteCurrency([FromRoute] int id) {
Currency existingCurrency = Repository.GetCurrencyById(id);
if (existingCurrency == null)
{
return NotFound($"Currency with id {id} not found");
}
bool status = Repository.DeleteCurrency(id);
if (status)
{
return NoContent();
}
return BadRequest($"Unable to delete currency with id {id}");
}
}
}

