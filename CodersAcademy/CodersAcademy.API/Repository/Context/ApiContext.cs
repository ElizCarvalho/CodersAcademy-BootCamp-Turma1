using CodersAcademy.API.Model;
using CodersAcademy.API.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodersAcademy.API.Repository.Context
{
	public class ApiContext : DbContext
	{

		public DbSet<Album> Albums{ get; set; }

		public ApiContext(DbContextOptions<ApiContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AlbumConfiguration());
			modelBuilder.ApplyConfiguration(new MusicConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			ILoggerFactory Logger = LoggerFactory.Create(x => x.AddConsole());
			optionsBuilder.UseLoggerFactory(Logger);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
