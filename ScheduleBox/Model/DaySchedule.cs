namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;

    public class DaySchedule
    {
        public DaySchedule(string name, IReadOnlyList<Activity> activities, DateTime starTime, DateTime endTime)
        {
            this.Name = name;
            this.Activities = activities;
        }

        public string Name { get; }

        public IReadOnlyList<Activity> Activities { get; }
    }
}