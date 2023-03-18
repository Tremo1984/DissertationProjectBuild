using ApplicationCore.Interfaces;
using ApplicationCore.Models.DataModels;

namespace ApplicationServices.MachineLearning;

internal class PredictionEngineService : IPredictionEngineService
{	
	public List<PredictedFailures> GetMachineLeaningPrediction(VehicleDataModel vehicleDataModel)
	{
        new Prediction.ModelInput();

        var result = Prediction.Predict();

        

        return new List<PredictedFailures>();
	}

}
