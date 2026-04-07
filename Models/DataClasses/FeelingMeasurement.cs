namespace MEMOMed.Models.DataClasses;

public class FeelingMeasurement : Measurement
{
    public string? Medication { get; set; }
    public string? Feeling { get; set; }
}