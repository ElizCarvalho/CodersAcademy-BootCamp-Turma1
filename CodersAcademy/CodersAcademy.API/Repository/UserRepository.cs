using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Repository
{
	public class UserRepository
	{
		private readonly ApiContext _context;

		public UserRepository(ApiContext context)
		{
			this._context = context;
		}

		public async Task SaveAsync(User user)
		{
			await this._context.Users.AddAsync(user);
			await this._context.SaveChangesAsync();
		}

		public async Task<User> AuthenticateAsync(string email, string password)
		{
			return await this._context.Users
								.Include(x => x.FavoriteMusics)
								.ThenInclude(x => x.Music)
								.ThenInclude(x => x.Album)
								.Where(x => x.Password == password && x.Email == email)
								.FirstOrDefaultAsync();
		}

		public async Task<IList<User>> GetAllAsync()
			=> await this._context.Users
								.Include(x => x.FavoriteMusics)
								.ThenInclude(x => x.Music)
								.ThenInclude(x => x.Album)
								.ToListAsync();

		public async Task<User> GetByIdAsync(Guid id)
			=> await this._context.Users
								.Include(x => x.FavoriteMusics)
								.ThenInclude(x => x.Music)
								.ThenInclude(x => x.Album).Where(x => x.Id == id).FirstOrDefaultAsync();

		public async Task DeleteAsync(User model)
		{
			this._context.Users.Remove(model);
			await this._context.SaveChangesAsync();
		}

		public async Task UpdateAsync(User user)
		{
			this._context.Users.Update(user);
			await this._context.SaveChangesAsync();
		}
	}
}
