using System.Collections.Generic;
using System.Threading.Tasks;
using core3_ef_15min.Data;
using core3_ef_15min.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace core3_ef_15min.Controllers
{
	[ApiController]
	[Route("v1/products")]
	public class ProductController : ControllerBase
	{

		//FIXME: Modo Antigo
		// private DataContext _context;
		// public ProductController(DataContext context)
		// {
		//     _context = context
		// }

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> Get([FromServices] DataContext context)
		{
			var products = await context.Products
				.Include(x => x.Category)
				.ToListAsync();

			return Ok(products);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetById([FromServices] DataContext context,int id)
		{
			var product = await context.Products
				.Include(bean => bean.Category)
				.AsNoTracking()
				.FirstOrDefaultAsync(bean => bean.Id == id);
			return Ok(product);
		}

		[HttpGet]
		[Route("categories/{id:int}")]
		public async Task<IActionResult> GetByCategory([FromServices] DataContext context, int id)
		{
			var products = await context.Products
				.Include(bean => bean.Category)
				.AsNoTracking()
				.Where(bean => bean.Category.Id == id)
				.ToListAsync();
				
			return Ok(products);
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> Post([FromServices] DataContext context, [FromBody] Product model)
		{
			if (ModelState.IsValid)
			{
				context.Products.Add(model);
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