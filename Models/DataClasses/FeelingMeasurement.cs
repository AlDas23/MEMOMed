using System;

namespace MEMOMed.Models.DataClasses;

public class FeelingMeasurement : Measurement
{
    public string Medication { get; init; }
    public string Feeling { get; init; }

    public FeelingMeasurement(int id, int personId, string date, string? medication, string? feeling)
    {
        Id = id;
        PersonId = personId;
        Date = date;
        Medication = medication ?? string.Empty;
        Feeling = feeling  ?? string.Empty;
    }

    public FeelingMeasurement(int personId, string date, string? medication, string? feeling)
    {
        Id = null;
        PersonId = personId;
        Date = date;
        Medication = medication ?? string.Empty;
        Feeling = feeling ?? string.Empty;
    }
}