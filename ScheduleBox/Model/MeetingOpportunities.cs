namespace ScheduleBox.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MeetingOpportunities
    {
        public MeetingOpportunities(IReadOnlyList<TimeSlot> timeSlots, IReadOnlyList<DaySchedule> schedules)
        {
            this.TimeSlots = timeSlots;
            this.Schedules = schedules;
        }

        public IReadOnlyList<TimeSlot> TimeSlots { get; }

        public IReadOnlyList<DaySchedule> Schedules { get; }

        public static MeetingOpportunities Create(DateTime date, IReadOnlyList<DaySchedule> schedules)
        {
            if (schedules.Count == 0)
            {
                throw new ArgumentException("Cannot create MeetingOpportunities from empty schedule.", nameof(schedules));
            }

            var time = new DateTimeOffset(date.Year, date.Month, date.Day, 8, 0, 0, TimeSpan.Zero);
            var slots = new List<TimeSlot>();
            while (time.Hour < 16)
            {
                var end = time.AddMinutes(15);
                slots.Add(new TimeSlot(time, end, GetAttendees(time, end, schedules).ToArray()));
                time = end;
            }

            return new MeetingOpportunities(slots, schedules);
        }

        private static IEnumerable<Person> GetAttendees(DateTimeOffset start, DateTimeOffset end, IReadOnlyList<DaySchedule> schedules)
        {
            foreach (var schedule in schedules)
            {
                if (!schedule.Activities.Any(x => x.Overlaps(start) || x.Overlaps(end)))
                {
                    yield return schedule.Person;
                }
            }
        }
    }
}
