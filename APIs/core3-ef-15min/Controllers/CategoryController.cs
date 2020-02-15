using System.Collections.Generic;
using System.Threading.Tasks;
using core3_ef_15min.Data;
using core3_ef_15min.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace core3_ef_15min.Controllers
{
	[ApiController]
	[Route("v1/categories")]
	public class CategoryController : ControllerBase
	{

		//FIXME: Modo Antigo
		// private DataContext _context;
		// public CategoryController(DataContext context)
		// {
		//     _context = context
		// }

		[HttpGet]
		[Route("")]
		public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
		{
			var categories = await context.Categories.ToListAsync();
			return categories;
		}

		[HttpPost]
		[Route("")]
		public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
		{
			if (ModelState.IsValid)
			{
				context.Categories.Add(model);
				await context.SaveChangesAsync();
				return model;
			}
			else 
			{
				return BadRequest(ModelState);
			}
		}
	}
}