namespace MEMOMed.Models.DataClasses;

public class HeartMeasurement : Measurement
{
    public int Sys { get; set; }
    public int Dia { get; set; }
    public int HRhythm { get; set; }
    public int IsArrhythmia { get; set; }
}