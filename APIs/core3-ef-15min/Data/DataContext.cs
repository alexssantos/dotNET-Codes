using core3_ef_15min.Models;
using Microsoft.EntityFrameworkCore;

namespace core3_ef_15min.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        //tables
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories {get; set;}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// global query default.

			// Allow Soft delete			
			modelBuilder.Entity<Category>().HasQueryFilter(x => !x.Deleted);
		}
        
    }
}