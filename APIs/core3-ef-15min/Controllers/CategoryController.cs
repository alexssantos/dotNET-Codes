using System.Threading.Tasks;
using core3_ef_15min.Data;
using core3_ef_15min.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace core3_ef_15min.Controllers
{
	[ApiController]
	[Route("v1/categories")]
	public class CategoryController : ControllerBase
	{

		//DONE: Modo Antigo
		// private DataContext _context;
		// public CategoryController(DataContext context)
		// {
		//     _context = context
		// }

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> Get([FromServices] DataContext context)
		{
			var categories = await context.Categories.ToListAsync();
			return Ok(categories);
		}

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAll([FromServices] DataContext context)
		{
			var categories = await context.Categories
				//ignore global query
				.IgnoreQueryFilters()
				.ToListAsync();

			return Ok(categories);
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Post([FromServices] DataContext context, [FromBody] Category model)
		{
			if (ModelState.IsValid)
			{
				context.Categories.Add(model);
				await context.SaveChangesAsync();
				return Ok(model);
			}
			else 
			{
				return BadRequest(ModelState);
			}
		}
	}
}