namespace iPractice.Domain.Models
{
    /// <summary>
    /// Represents a time slot.
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// The identifier of psychologist.
        /// </summary>
        public long PsychologistId { get; private set; }

        /// <summary>
        /// The start date and time of the time slot.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// The end date and time of the time slot.
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSlot"/> class.
        /// </summary>
        /// <param name="psychologistId">The identifier of the psychologist.</param>
        /// <param name="start">The start date and time of the time slot.</param>
        /// <param name="end">The end date and time of the time slot.</param>
        public TimeSlot(long psychologistId, DateTime start, DateTime end)
        {
            PsychologistId = psychologistId;
            Start = start;
            End = end;
        }
    }
}