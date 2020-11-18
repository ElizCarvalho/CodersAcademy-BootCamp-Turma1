using Microsoft.AspNetCore.Mvc;

namespace CodersAcademy.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetAlguns()
		{
			return Ok(new { Message = "Primeira API criada." });
		}
	}
}
