using ApplicationCore.Interfaces;

namespace ApplicationServices.ServiceWrappers;

internal class ServiceWrapper : IServiceWrapper
{
    public ServiceWrapper(IMotHistoryApiService motHistoryApiService)
    {
        MotHistoryApiService = motHistoryApiService;
    }

    public IMotHistoryApiService MotHistoryApiService { get; }
}
