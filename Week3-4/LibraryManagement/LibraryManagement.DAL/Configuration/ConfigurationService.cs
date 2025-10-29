using LibraryManagement.DAL.Data;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.DAL.Configuration
{
    public static class ConfigurationService
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(LibraryContext).Assembly.FullName)).EnableSensitiveDataLogging());

            services.AddScoped<IAuthorRepository, AuthorRepository>()
                .AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
