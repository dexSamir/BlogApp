using BlogApp.DAL;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BlogApp.BL.Services.Implements;
using BlogApp.BL.Services.Interfaces;
using BlogApp.BL;
using BlogApp.API;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace TogrulAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options=>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddAuth(builder.Configuration); 
            builder.Services.AddJwtOptions(builder.Configuration);

            builder.Services.AddDbContext<BlogAppDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddFluentValidation();
            builder.Services.AddAutoMapper();
             
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}