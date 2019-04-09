namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    using System.Collections.Generic;

    public class Scheduleresult
    {
        public Scheduleresult(IReadOnlyList<Schedule> schedules)
        {
            this.Schedules = schedules;
        }

        public IReadOnlyList<Schedule> Schedules { get; }
    }
}