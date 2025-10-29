using LibraryManagement.BLL.Interfaces;
using LibraryManagement.BLL.Services;
using LibraryManagement.DAL.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryManagement.BLL.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessServices(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IBookService, BookService>();
        }
    }
}
