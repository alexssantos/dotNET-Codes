using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SENSORTRACKING.MODELS.DTO
{
	[DataContract]
	public class SensorInputDto
	{
		[DataMember(Name = "timestamp")]
		public long Timestamp { get; set; }

		[DataMember(Name = "tag")]
		public string Tag { get; set; }

		[DataMember(Name = "valor")]
		[Required(ErrorMessage = "Este campo é obrigatorio")]
		public string Valor { get; set; }
	}
}
