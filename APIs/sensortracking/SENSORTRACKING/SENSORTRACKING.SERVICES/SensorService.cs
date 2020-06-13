using ReflectionIT.Mvc.Paging;
using SENSORTRACKING.DAO;
using SENSORTRACKING.MODELS;
using SENSORTRACKING.MODELS.DTO;
using System;
using System.Collections.Generic;

namespace SENSORTRACKING.SERVICES
{
	public class SensorService
	{

		private readonly SensorDao _sensorDao;

		public SensorService(SensorDao sensorDao)
		{
			_sensorDao = sensorDao;
		}

		public IEnumerable<SensorModel> GetSensores()
		{
			return _sensorDao.GetSensores();
		}

		public int GetTotalNoBanco()
		{
			return _sensorDao.GetTotal();
		}

		public Dictionary<string, Dictionary<string, int>> GetTotaisPorRegiao()
		{
			Dictionary<string, Dictionary<string, int>> totaisDict = _sensorDao.GetTotaisPorRegiao();

			return totaisDict;
		}

		public SensoresTableDto GetSensoresPaginado(string buscaCorrente, string busca, int paginaTamanho, int paginaIndex)
		{
			PagingList<SensorModel> list = _sensorDao.GetSensoresPaginado(buscaCorrente, busca, paginaTamanho, paginaIndex);

			SensoresTableDto dto = new SensoresTableDto();
			dto.ItensPorPagina = paginaTamanho;
			dto.TotalItensPagina = list.Capacity;
			dto.PaginaAtual = list.PageIndex;
			dto.PrimeirpItemIndex = (dto.PaginaAtual - 1) * dto.ItensPorPagina + 1;
			dto.UltimoItemIndex = (dto.PaginaAtual - 1) * dto.ItensPorPagina + dto.TotalItensPagina;
			dto.ItensTotal = list.TotalRecordCount;
			dto.TotalPaginas = list.PageCount;
			dto.Itens = list;

			return dto;
		}

		public SensorDto SaveSensorAsync(SensorInputDto dto)
		{
			var model = SensorModel.Build(dto.Tag, dto.Timestamp, dto.Valor);
			_sensorDao.Save(model);

			SensorDto novoDto = new SensorDto();
			novoDto.SensorNome = model.Nome;
			novoDto.Pais = model.Pais;
			novoDto.Regiao = model.Regiao;
			novoDto.Valor = model.Valor;

			return novoDto;
		}

		public List<GraficoEventosDto> GetTotalEventosComNumero()
		{
			int atrasoEmSeg = 15;
			int qtdPontos = 30;
			DateTime momentoCondicao = DateTime.UtcNow.AddSeconds(-atrasoEmSeg);
			string formatDate = "s";

			List<GraficoEventosDto> totaisList = _sensorDao.GetTotalEventosPorSeg(momentoCondicao, qtdPontos, formatDate);

			return totaisList;
		}
	}
}
