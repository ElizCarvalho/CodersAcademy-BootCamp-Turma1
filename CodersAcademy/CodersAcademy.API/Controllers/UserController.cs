using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using CodersAcademy.API.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserRepository _userRepository;
		private readonly AlbumRepository _albumRepository;
		private readonly IMapper _mapper;

		public UserController(UserRepository userRepository, AlbumRepository albumRepository, IMapper mapper)
		{
			this._userRepository = userRepository;
			this._albumRepository = albumRepository;
			this._mapper = mapper;
		}


		/// <summary>
		/// Get an user by Id.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var user = await this._userRepository.GetByIdAsync(id);
			if (user is null)
				return NotFound();

			var result = this._mapper.Map<UserResponse>(user);
			return Ok(result);
		}

		/// <summary>
		/// Get a list with all users registered.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await this._userRepository.GetAllAsync();
			var result = this._mapper.Map<List<UserResponse>>(users);
			return Ok(result);
		}

		/// <summary>
		/// Authenticate an user by email and password.
		/// </summary>
		[HttpPost("authenticate")]
		public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

			var user = await this._userRepository.AuthenticateAsync(request.Email, password);

			if (user is null)
				return Unauthorized(new
				{
					Message = "Email/Senha inválidos."
				});

			var result = this._mapper.Map<UserResponse>(user);

			return Ok(result);
		}

		/// <summary>
		/// Register an user.
		/// </summary>
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = this._mapper.Map<User>(request);
			user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
			user.Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any";

			//Grava o usuário na base
			await this._userRepository.SaveAsync(user);

			var result = this._mapper.Map<UserResponse>(user);

			return Created($"/{result.Id}", result);
		}

		/// <summary>
		/// Remove an user by id.
		/// </summary>
		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveUser(Guid id)
		{
			var user = await this._userRepository.GetByIdAsync(id);

			if (user is null)
				return UnprocessableEntity(new { Message = "User not found." });

			await this._userRepository.DeleteAsync(user);

			return NoContent();
		}

		/// <summary>
		/// Add an favorite music for user by userId and musicId 
		/// </summary>
		[HttpPost("{id}/favorite-music/{musicId}")]
		public async Task<IActionResult> SaveUserFavoriteMusic(Guid id, Guid musicId)
		{
			var music = await this._albumRepository.GetMusicByIdAsync(musicId);
			var user = await this._userRepository.GetByIdAsync(id);

			if (music is null)
				return UnprocessableEntity(new { Message = "Music not found." });

			if (user is null)
				return UnprocessableEntity(new { Message = "User not found." });

			user.AddFavoriteMusic(music);

			await this._userRepository.UpdateAsync(user);

			return Ok();
		}

		/// <summary>
		/// Remove an favorite music for user by userId and musicId 
		/// </summary>
		[HttpDelete("{id}/favorite-music/{musicId}")]
		public async Task<IActionResult> RemoveUserFavoriteMusic(Guid id, Guid musicId)
		{
			var music = await this._albumRepository.GetMusicByIdAsync(musicId);
			var user = await this._userRepository.GetByIdAsync(id);

			if (music is null)
				return UnprocessableEntity(new { Message = "Music not found." });

			if (user is null)
				return UnprocessableEntity(new { Message = "User not found." });

			user.RemoveFavoriteMusic(music);

			await this._userRepository.UpdateAsync(user);

			return Ok();
		}
	}
}
