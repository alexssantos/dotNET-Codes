using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thais_charts.api.Models;

namespace thais_charts.api.Data
{
	public class MyDbContext : DbContext
	{

		public MyDbContext(DbContextOptions<MyDbContext> options): base(options) 
		{}

		public DbSet<ChartData> ChartDatas { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder){ }
		
		// Config Provider
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	if (!optionsBuilder.IsConfigured)
		//	{
		//		string connStr = @"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0";
				
		//		//SqlServer Provider
		//		//optionsBuilder.UseSqlServer(connStr);

		//		//SqlLite Provider
		//		//options.UseSqlite("Data Source=blogging.db");
		//	}
		//}		
	}
}
