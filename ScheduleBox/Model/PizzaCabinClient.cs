namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class PizzaCabinClient
    {
        private readonly HttpClient httpClient;

        public PizzaCabinClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyList<DaySchedule>> GetSchedulesAsync(DateTimeOffset date)
        {
            var json = await this.httpClient.GetStringAsync($"http://pizzacabininc.azurewebsites.net/PizzaCabinInc.svc/schedule/{date.UtcDateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}").ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<ScheduleResponse>(json);
            if (response.ScheduleResult?.Schedules == null)
            {
                return Array.Empty<DaySchedule>();
            }

            return response.ScheduleResult.Schedules
                           .Select(
                               x => new DaySchedule(
                                   new Person(
                                       x.Name,
                                       x.PersonId),
                                   x.Projection.Select(p =>
                                        new Activity(
                                            p.Description,
                                            p.Color,
                                            p.Start,
                                            p.Start.AddMinutes(p.Minutes)))
                                    .ToArray()))
                           .ToArray();
        }

        public class ScheduleResponse
        {
            public ScheduleResponse(ScheduleResult scheduleResult)
            {
                this.ScheduleResult = scheduleResult;
            }

            public ScheduleResult ScheduleResult { get; }
        }

        public class ScheduleResult
        {
            public ScheduleResult(IReadOnlyList<Schedule> schedules)
            {
                this.Schedules = schedules;
            }

            public IReadOnlyList<Schedule> Schedules { get; }
        }

        public class Schedule
        {
            public Schedule(int contractTimeMinutes, DateTimeOffset date, bool isFullDayAbsence, string name, Guid personId, IReadOnlyList<Projection> projection)
            {
                this.ContractTimeMinutes = contractTimeMinutes;
                this.Date = date;
                this.IsFullDayAbsence = isFullDayAbsence;
                this.Name = name;
                this.PersonId = personId;
                this.Projection = projection;
            }

            public int ContractTimeMinutes { get; }

            public DateTimeOffset Date { get; }

            public bool IsFullDayAbsence { get; }

            public string Name { get; }

            public Guid PersonId { get; }

            public IReadOnlyList<Projection> Projection { get; }
        }

        public class Projection
        {
            public Projection(string color, string description, DateTimeOffset start, int minutes)
            {
                this.Color = color;
                this.Description = description;
                this.Start = start;
                this.Minutes = minutes;
            }

            public string Color { get; }

            public string Description { get; }

            public DateTimeOffset Start { get; }

            public int Minutes { get; }
        }
    }
}
