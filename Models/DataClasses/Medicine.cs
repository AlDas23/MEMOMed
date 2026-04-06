using System.Collections.Generic;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.Models.DataClasses;

public class Medicine
{
    private int Id { get; set; }
    public required string Name;
    public required string Description;
    public required List<EWeekday> DaySchedule;
    public required List<EDayTime> TimeSchedule;
}