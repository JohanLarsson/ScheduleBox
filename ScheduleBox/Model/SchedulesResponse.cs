namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SchedulesResponse
    {
        public SchedulesResponse(IReadOnlyList<DaySchedule> schedules)
        {
            this.Schedules = schedules;
            this.Start = schedules.SelectMany(x => x.Activities).Min(x => x.Start);
            this.End = schedules.SelectMany(x => x.Activities).Max(x => x.End);
        }

        public DateTimeOffset Start { get; }

        public DateTimeOffset End { get; }

        public IReadOnlyList<DaySchedule> Schedules { get; }
    }
}
