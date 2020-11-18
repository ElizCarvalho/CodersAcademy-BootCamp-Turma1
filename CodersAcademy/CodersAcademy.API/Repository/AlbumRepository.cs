using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodersAcademy.API.Repository
{
	public class AlbumRepository
	{
		//o init permite atribuir o valor uma unica vez (como na criação do objeto), ​depois disso não pode mais ser alterado
		private MusicContext Context { get; init; }

		public AlbumRepository(MusicContext context)
		{
			this.Context = context;
		}

		public async Task<IList<Album>> GetAlbumsAsync()
			=> await this.Context.Albums.ToListAsync();
	}
}
