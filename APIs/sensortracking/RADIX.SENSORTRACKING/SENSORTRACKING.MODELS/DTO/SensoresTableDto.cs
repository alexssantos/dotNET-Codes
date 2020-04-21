using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SENSORTRACKING.MODELS.DTO

{
	[DataContract]
	public class SensoresTableDto
	{
		[DataMember(Name = "itens")]
		public List<SensorModel> Itens { get; set; }

		[DataMember(Name = "primeirpItemIndex")]
		public int PrimeirpItemIndex { get; set; }

		[DataMember(Name = "ultimoItemIndex")]
		public int UltimoItemIndex { get; set; }

		[DataMember(Name = "itensTotal")]
		public int ItensTotal { get; set; }

		[DataMember(Name = "totalPaginas")]
		public int TotalPaginas { get; set; }

		[DataMember(Name = "itensPorPagina")]
		public int ItensPorPagina { get; set; }

		[DataMember(Name = "totalItensPagina")]
		public int TotalItensPagina { get; set; }

		[DataMember(Name = "paginaAtual")]
		public int PaginaAtual { get; set; }
	}
}
