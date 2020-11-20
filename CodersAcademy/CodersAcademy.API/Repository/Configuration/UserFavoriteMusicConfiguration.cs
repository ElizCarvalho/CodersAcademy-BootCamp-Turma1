using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodersAcademy.API.Repository.Configuration
{
	public class UserFavoriteMusicConfiguration : IEntityTypeConfiguration<UserFavoriteMusic>
	{
		public void Configure(EntityTypeBuilder<UserFavoriteMusic> builder)
		{
			builder.ToTable("UserFavoriteMusics");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.MusicId);

			builder.HasOne(x => x.Music).WithOne().HasForeignKey<UserFavoriteMusic>(x => x.MusicId);
		}
	}
}
