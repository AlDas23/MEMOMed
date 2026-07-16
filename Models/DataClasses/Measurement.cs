namespace MEMOMed.Models.DataClasses;

public abstract class Measurement
{
    public string? DateTime { get; set; }
    public int? PersonId { get; set; }
    public int? Id { get; set; }
}