using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using CodersAcademy.API.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;
using System;
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

		[HttpPost("authenticate")]
		public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

			var user = await this._userRepository.AuthenticateAsync(request.Email, password);

			if (user is null)
				return UnprocessableEntity(new
				{
					Message = "Email/Senha inválidos."
				});

			var result = this._mapper.Map<UserResponse>(user);

			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = this._mapper.Map<User>(request);
			user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
			user.Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any";

			await this._userRepository.SaveAsync(user);

			var result = this._mapper.Map<UserResponse>(user);

			return Created($"/{result.Id}", result);
		}

	}
}
