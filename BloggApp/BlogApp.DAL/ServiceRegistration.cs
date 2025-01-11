using System;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DAL
{
	public static class ServiceRegistration 
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IBlogRepository, BlogRepository>();

            return services;
		}
        //public static IServiceCollection AddServices(this IServiceCollection services)
        //{
        //    services.AddScoped<, UserService>();

        //    return services;
        //}

    }
}

