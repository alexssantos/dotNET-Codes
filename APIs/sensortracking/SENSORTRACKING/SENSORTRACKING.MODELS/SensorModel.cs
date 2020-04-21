using System;
using System.ComponentModel.DataAnnotations;

namespace SENSORTRACKING.MODELS
{
	public class SensorModel : IEquatable<SensorModel>
	{
		[Key]
		public long Id { get; set; }
		public string Pais { get; set; }
		public string Regiao { get; set; }
		public string Nome { get; set; }
		public string Valor { get; set; }
		public int? ValorNumero { get; set; }
		public DateTime Data { get; set; }
		public DateTime DataHoraInsert { get; set; }


		public override bool Equals(object obj)
		{
			return Equals(obj as SensorModel);
		}

		public bool Equals(SensorModel other)
		{
			return other != null &&
				   Pais == other.Pais &&
				   Regiao == other.Regiao &&
				   Nome == other.Nome;
		}

		public static SensorModel Build(string tag, long timestampMili, string valor)
		{
			string[] tagInArray = tag.Split(".");
			string paisTag = tagInArray[0];
			string regiaoTag = tagInArray[1];
			string sensorNome = tagInArray[2];

			double timestampSeg = timestampMili / 1000;
			double timestampSegTrunc = Math.Round(timestampSeg, 0);
			DateTime dt = DateTime.UnixEpoch.AddSeconds(timestampSegTrunc).ToUniversalTime();

			int.TryParse(valor, out int valorNumero);


			SensorModel model = new SensorModel();
			model.Nome = sensorNome;
			model.Pais = paisTag;
			model.Regiao = regiaoTag;
			model.Valor = valor;
			model.ValorNumero = (valorNumero == 0) ? null : (int?)valorNumero;
			model.Data = dt;
			model.DataHoraInsert = DateTime.Now;

			return model;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, Pais, Regiao, Nome, Valor, ValorNumero, Data, DataHoraInsert);
		}
	}
}
