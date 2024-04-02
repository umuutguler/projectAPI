using System.Text;
using AspNetCoreRateLimit;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
                 /*  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)*/);
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

        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<ProductDto>, DataShaper<ProductDto>>();
        }

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>() // Limit Kuralları listesi
    {
        new RateLimitRule() // Limit Kuralları
        {
            Endpoint = "*", // Endpointlerin tamamını kapsasın
            Limit = 60, // Dakikada 3 istek alalım
            Period = "1m" // 1 dakika
            // Yeni Kurallar Eklenebilir
        }
    };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules; // Yukarıda belirttiğimiz kualları genel kurallar olarak konfigüre etmiş olduk
            });

            // IoC Kayıtları
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();  // AddSingleton -> Tek Bir Nesne Oluşması Yeter
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }


        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opts =>  // User ve User rolü - IdentityUser ve IdentityRole - User ifadesini IdentityUser clasından kalıtarak ürettiğimiz için IdentityUser yerine User yazabiliyoruz
            {
                opts.Password.RequireDigit = true;   // Şifrede rakan istiyor muyuz
                opts.Password.RequireLowercase = false; // küçük harf istiyor muyuz
                opts.Password.RequireUppercase = false; // büyük harf istiyor muyuz
                opts.Password.RequireNonAlphanumeric = false; // Alfanumarik istiyor muyuz
                opts.Password.RequiredLength = 6;    // Şifre uzunluğu

                opts.User.RequireUniqueEmail = true;      // Bir email bir defa kullanılsın
            })
                .AddEntityFrameworkStores<RepositoryContext>() // 
                .AddDefaultTokenProviders();  // Json ve Token Kullanmak için
        }

        public static void ConfigureJWT(this IServiceCollection services,
    IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true,
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) 
                }
            );
        }

        public static void ConfigurePayment(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentManager>();
        }

    }
}
