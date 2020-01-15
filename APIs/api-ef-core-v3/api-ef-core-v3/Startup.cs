using api_ef_core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api_ef_core_v3
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			// Ef Core InMemory
			//services.AddDbContext<DataContext>(opt => opt.UseMemoryDatabase("Database"));

			// Gestao de Dependencia // Cria em memoria uma instancia unica. Sem novas conexoes no banco e destruir no final da requisição.
			services.AddScoped<DataContext, DataContext>();

			//dotnet  Core v3
			//services.AddControllers();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			// forçar HTTPS
			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
