namespace ScheduleBox.Model
{
    using System;

    public class Person
    {
        public Person(string name, Guid id)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Name { get; }

        public Guid Id { get; }
    }
}