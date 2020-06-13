using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SENSORTRACKING.DAO;
using SENSORTRACKING.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SENSORTRACKING.SERVICES
{
	public static class ServiceCollectionExtension
	{
		public static void AddDataAccessServicesDev(this IServiceCollection services)
		{
			services.AddDbContext<SensorTrackingDbContext>(options =>
				options.UseInMemoryDatabase("radix-stdb"));
		}

		public static void AddDataAccessServices(this IServiceCollection services, string connString)
		{
			services.AddDbContext<SensorTrackingDbContext>(options =>
				options.UseSqlServer(connString));

		}

		public static void AddSensorTrackingServices(this IServiceCollection services)
		{
			//Services da API
			services.AddScoped<SensorService, SensorService>();
		}

		public static void AddSensorTrackingDaos(this IServiceCollection services)
		{
			//Services da API
			services.AddScoped<SensorDao, SensorDao>();
		}

		public static void FillDatabase(SensorTrackingDbContext context)
		{
			bool createdDb = context.Database.EnsureCreated();
			if (createdDb)
			{
				string done = "criado";
			}

			if (context.Sensores.Any())
			{
				return;
			}

			DateTime startDt = DateTime.UtcNow;
			double timestampInicio = startDt.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
			double timestampFim = startDt.AddMinutes(2).Subtract(DateTime.UnixEpoch).TotalMilliseconds;

			int idTemp = 1;

			// 10 regioes.
			ICollection<string> regiaoList = new List<string>() {
				"rio_de_janeiro", "sao_paulo", "curitiba","joinvile", "maranhao","paraiba",
				"brasilia","sergipe","belo_horizonte","fortaleza"
			};

			// de 10 a 15 sensores por regiao
			string base_name = "sensor_0";

			// 5min de dados: 60x5 = 300/sensor
			// valor: string e numero
			// 10 reg x 15 sensor_name x 300 sensor_input = 45.000

			ICollection<SensorModel> novosModels = new List<SensorModel>();
			foreach (string reg in regiaoList)
			{
				int qtddSensorReg = GetRandomInt(15);
				ICollection<int> intChoised = new HashSet<int>();
				while (intChoised.Count != qtddSensorReg)
				{
					intChoised.Add(GetRandomInt(15));
				}

				foreach (int num in intChoised)
				{
					int oneSecond = 1000;
					double timeStampCur = timestampInicio;
					while (timeStampCur < timestampFim)
					{
						string valor = GetValue();
						string tag = "brasil." + reg + "." + base_name + num;
						long timestamp = Convert.ToInt64(Math.Round(timeStampCur));

						SensorModel model = SensorModel.Build(tag, timestamp, valor);
						//model.Id = idTemp;
						novosModels.Add(model);

						idTemp += 1;
						timeStampCur += oneSecond;
					}
				}
			}

			// https://docs.microsoft.com/pt-br/ef/core/modeling/data-seeding#custom-initialization-logic
			context.Sensores.AddRangeAsync(novosModels);
			context.SaveChangesAsync();
		}

		private static int GetRandomInt(int max)
		{
			Random r = new Random();
			return r.Next(0, max); //for ints
		}

		private static string GetValue()
		{
			int rand = GetRandomInt(10);

			// 80% valores numericos // 20% valores string
			if (rand > 2)
			{
				return GetRandomInt(100).ToString();
			}
			return "val00-" + GetRandomInt(100) + "00";
		}
	}
}
