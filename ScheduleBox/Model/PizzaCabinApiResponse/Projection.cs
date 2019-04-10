namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    using System;

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