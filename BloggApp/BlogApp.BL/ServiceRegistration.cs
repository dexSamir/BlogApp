using System;
using BlogApp.BL.ExternalServices.Implements;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Implements;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        return services;
    }
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));
        return services; 
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}

