namespace ScheduleBox.Model
{
    using System;

    public class Activity
    {
        public Activity(string description, string color, DateTime start, DateTime end)
        {
            this.Description = description;
            this.Start = start;
            this.End = end;
            this.Color = color;
        }

        public string Description { get; }

        public string Color { get; }

        public DateTime Start { get; }

        public DateTime End { get; }
    }
}