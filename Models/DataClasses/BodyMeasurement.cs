namespace MEMOMed.Models.DataClasses;

public class BodyMeasurement : Measurement
{
    public double Temperature { get; }

    // Full constructor
    public BodyMeasurement(int id, int personId, string date, double temperature)
    {
        Id = id;
        PersonId = personId;
        DateTime = date;
        Temperature = temperature;
    }
    
    public BodyMeasurement(int personId, string date, double temperature)
    {
        Id = null;
        PersonId = personId;
        DateTime = date;
        Temperature = temperature;
    }
}