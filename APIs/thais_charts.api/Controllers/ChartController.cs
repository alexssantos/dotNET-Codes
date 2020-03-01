using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thais_charts.api.Data;
using thais_charts.api.Models;

namespace thais_charts.api.Controllers
{
	[ApiController]
	[Route("api/chart")]
	public class ChartController : ControllerBase
	{
		public ChartDataDao ChartDataDao { get; set; }

		public ChartController(ChartDataDao chartDataDao)
		{
			ChartDataDao = chartDataDao;
		}

		[HttpGet]
		[Route("")]
		public ActionResult<IEnumerable<string>> Get()
		{
			Dictionary<CHART_TYPE, Dictionary<string, List<ChartData>>> chartData = ChartDataDao.GetAll();
			return Ok(chartData);
		}
	}
}
