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

	}
}
