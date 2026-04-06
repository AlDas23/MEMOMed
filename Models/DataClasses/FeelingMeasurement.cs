namespace MEMOMed.Models.DataClasses;

public class FeelingMeasurement : Measurement
{
    private string? Medication { get; set; }
    private string? Feeling { get; set; }
}