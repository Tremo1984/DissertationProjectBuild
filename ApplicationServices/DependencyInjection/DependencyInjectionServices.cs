using ApplicationCore.Interfaces;
using ApplicationServices.MachineLearning;
using ApplicationServices.ServiceWrappers;
using ApplicationServices.WebServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore.DependencyInjection;

public static partial class DependencyInjectionServices
{
    public static IServiceCollection InjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMotHistoryApiService, MotHistoryApiService>();
        services.AddScoped<IServiceWrapper, ServiceWrapper>();
        services.AddScoped<IPredictionEngineService, PredictionEngineService>();

        return services;
    }
}
