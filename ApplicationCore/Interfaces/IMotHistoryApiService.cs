using ApplicationCore.Models.DataModels;

namespace ApplicationCore.Interfaces;

public interface IMotHistoryApiService
{
    Task<VehicleDataModel> GetVehicleData(string registration);
}
