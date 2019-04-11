namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;

    public class SchedulesResponse
    {
        public SchedulesResponse(DateTimeOffset date, IReadOnlyList<DaySchedule> schedules)
        {
            this.Date = date;
            this.Schedules = schedules;
        }

        public DateTimeOffset Date { get; }

        public IReadOnlyList<DaySchedule> Schedules { get; }
    }
}
