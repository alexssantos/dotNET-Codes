using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SENSORTRACKING.DAO;
using SENSORTRACKING.SERVICES;

namespace SENSORTRACKING.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//SqlServer + ConnString
			//services.AddDataAccessServices(Configuration.GetConnectionString("SensorTrackingConn"));

			services.AddDataAccessServices();
			services.AddSensorTrackingServices();
			services.AddSensorTrackingDaos();

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SensorTrackingDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				ServiceCollectionExtension.FillDatabase(dbContext);
			}


			app.UseRouting();

			app.UseAuthorization();

			app.UseCors(option => option.AllowAnyOrigin());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
