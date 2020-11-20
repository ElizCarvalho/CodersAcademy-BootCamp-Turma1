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
		//o init permite atribuir o valor uma unica vez (como na criação do objeto), ​depois disso não pode mais ser alterado
		private ApiContext Context { get; init; }

		public AlbumRepository(ApiContext context)
		{
			this.Context = context;
		}

		public async Task<IList<Album>> GetAlbumsAsync()
			=> await this.Context.Albums.Include(x => x.Musics).ToListAsync();

		public async Task<Album> GetAlbumByIdAsync(Guid id)
			=> await this.Context.Albums.Where(x => x.Id == id).Include(x => x.Musics).FirstOrDefaultAsync();

		public async Task DeleteAsync(Album model)
		{
			this.Context.Remove(model);
			await this.Context.SaveChangesAsync();
		}

		public async Task SaveAsync(Album album)
		{
			await this.Context.Albums.AddAsync(album);
			await this.Context.SaveChangesAsync();
		}
	}
}
