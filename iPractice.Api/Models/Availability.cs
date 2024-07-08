using System;

namespace iPractice.Api.Models
{
    /// <summary>
    /// Represents the availability of a psychologist.
    /// </summary>
    public class Availability
    {
        /// <summary>
        /// The start date and time of the availability.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end date and time of the availability.
        /// </summary>
        public DateTime End { get; set; }
    }
}