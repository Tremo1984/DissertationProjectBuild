namespace ApplicationCore.Models.DataModels;

public class VehicleMotHistoryModel
{
    public int Id { get; set; }

    public string Result { get; set; }

    public string Advisories { get; set; }

    public string Falures { get; set; }

    public DateTime DateOfTest { get; set; }

    public virtual ICollection<MotHistoryCommentsModel> MotHistoryCommentsModel { get; set; }
}
