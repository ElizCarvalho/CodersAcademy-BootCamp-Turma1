using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Repository
{
	public class MusicRepository
	{
		private readonly ApiContext _context;

		public MusicRepository(ApiContext context)
		{
			this._context = context;
		}

		public async Task<IList<Music>> GetAllAsync()
			=> await this._context.Musics.ToListAsync();

		public async Task<Music> GetByIdAsync(Guid id)
			=> await this._context.Musics.Where(x => x.Id == id).FirstOrDefaultAsync();

		public async Task DeleteAsync(Music model)
		{
			this._context.Remove(model);
			await this._context.SaveChangesAsync();
		}

		public async Task SaveAsync(Music music)
		{
			await this._context.Musics.AddAsync(music);
			await this._context.SaveChangesAsync();
		}
	}
}
