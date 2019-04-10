namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;

    public class TimeSlot
    {
        public TimeSlot(DateTimeOffset start, DateTimeOffset end, IReadOnlyList<Person> attendees)
        {
            this.Start = start;
            this.End = end;
            this.Attendees = attendees;
        }

        public DateTimeOffset Start { get; }

        public DateTimeOffset End { get; }

        public IReadOnlyList<Person> Attendees { get; }
    }
}