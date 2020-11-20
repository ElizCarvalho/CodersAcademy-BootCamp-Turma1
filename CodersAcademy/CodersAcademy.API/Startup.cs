using AutoMapper;
using CodersAcademy.API.Repository;
using CodersAcademy.API.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CodersAcademy.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();

			services.AddDbContext<ApiContext>(c =>
			{
				c.UseSqlite(this.Configuration.GetConnectionString("BootcampConnection"));
			});

			services.AddAutoMapper(typeof(Startup).Assembly);

			services.AddScoped<AlbumRepository>();
			services.AddScoped<MusicRepository>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo() 
				{
					Version = "v1",
					Title = "Coders Academy - Music API",
					Description = "A example ASP.NET Core WebA API created in bootcamp.",
					Contact = new OpenApiContact
					{
						Name = "Elizabeth Carvalho",
						Email = "elizabethcarvalh0@yahoo.com",
					}
				});

				#region Necessario para usar o summary para detalhar os métodos no Swagger
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
				#endregion
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coders Academy Bootcamp"));

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
