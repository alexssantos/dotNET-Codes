using Microsoft.AspNetCore.Mvc;

namespace core3_ef_15min.Controllers
{
	[Route("api/Empty")]
	[ApiController]
	public class EmptyController : AbstractApiController
	{
		public EmptyController()
		{

		}//constructor

		[HttpGet]
		[Route("catchIp")]
		public ActionResult<string> CatchIp()
		{
			var ip = GetIp(Request);

			return Ok(ip);
		}

	}
}