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

        [HttpGet("{dateString}")]
#pragma warning disable MVC1004 // Rename model bound parameter. https://github.com/aspnet/AspNetCore/issues/6945
        public async Task<ActionResult<SchedulesResponse>> Get(string dateString)
#pragma warning restore MVC1004 // Rename model bound parameter.
        {
            if (DateTimeOffset.TryParse(dateString, out var date))
            {
                var schedules = await this.client.GetSchedulesAsync(date);
                if (schedules.Count == 0)
                {
                    return this.NotFound("No schedules found for selected date. Please try again.");
                }

                return new SchedulesResponse(schedules);
            }

            return this.BadRequest("Expected a date. Please try again.");
        }
    }
}
