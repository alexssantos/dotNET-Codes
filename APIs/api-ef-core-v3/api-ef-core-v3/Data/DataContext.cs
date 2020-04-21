using api_ef_core.Models;
using Microsoft.EntityFrameworkCore;

namespace api_ef_core.Data
{
	public class DataContext : DbContext
	{

		//SqlServer
		//private string connString { get; set; }

		public DataContext(DbContextOptions<DataContext> myOptions) : base(options: myOptions)
		{
		}

		//Collections to Use
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }

	}
}
