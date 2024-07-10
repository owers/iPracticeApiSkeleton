namespace iPractice.Domain.Models
{
    /// <summary>
    /// Represents the availability of a psychologist.
    /// </summary>
    public class Availability
    {
        /// <summary>
        /// The start date and time of the availability.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// The end date and time of the availability.
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Availability"/> class.
        /// </summary>
        /// <param name="start">The start date and time of the availability.</param>
        /// <param name="end">The end date and time of the availability.</param>
        public Availability(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}