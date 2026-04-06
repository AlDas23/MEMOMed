namespace MEMOMed.Models.DataClasses;

public abstract class Measurement
{
    public required string Datetime { get; set; }
    public required int PersonId { get; set; }
}