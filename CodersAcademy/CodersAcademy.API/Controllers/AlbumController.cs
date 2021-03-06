﻿using AutoMapper;
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
	public class AlbumController : ControllerBase
	{
		private readonly AlbumRepository _repository;
		private readonly IMapper _mapper;

		public AlbumController(AlbumRepository repository, IMapper mapper)
		{
			this._repository = repository;
			this._mapper = mapper;
		}

		/// <summary>
		/// Get a list with all albuns registered.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await this._repository.GetAllAsync());
		}

		/// <summary>
		/// Get an album by Id.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var result = await this._repository.GetByIdAsync(id);

			//Somente C#9 
			return result is not null ? Ok(result) : NotFound();
		}

		/// <summary>
		/// Get the musics of especific album by Id.
		/// </summary>
		[HttpGet("{id}/Musics")]
		public async Task<IActionResult> GetMusicsById(Guid id)
		{
			var result = await this._repository.GetByIdAsync(id);

			//Somente C#9 
			return result is not null ? Ok(result.Musics) : NotFound();
		}


		/// <summary>
		/// Create an Album.
		/// </summary>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Save(AlbumRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState); //400

			var album = this._mapper.Map<Album>(request);

			await this._repository.SaveAsync(album);

			return Created($"/{album.Id}", album); //201
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
