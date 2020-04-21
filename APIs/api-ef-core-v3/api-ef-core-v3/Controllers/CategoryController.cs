using api_ef_core.Data;
using api_ef_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_ef_core.Controllers
{
	[ApiController]
	[Route("v1/categories")]
	public class CategoryController : Controller
	{
		public CategoryController()
		{
		}

		[HttpGet]
		[Route("")]
		public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
		{
			var categories = await context.Categories.ToListAsync();
			return categories;
		}
	}
}
