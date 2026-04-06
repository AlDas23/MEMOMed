namespace MEMOMed.Models.DataClasses;

public class HeartMeasurement : Measurement
{
    private int Sys { get; set; }
    private int Dia { get; set; }
    private int HRhythm { get; set; }
    private int IsArrhythmia { get; set; }
}