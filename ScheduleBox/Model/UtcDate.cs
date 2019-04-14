namespace ScheduleBox.Model
{
    using System;
    using System.Globalization;

    public struct UtcDate
    {
        private const string Format = "yyyy-MM-dd";
        private readonly DateTime? date;

        public UtcDate(DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Expected UTC.");
            }

            this.date = date;
        }

        public static bool TryParse(string text, out UtcDate date)
        {
            if (DateTimeOffset.TryParseExact(text, Format, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out var offset))
            {
                date = new UtcDate(offset.UtcDateTime);
                return true;
            }

            date = default(UtcDate);
            return false;
        }

        public override string ToString()
        {
            if (this.date is DateTime dateTime)
            {
                return dateTime.ToString(Format, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException("default instance cannot be used.");
        }
    }
}