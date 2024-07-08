using System;

namespace iPractice.Api.Models
{
    /// <summary>
    /// Represents a time slot.
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// The identifier of psychologist.
        /// </summary>
        public long PsychologistId { get; set;}

        /// <summary>
        /// The start date and time of the time slot.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end date and time of the time slot.
        /// </summary>
        public DateTime End { get; set; }
    }
}