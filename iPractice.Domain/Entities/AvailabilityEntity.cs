using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    public class AvailabilityEntity
    {
        public long Id { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        private AvailabilityEntity() { }

        public AvailabilityEntity(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        internal void Update(Availability availability)
        {
            Start = availability.Start;
            End = availability.End;
        }
    }
}