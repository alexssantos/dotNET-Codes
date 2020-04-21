using System.Runtime.Serialization;

namespace SENSORTRACKING.MODELS.DTO
{
	[DataContract]
	public class SensorDto
	{
		[DataMember(Name = "pais")]
		public string Pais { get; set; }

		[DataMember(Name = "regiao")]
		public string Regiao { get; set; }

		[DataMember(Name = "sensorNome")]
		public string SensorNome { get; set; }

		[DataMember(Name = "valor")]
		public string Valor { get; set; }
	}
}
