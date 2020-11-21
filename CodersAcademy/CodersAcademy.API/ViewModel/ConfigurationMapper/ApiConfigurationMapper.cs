using CodersAcademy.API.Model;
using CodersAcademy.API.ViewModel.Request;
using CodersAcademy.API.ViewModel.Response;

namespace CodersAcademy.API.ViewModel.ConfigurationMapper
{
	public class ApiConfigurationMapper : AutoMapper.Profile
	{
		public ApiConfigurationMapper()
		{
			CreateMap<MusicRequest, Music>();
			CreateMap<AlbumRequest, Album>();
			CreateMap<RegisterRequest, User>();
			CreateMap<User, UserResponse>();
			CreateMap<UserFavoriteMusic, FavoriteMusicResponse>()
				.ForMember(x => x.AlbumId, m => m.MapFrom(f => f.Music.Album.Id))
				.ForMember(x => x.AlbumName, m => m.MapFrom(f => f.Music.Album.Name))
				.ForMember(x => x.Backdrop, m => m.MapFrom(f => f.Music.Album.Backdrop))
				.ForMember(x => x.Band, m => m.MapFrom(f => f.Music.Album.Band))
				.ForMember(x => x.Duration, m => m.MapFrom(f => f.Music.Duration))
				.ForMember(x => x.Id, m => m.MapFrom(f => f.Id))
				.ForMember(x => x.MusicId, m => m.MapFrom(f => f.MusicId))
				.ForMember(x => x.Name, m => m.MapFrom(f => f.Music.Name));
				
		}
	}
}
