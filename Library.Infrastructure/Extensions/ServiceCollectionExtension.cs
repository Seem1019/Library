using Library.Core.CustomEntities;
using Library.Core.Interfaces;
using Library.Core.Services;
using Library.Infrastructure.Data;
using Library.Infrastructure.Interfaces;
using Library.Infrastructure.Options;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TravelLibraryContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("SocialMedia"))
           );

            return services;
        }
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            return services;
        }
    }
}
