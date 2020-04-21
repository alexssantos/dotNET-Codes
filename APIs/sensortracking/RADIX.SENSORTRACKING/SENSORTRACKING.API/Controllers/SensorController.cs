using Microsoft.AspNetCore.Mvc;
using SENSORTRACKING.MODELS;
using SENSORTRACKING.MODELS.DTO;
using SENSORTRACKING.SERVICES;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SENSORTRACKING.API.Controllers
{
	[ApiController]
	[Route("api/sensor")]
	public class SensorController : ControllerBase
	{
		[HttpGet]
		public ActionResult<SensorInputDto> Get([FromServices]SensorService sensorService)
		{
			IEnumerable<SensorModel> sensores = sensorService.GetSensores();

			return Ok(sensores);
		}

		[HttpGet("total")]
		public ActionResult<int> GetTotal([FromServices]SensorService sensorService)
		{
			int total = sensorService.GetTotalNoBanco();

			return Ok(total);
		}

		[HttpGet("totais")]
		public ActionResult GetTotais([FromServices]SensorService sensorService)
		{
			var totais = sensorService.GetTotaisPorRegiao();

			return Ok(totais);
		}

		[HttpGet("paginado")]
		public ActionResult<SensoresTableDto> GetTotais(
			[FromServices]SensorService sensorService,
			[FromQuery] int pagina,
			[FromQuery] int paginaTamanho,
			[FromQuery] string busca,
			[FromQuery] string buscaCorrente)
		{
			SensoresTableDto lista = sensorService.GetSensoresPaginado(buscaCorrente, busca, paginaTamanho, pagina);

			return Ok(lista);
		}

		[HttpPost]
		public ActionResult<SensorDto> Post([FromServices]SensorService service, [FromBody]SensorInputDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			SensorDto dtoResultado = service.SaveSensorAsync(model);
			return Ok(dtoResultado);
		}

		[HttpGet("graficoEventos")]
		public ActionResult GetGraficoEventos([FromServices]SensorService sensorService)
		{
			var graficoDados = sensorService.GetTotalEventosComNumero();

			return Ok(graficoDados);
		}


		// ============================================
		// TESTE
		// ============================================

		// GET api/sensor/teste
		[HttpGet("teste")]
		public ActionResult<string> Teste()
		{
			return Ok("API It's ON!");
		}
	}
}
