using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace core3_ef_15min.Controllers
{
	//[ApiExceptionFilter]	: ExceptionFilterAttribute
	[ApiController]
	public class AbstractApiController : ControllerBase
	{
		protected string GetIp(HttpRequest requst)
		{
			var requestToUse = requst ?? this.Request;
			IPAddress remoteIpAddress = requestToUse.HttpContext.Connection.RemoteIpAddress;
			if (IPAddress.IsLoopback(remoteIpAddress))
			{
				// Gets the IP loopback address and converts it to a string.
				string IpAddressString = IPAddress.Loopback.ToString();
				Console.WriteLine("Loopback IP address : " + IpAddressString);
				return IpAddressString;
			}

			return remoteIpAddress.ToString();

		}
	}
}