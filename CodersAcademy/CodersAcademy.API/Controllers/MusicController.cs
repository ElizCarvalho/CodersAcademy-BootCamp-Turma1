using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MusicController : ControllerBase
	{
		private readonly MusicRepository _repository;
		private readonly IMapper _mapper;

		public MusicController(MusicRepository repository, IMapper mapper)
		{
			this._repository = repository;
			this._mapper = mapper;
		}

		/// <summary>
		/// Get a list with all musics registered.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await this._repository.GetAllAsync());
		}

		/// <summary>
		/// Get an music by Id.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await this._repository.GetByIdAsync(id);
			return result is not null ? Ok(result) : NotFound();
		}

		/// <summary>
		/// Create an Album.
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Save(MusicRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState); //400

			var music = this._mapper.Map<Music>(request);

			await this._repository.SaveAsync(music);

			return Created($"/{music.Id}", music); //201
		}

		/// <summary>
		/// Delete a specific Album by Id.
		/// </summary>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await this._repository.GetByIdAsync(id);

			if (result == null)
				return NotFound(); //404

			await this._repository.DeleteAsync(result);

			return NoContent(); //204
		}
	}
}
