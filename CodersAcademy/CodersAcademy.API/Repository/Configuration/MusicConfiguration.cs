﻿using CodersAcademy.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodersAcademy.API.Repository.Configuration
{
	public class MusicConfiguration : IEntityTypeConfiguration<Music>
	{
		public void Configure(EntityTypeBuilder<Music> builder)
		{
			builder.ToTable("Musics");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
			builder.Property(x => x.Duration).IsRequired();
		}
	}
}
