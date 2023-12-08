using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StokKontrolProje.Repositories.Abstract;
using StokKontrolProje.Repositories.Concrete;
using StokKontrolProje.Repositories.Context;
using StokKontrolProje.Services.Abstract;
using StokKontrolProje.Services.Concrete;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace StokKontrolProje.WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddControllers().AddNewtonsoftJson(option =>
			{
				option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});


			builder.Services.AddDbContext<StokKontrolContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn"));
			});

			builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericManager<>));
			builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "insert JWT Token",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});


			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
				(
				x =>
				{
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuers = new string[] { builder.Configuration["JWT:Issuer"] },
						ValidAudiences = new string[] { builder.Configuration["JWT:Audience"] },
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
						ClockSkew = TimeSpan.FromMinutes(30),
					};
				});
			var app = builder.Build();
			app.UseCors(options=>options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
			

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}