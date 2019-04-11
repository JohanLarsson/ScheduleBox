namespace ScheduleBox.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ScheduleBox.Model;
    using ScheduleBox.Model.PizzaCabinApiResponse;

    [Route("api/schedules")]
    public class SchedulesController : Controller
    {
        private readonly PizzaCabinClient client;

        public SchedulesController(PizzaCabinClient client)
        {
            this.client = client;
        }

        [HttpGet("{date:datetime}")]
#pragma warning disable MVC1004 // Rename model bound parameter. https://github.com/aspnet/AspNetCore/issues/6945
        public async Task<ActionResult<SchedulesResponse>> Get(DateTime date)
#pragma warning restore MVC1004 // Rename model bound parameter.
        {
            var schedules = await this.client.GetSchedulesAsync(date);
            if (schedules.Count == 0)
            {
                return this.NotFound("No schedules found for selected date. Please try again.");
            }

            return new SchedulesResponse(new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, TimeSpan.Zero), schedules);
        }
    }
}
