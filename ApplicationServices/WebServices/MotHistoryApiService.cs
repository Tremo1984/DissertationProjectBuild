using ApplicationCore.Interfaces;
using ApplicationCore.Models.DataModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApplicationServices.WebServices;

internal class MotHistoryApiService : IMotHistoryApiService
{
    private readonly IAsyncRepository<VehicleDataModel> _repository;
    private readonly IConfiguration _configuration;
    private IConfigurationSection _apiConfiguration => _configuration.GetSection("MOTAPI");

    public MotHistoryApiService(IAsyncRepository<VehicleDataModel> repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    private HttpClient HttpClient()
    {
        var httpClient = new HttpClient();
        var apiData = _apiConfiguration.GetChildren();
        var key = apiData.Where(apiData => apiData.Key != null && apiData.Key.Equals("ApiKey")).First();
        var address = apiData.Where(apiData => apiData.Key != null && !apiData.Key.Equals("EndPoint")).First();

        if (string.IsNullOrEmpty(key.Value) || string.IsNullOrEmpty(address.Value))
        {
            throw new ArgumentNullException();
        }
        
        httpClient.BaseAddress = new Uri(address.Value + "/" + key.Value);
        
        return httpClient;
    }

    public async Task<VehicleDataModel> GetVehicleData(string registration)
    {
        if (string.IsNullOrEmpty(registration))
        {
            throw new ArgumentNullException();
        }

        VehicleDataModel vehicleDataModel = null;

        var GetData = await HttpClient().GetAsync(registration);

        if (GetData.IsSuccessStatusCode)
        {
            var json = await GetData.Content.ReadAsStringAsync();
            vehicleDataModel = JsonConvert.DeserializeObject<VehicleDataModel>(json);

            await _repository.CreateAsync(vehicleDataModel);
        }

        return vehicleDataModel != null ? vehicleDataModel : new VehicleDataModel(); 
    }

    public async Task BulkMotDataDownload()
    {
        ICollection<VehicleDataModel> vehicleDataModel = null;
        var startDate = DateTime.UtcNow.AddYears(-13);
        var endDate = DateTime.UtcNow.AddYears(-12);
        
        var getBulkData = await HttpClient().GetAsync(startDate + "," + endDate);

        if (getBulkData.IsSuccessStatusCode)
        {
            var json = await getBulkData.Content.ReadAsStringAsync();
            vehicleDataModel = JsonConvert.DeserializeObject<ICollection<VehicleDataModel>>(json);

            foreach (var mot in vehicleDataModel)
            {
                await _repository.CreateAsync(mot);
            }
        }

        await Task.CompletedTask;
    }
}
