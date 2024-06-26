﻿using UMS.Contracts;
using Microsoft.EntityFrameworkCore;
using UMS.Repository;
using UMS.Service;
using UMS.LoggerService;
using UMS.Service.Contracts;
using Marvin.Cache.Headers;

namespace UMS.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });
    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });
    public static void ConfigureLoggerService(this IServiceCollection services) =>
 services.AddSingleton<ILoggerManager, LoggerManager>();
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
     services.AddScoped<IRepositoryManager, RepositoryManager>();
    public static void ConfigureServiceManager(this IServiceCollection services) =>
services.AddScoped<IServiceManager, ServiceManager>();
    public static void ConfigureSqlContext(this IServiceCollection services,
IConfiguration configuration) =>
services.AddDbContext<RepositoryContext>(opts =>
opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
    public static void ConfigureResponseCaching(this IServiceCollection services) =>
services.AddResponseCaching();
    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
services.AddHttpCacheHeaders((expirationOpt) =>
    {
        expirationOpt.MaxAge = 65;
        //expirationOpt.CacheLocation = CacheLocation.Private;
    },
    (validationOpt) =>
    {
        validationOpt.MustRevalidate = true;
    });


}
