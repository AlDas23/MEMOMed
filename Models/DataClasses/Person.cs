using System.Collections.Generic;

namespace MEMOMed.Models.DataClasses;

public class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Medicine> MedicineList = new List<Medicine>();

    public override string ToString()
    {
        return $"{Id}:{FirstName} {LastName}";
    }
}