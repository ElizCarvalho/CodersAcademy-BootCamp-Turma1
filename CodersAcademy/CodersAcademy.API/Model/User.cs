using System;
using System.Collections.Generic;
using System.Linq;

namespace CodersAcademy.API.Model
{
	public class User
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
		public String Photo { get; set; }
		public IList<UserFavoriteMusic> FavoriteMusics { get; set; }

		public void AddFavoriteMusic(Music music)
		{
			this.FavoriteMusics.Add(new UserFavoriteMusic()
			{
				Music = music,
				MusicId = music.Id,
				User = this,
				UserId = this.Id
			});
		}

		public void RemovefavoriteMusic(Music music)
		{
			var favMusic = this.FavoriteMusics.FirstOrDefault(x => x.MusicId == music.Id);

			if (favMusic is null)
				throw new Exception("Não encontrada a música na lista de favoritos");

			this.FavoriteMusics.Remove(favMusic);
		}
	}
}
