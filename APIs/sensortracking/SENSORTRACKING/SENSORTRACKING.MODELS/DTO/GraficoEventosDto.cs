using System.Runtime.Serialization;

namespace SENSORTRACKING.MODELS.DTO
{
	[DataContract]
	public class GraficoEventosDto
	{
		[DataMember(Name = "dataEventos")]
		public string DataEventos { get; set; }
		[DataMember(Name = "totalEventos")]
		public int TotalEventos { get; set; }


	}
}
