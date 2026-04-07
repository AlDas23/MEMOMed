using System;
using System.Collections.Generic;
using System.Text;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.Models.DataClasses;

public class Medicine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<EWeekday> DaySchedule { get; set; }
    public List<EDayTime> TimeSchedule { get; set; }

    public string GetDayScheduleString()
    {
        var result = new StringBuilder();
        for (int i = 0; i < DaySchedule.Count; i++)
        {
            result.Append((int)DaySchedule[i]);

            if (i < DaySchedule.Count - 1)
            {
                result.Append('|');
            }
        }

        return result.ToString();
    }

    public string GetTimeScheduleString()
    {
        var result = new StringBuilder();
        for (int i = 0; i < TimeSchedule.Count; i++)
        {
            result.Append((int)TimeSchedule[i]);

            if (i < TimeSchedule.Count - 1)
            {
                result.Append('|');
            }
        }

        return result.ToString();
    }

    public void SetDayScheduleFromString(string scheduleString)
    {
        DaySchedule = new List<EWeekday>();

        if (string.IsNullOrEmpty(scheduleString))
        {
            return;
        }

        string[] days = scheduleString.Split('|');

        foreach (string day in days)
        {
            DaySchedule.Add(Enum.Parse<EWeekday>(day));
        }
    }
    
    public void SetTimeScheduleFromString(string scheduleString)
    {
        TimeSchedule = new List<EDayTime>();

        if (string.IsNullOrEmpty(scheduleString))
        {
            return;
        }

        string[] timeslots = scheduleString.Split('|');

        foreach (string time in timeslots)
        {
            TimeSchedule.Add(Enum.Parse<EDayTime>(time));
        }
    }
}