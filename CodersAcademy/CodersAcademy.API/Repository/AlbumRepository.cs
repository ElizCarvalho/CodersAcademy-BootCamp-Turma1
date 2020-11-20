using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Repository
{
	public class AlbumRepository
	{
		private readonly ApiContext _context;

		public AlbumRepository(ApiContext context)
		{
			this._context = context;
		}

		public async Task<IList<Album>> GetAllAsync()
			=> await this._context.Albums.Include(x => x.Musics).ToListAsync();

		public async Task<Album> GetByIdAsync(Guid id)
			=> await this._context.Albums.Where(x => x.Id == id).Include(x => x.Musics).FirstOrDefaultAsync();

		public async Task DeleteAsync(Album model)
		{
			this._context.Remove(model);
			await this._context.SaveChangesAsync();
		}

		public async Task SaveAsync(Album album)
		{
			await this._context.Albums.AddAsync(album);
			await this._context.SaveChangesAsync();
		}
	}
}
