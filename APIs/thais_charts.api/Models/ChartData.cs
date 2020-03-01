using System.ComponentModel.DataAnnotations;

namespace thais_charts.api.Models
{
	public class ChartData
	{
		[Key]
		public long Id { get; set; }

		public int Value { get; set; }

		public string Label { get; set; }

		public CHART_TYPE TipoChart { get; set; }
	}

	public enum CHART_TYPE
	{
		PIE = 1,
		LINE = 2,
		BAR = 3,
		RADAR = 4
	}
}
