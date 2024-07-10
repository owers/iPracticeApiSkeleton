using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    /// <summary>
    /// Represents an entity for availability.
    /// </summary>
    public class AvailabilityEntity
    {
        /// <summary>
        /// Gets the identifier of the availability.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the start date and time of the availability.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Gets the end date and time of the availability.
        /// </summary>
        public DateTime End { get; private set; }

        private AvailabilityEntity() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityEntity"/> class.
        /// </summary>
        /// <param name="start">The start date and time of the availability.</param>
        /// <param name="end">The end date and time of the availability.</param>
        public AvailabilityEntity(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Updates the availability with new values.
        /// </summary>
        /// <param name="availability">The availability to update.</param>
        public void Update(Availability availability)
        {
            Start = availability.Start;
            End = availability.End;
        }
    }
}