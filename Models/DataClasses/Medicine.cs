using System;
using System.Collections.Generic;
using System.Text;
using MEMOMed.Models.DataClasses.Enums;

namespace MEMOMed.Models.DataClasses;

public class Medicine
{
    public int? Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    private List<EWeekday>? DaySchedule { get; }
    public string DayScheduleString => GetDayScheduleString();
    private List<EDayTime>? TimeSchedule { get; }
    public string TimeScheduleString => GetTimeScheduleString();

    // Full constructor
    public Medicine(int? id, string name, string description, List<EWeekday> daySchedule, List<EDayTime> timeSchedule)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.DaySchedule = daySchedule;
        this.TimeSchedule = timeSchedule;
    }

    // Only name + description constructor
    public Medicine(string name, string description)
    {
        this.Id = null;
        this.Name = name;
        this.Description = description;
        this.DaySchedule = null;
        this.TimeSchedule = null;
    }

    public string GetDayScheduleString()
    {
        if (DaySchedule == null || DaySchedule.Count == 0)
        {
            return string.Empty;
        }

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
        if (TimeSchedule == null || TimeSchedule.Count == 0)
        {
            return string.Empty;
        }

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

    public static List<EWeekday> DayScheduleFromString(string scheduleString)
    {
        var daySchedule = new List<EWeekday>();

        if (scheduleString == string.Empty)
        {
            return daySchedule;
        }

        var days = scheduleString.Split('|');

        foreach (string day in days)
        {
            daySchedule.Add(Enum.Parse<EWeekday>(day));
        }

        return daySchedule;
    }

    public static List<EDayTime> TimeScheduleFromString(string scheduleString)
    {
        var timeSchedule = new List<EDayTime>();

        if (scheduleString == string.Empty)
        {
            return timeSchedule;
        }

        var timeslots = scheduleString.Split('|');

        foreach (string time in timeslots)
        {
            timeSchedule.Add(Enum.Parse<EDayTime>(time));
        }

        return timeSchedule;
    }
}