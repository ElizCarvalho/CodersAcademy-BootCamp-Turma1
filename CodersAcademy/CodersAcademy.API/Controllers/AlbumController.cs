using CodersAcademy.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumController : ControllerBase
	{
		private AlbumRepository Repository { get; init; }

		public AlbumController(AlbumRepository repository)
		{
			this.Repository = repository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAlguns()
		{
			return Ok(await this.Repository.GetAlbumsAsync());
		}
	}
}
