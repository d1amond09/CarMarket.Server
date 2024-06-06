using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.Server.Controllers
{
	[Route("api")]
	[ApiController]
	public class RootController(LinkGenerator linkGenerator) : ControllerBase
	{
		private readonly LinkGenerator _linkGenerator = linkGenerator;

		[HttpGet(Name = "GetRoot")]
		public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
		{
			if (mediaType.Contains("application/vnd.codemaze.apiroot"))
			{
				var list = new List<Link> {
					new() {
						Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {}),
						Rel = "self",
						Method = "GET" 
					},
					new() {
						Href = _linkGenerator.GetUriByName(HttpContext, "GetCarShops", new {}),
						Rel = "carShops",
						Method = "GET" 
					},
					new() {
						Href = _linkGenerator.GetUriByName(HttpContext, "CreateCarShop", new {}),
						Rel = "create_carShop",
						Method = "POST" 
					}
				};
				return Ok(list);
			}
			return NoContent();
		}

	}
}
