namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;

    public class DaySchedule
    {
        public DaySchedule(string name, Guid id, IReadOnlyList<Activity> activities)
        {
            this.Name = name;
            this.Id = id;
            this.Activities = activities;
        }

        public string Name { get; }

        public Guid Id { get; }

        public IReadOnlyList<Activity> Activities { get; }
    }
}