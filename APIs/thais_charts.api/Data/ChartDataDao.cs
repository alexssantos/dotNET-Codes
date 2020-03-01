using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thais_charts.api.Data;
using thais_charts.api.Models;

namespace thais_charts.api.Controllers
{
	public class ChartDataDao
	{
		protected readonly MyDbContext _context;

		public ChartDataDao(MyDbContext context)
		{
			_context = context;
		}

		public Dictionary<CHART_TYPE, Dictionary<string, List<ChartData>>> GetAll()
		{			
			var list = _context.ChartDatas.ToList();
			if (list.Count == 0)
			{
				list = CreateMany(this.BuildMock());
			}

			Dictionary<CHART_TYPE, Dictionary<string, List<ChartData>>> dict = list.GroupBy(x => x.TipoChart)
				.ToDictionary(
					key => key.Key,
					val => val
						.GroupBy(group => group.Label)
						.ToDictionary(
							label => label.Key,
							charts => charts.ToList()
							));

			return dict;
		}

		public List<ChartData> CreateMany(List<ChartData> entities)
		{
			_context.ChartDatas.AddRange(entities);
			_context.SaveChanges();

			return entities;
		}

		private List<ChartData> BuildMock()
		{
			int i = 1;
			int SERIE_LENGHT = 7;
			var listData = new List<ChartData>();


			//pie
			listData.Add(new ChartData() { Id = i++, Label = "", TipoChart = CHART_TYPE.PIE, Value = RandomNumber(0,100)});
			listData.Add(new ChartData() { Id = i++, Label = "", TipoChart = CHART_TYPE.PIE, Value = RandomNumber(0,100)});
			listData.Add(new ChartData() { Id = i++, Label = "", TipoChart = CHART_TYPE.PIE, Value = RandomNumber(0,100)});
			listData.Add(new ChartData() { Id = i++, Label = "", TipoChart = CHART_TYPE.PIE, Value = RandomNumber(0, 100)});

			//line
			listData.AddRange(CreateMany("Series A", CHART_TYPE.LINE, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series B", CHART_TYPE.LINE, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series C", CHART_TYPE.LINE, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series D", CHART_TYPE.LINE, SERIE_LENGHT));

			//bar			  		   
			listData.AddRange(CreateMany("Series A", CHART_TYPE.BAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series B", CHART_TYPE.BAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series C", CHART_TYPE.BAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series D", CHART_TYPE.BAR, SERIE_LENGHT));

			//pradar		  		   
			listData.AddRange(CreateMany("Series A", CHART_TYPE.RADAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series B", CHART_TYPE.RADAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series C", CHART_TYPE.RADAR, SERIE_LENGHT));
			listData.AddRange(CreateMany("Series D", CHART_TYPE.RADAR, SERIE_LENGHT));

			return listData;			
		}

		private int RandomNumber(int min, int max)
		{
			return new Random().Next(min, max);
		}

		private List<ChartData> CreateMany(string label, CHART_TYPE type, int length)
		{			
			List<ChartData> list = new List<ChartData>();
			for (int i = 0; i < length; i++)
			{
				list.Add(new ChartData()
					{
						Id = RandomNumber(0, Int32.MaxValue),
						Label = label,
						TipoChart = type,
						Value = RandomNumber(0, 100)
					});
			}
			return list;
		}
		
	}
}
