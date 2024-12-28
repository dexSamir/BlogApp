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
			return services;
		}
	}
}

