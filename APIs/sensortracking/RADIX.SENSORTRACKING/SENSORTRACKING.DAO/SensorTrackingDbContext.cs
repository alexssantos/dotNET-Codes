using Microsoft.EntityFrameworkCore;
using SENSORTRACKING.MODELS;

namespace SENSORTRACKING.DAO
{
	public class SensorTrackingDbContext : DbContext
	{

		public SensorTrackingDbContext(DbContextOptions<SensorTrackingDbContext> options) : base(options)
		{
		}

		//tables
		public DbSet<SensorModel> Sensores { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder?.Entity<SensorModel>()
				.Property(p => p.Id)
				.ValueGeneratedOnAdd();
		}
	}
}
