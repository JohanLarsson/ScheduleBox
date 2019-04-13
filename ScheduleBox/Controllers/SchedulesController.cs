namespace ScheduleBox.Controllers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ScheduleBox.Model;
    using ScheduleBox.Model.PizzaCabinApiResponse;

    [Route("api/schedules")]
    public class SchedulesController : Controller
    {
        private static readonly string[] Formats =
        {
            "O",
            "yyyy-MM-ddTHH\\:mm\\:sszzz",
            "yyyy-MM-ddTHH\\:mm\\:ssZ",
            "yyyy-MM-dd",
        };

        private readonly PizzaCabinClient client;

        public SchedulesController(PizzaCabinClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Get schedules for selected date.
        /// </summary>
        /// <param name="dateString">
        /// The date formatted according to UTC ISO 8601.
        /// https://sv.wikipedia.org/wiki/ISO_8601.
        /// Or yyyy-MM-dd where UTC is assumed.
        /// </param>
        /// <returns>The schedules for selected date.</returns>
        [HttpGet("{dateString}")]
        public async Task<ActionResult<SchedulesResponse>> Get(string dateString)
        {
            if (DateTimeOffset.TryParseExact(dateString, Formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.RoundtripKind | DateTimeStyles.AssumeUniversal, out var date))
            {
                var schedules = await this.client.GetSchedulesAsync(date);
                if (schedules.Count == 0)
                {
                    return this.NotFound("No schedules found for selected date. Please try again.");
                }

                return new SchedulesResponse(schedules);
            }

            return this.BadRequest("Expected a date in ISO 8601 format. Please try again.");
        }
    }
}
