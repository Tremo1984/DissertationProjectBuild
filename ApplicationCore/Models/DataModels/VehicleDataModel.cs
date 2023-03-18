namespace ApplicationCore.Models.DataModels;

public class VehicleDataModel
{
    public int Id { get; set; }
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string FuelType { get; set; }
    public string Mileage { get; set; }
    public string NumberOfDoors { get; set; }
    public string BodyStyle { get; set; }

    public virtual ICollection<VehicleMotHistoryModel> VehicleMotHistoryModel { get; set; }
}
