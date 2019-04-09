namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    public class ScheduleResponse
    {
        public ScheduleResponse(Scheduleresult scheduleResult)
        {
            this.ScheduleResult = scheduleResult;
        }

        public Scheduleresult ScheduleResult { get; }
    }
}