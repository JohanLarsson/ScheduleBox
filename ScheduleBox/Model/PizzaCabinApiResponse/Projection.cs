namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    using System;

    public class Projection
    {
        public Projection(string color, string description, DateTime start, int minutes)
        {
            this.Color = color;
            this.Description = description;
            this.Start = start;
            this.Minutes = minutes;
        }

        public string Color { get; }

        public string Description { get; }

        public DateTime Start { get; }

        public int Minutes { get; }
    }
}