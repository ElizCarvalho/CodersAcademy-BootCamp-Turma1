using CodersAcademy.API.Model;
using CodersAcademy.API.ViewModel.Request;

namespace CodersAcademy.API.ViewModel.ConfigurationMapper
{
	public class ApiConfigurationMapper : AutoMapper.Profile
	{
		public ApiConfigurationMapper()
		{
			CreateMap<MusicRequest, Music>();
			CreateMap<AlbumRequest, Album>();
		}
	}
}
