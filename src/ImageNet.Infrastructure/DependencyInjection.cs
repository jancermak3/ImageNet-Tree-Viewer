using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ImageNet.Infrastructure.Data;
using ImageNet.Core.Interfaces;
using ImageNet.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ImageNet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImageNetDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ImageNetDbContext).Assembly.FullName)));

            services.AddScoped<IImageNetItemRepository, ImageNetItemRepository>();

            return services;
        }
    }
}