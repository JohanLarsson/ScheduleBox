namespace ScheduleBox.Model
{
    using System;

    public class Activity
    {
        public Activity(string description, string color, DateTimeOffset start, DateTimeOffset end)
        {
            this.Description = description;
            this.Start = start;
            this.End = end;
            this.Color = color;
        }

        public string Description { get; }

        public string Color { get; }

        public DateTimeOffset Start { get; }

        public DateTimeOffset End { get; }
    }
}