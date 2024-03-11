using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions
{
    public static class SevicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerService, LoggerManager>();  // AddSingleton static bir öğe varmış gibi düşüneceğiz, logger bir kez üretilecek ve herkes aynı nesneyi kullanacak
       
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>(); // IoC
            services.AddSingleton<LogFilterAttribute>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        // Cors - Kökenler arası kaynak paylaşımı. - API'ye bağlanmak isteyene kanala erişim izni vermeliyiz ki API ya istek atabilsin
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>  // ismi ve builder ifadesi
                    builder.AllowAnyOrigin()   // bütün kökenlere izin ver
                    .AllowAnyMethod()     // bütün metodlara izin ver. GET, POST, PUT ...
                    .AllowAnyHeader()     // Bütün headerlara izin ver
                    .WithExposedHeaders("X-Pagination")
                );
            });
        }

    }
}
