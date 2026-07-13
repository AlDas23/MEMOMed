namespace MEMOMed.Models.DataClasses;

public class HeartMeasurement : Measurement
{
    public int Sys { get; set; }
    public int Dia { get; set; }
    public int HRhythm { get; set; }
    public string? Feeling { get; set; }
    public string? Medication { get; set; }
    public bool IsArrhythmia { get; set; }

    public HeartMeasurement(int id, int personId, string date, int sys, int dia, int hRhythm, string feeling,
        string medication, bool isArrhythmia)
    {
        Id = id;
        PersonId = personId;
        Date = date;
        Sys = sys;
        Dia = dia;
        HRhythm = hRhythm;
        Feeling = feeling;
        Medication = medication;
        IsArrhythmia = isArrhythmia;
    }

    public HeartMeasurement(int personId, string date, int sys, int dia, int hRhythm, string? feeling, string? medication,
        bool isArrhythmia)
    {
        Id = null;
        PersonId = personId;
        Date = date;
        Sys = sys;
        Dia = dia;
        HRhythm = hRhythm;
        Feeling = feeling;
        Medication = medication;
        IsArrhythmia = isArrhythmia;
    }
}