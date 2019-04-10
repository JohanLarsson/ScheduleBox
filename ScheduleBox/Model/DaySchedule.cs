namespace ScheduleBox.Model
{
    using System.Collections.Generic;

    public class DaySchedule
    {
        public DaySchedule(Person person, IReadOnlyList<Activity> activities)
        {
            this.Person = person;
            this.Activities = activities;
        }

        public Person Person { get; }

        public IReadOnlyList<Activity> Activities { get; }
    }
}