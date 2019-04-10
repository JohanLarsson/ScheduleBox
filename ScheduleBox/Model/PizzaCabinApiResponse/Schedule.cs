namespace ScheduleBox.Model.PizzaCabinApiResponse
{
    using System;
    using System.Collections.Generic;

    public class Schedule
    {
        public Schedule(int contractTimeMinutes, DateTimeOffset date, bool isFullDayAbsence, string name, string personId, IReadOnlyList<Projection> projection)
        {
            this.ContractTimeMinutes = contractTimeMinutes;
            this.Date = date;
            this.IsFullDayAbsence = isFullDayAbsence;
            this.Name = name;
            this.PersonId = personId;
            this.Projection = projection;
        }

        public int ContractTimeMinutes { get;  }

        public DateTimeOffset Date { get;  }

        public bool IsFullDayAbsence { get;  }

        public string Name { get;  }

        public string PersonId { get;  }

        public IReadOnlyList<Projection> Projection { get; }
    }
}