using ApplicationCore.Models.DataModels;

namespace ApplicationCore.Interfaces;

public interface IPredictionEngineService
{
    List<PredictedFailures> GetMachineLeaningPrediction(VehicleDataModel vehicleDataModel);
}
