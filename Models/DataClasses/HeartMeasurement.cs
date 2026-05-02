namespace MEMOMed.Models.DataClasses;

public class HeartMeasurement : Measurement
{
    public int Sys { get; set; }
    public int Dia { get; set; }
    public int HRhythm { get; set; }
    public bool IsArrhythmia { get; set; }

    public HeartMeasurement(int id, int personId, string date, int sys, int dia, int hRhythm, bool isArrhythmia)
    {
        Id = id;
        PersonId = personId;
        Date = date;
        Sys = sys;
        Dia = dia;
        HRhythm = hRhythm;
        IsArrhythmia = isArrhythmia;
    }

    public HeartMeasurement(int personId, string date, int sys, int dia, int hRhythm, bool isArrhythmia)
    {
        Id = null;
        PersonId = personId;
        Date = date;
        Sys = sys;
        Dia = dia;
        HRhythm = hRhythm;
        IsArrhythmia = isArrhythmia;
    }
}