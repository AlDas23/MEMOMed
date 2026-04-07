using System.Collections.Generic;

namespace MEMOMed.Models.DataClasses;

public class Person
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Medicine>? MedicineList { get; set; }
}