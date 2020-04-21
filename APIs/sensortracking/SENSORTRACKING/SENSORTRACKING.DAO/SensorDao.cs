using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SENSORTRACKING.MODELS;
using SENSORTRACKING.MODELS.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SENSORTRACKING.DAO
{
	public class SensorDao
	{

		private readonly SensorTrackingDbContext _context;

		public SensorDao(SensorTrackingDbContext context)
		{
			_context = context;
		}

		public IEnumerable<SensorModel> GetSensores()
		{
			return _context.Sensores.ToList();
		}

		public int GetTotal()
		{
			return _context.Sensores.Count();
		}

		public Dictionary<string, Dictionary<string, int>> GetTotaisPorRegiao()
		{
			/*EXCEPTION: Momery Leak
				reference to constant expression of 'Microsoft.EntityFrameworkCore.Metadata.IPropertyBase' 
				which is being passed as argument to method 'TryReadValue'.This could potentially cause 
				memory leak.Consider assigning this constant to local variable and using the variable in the query instead...
				https://dzone.com/articles/investigating-a-memory-leak-in-entity-framework-co
			*/

			List<SensorModel> groupQuery = _context.Sensores.ToList();

			Dictionary<string, Dictionary<string, int>> totaisDict = groupQuery
				.GroupBy(x => x.Regiao)
				.ToDictionary(
					kvp => kvp.Key,
					kvp => kvp.GroupBy(x => x.Nome)
					.ToDictionary(
						kvp2 => kvp2.Key,
						kvp2 => kvp2.Count()
				));


			return totaisDict;
		}

		public PagingList<SensorModel> GetSensoresPaginado(string buscaCorrente, string busca, int paginaTamanho, int? paginaIndex)
		{
			IQueryable<SensorModel> query = _context.Sensores.AsNoTracking();

			// nova busca
			if (busca != null)
			{
				paginaIndex = 1;
			}
			// com busca antiga. possivelmente alterando pagina.
			else
			{
				busca = buscaCorrente;
			}

			if (!string.IsNullOrEmpty(busca))
			{
				query = query.Where(x =>
					x.Nome.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
					x.Pais.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
					x.Regiao.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
					x.Valor.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
					x.Regiao.Contains(busca, StringComparison.OrdinalIgnoreCase));
			}

			int paginaAtual = (paginaIndex ?? 1);
			PagingList<SensorModel> lista = PagingList.Create(query, paginaTamanho, paginaAtual);

			return lista;
		}

		public SensorModel Save(SensorModel model)
		{
			_context.Sensores.Add(model);
			_context.SaveChangesAsync();

			return model;
		}

		public List<GraficoEventosDto> GetTotalEventosPorSeg(DateTime momentoCondicao, int qtdPontos, string formatDate)
		{
			List<GraficoEventosDto> totaisEventos = _context.Sensores
				.Where(x => (x.Data <= momentoCondicao) && (x.ValorNumero != null))
				.GroupBy(x => x.Data)
				.OrderByDescending(x => x.Key)
				.Take(qtdPontos)
				.Select(x => new GraficoEventosDto()
				{
					DataEventos = x.Key.ToString(formatDate, CultureInfo.InvariantCulture),
					TotalEventos = x.Count()
				})
				.ToList();

			totaisEventos.Reverse();
			return totaisEventos;
		}
	}
}
