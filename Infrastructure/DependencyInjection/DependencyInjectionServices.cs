using Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using Infrastructure.Repository;

namespace Infrastructure.DependencyInjection;

public static partial class DependencyInjectionServices
{
    public static IServiceCollection InjectInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AppContextConnection")
            ?? throw new InvalidOperationException("Connection string 'AppContextConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        return services;
    }
}
