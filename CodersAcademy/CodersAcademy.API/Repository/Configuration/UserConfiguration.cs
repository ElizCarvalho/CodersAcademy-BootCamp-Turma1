﻿using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodersAcademy.API.Repository.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Photo).IsRequired().HasMaxLength(200);

			builder.HasMany(x => x.FavoriteMusics).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
