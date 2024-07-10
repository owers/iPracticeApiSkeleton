using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    /// <summary>
    /// Represents an entity for a booking.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Gets the identifier of the booking.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the start date and time of the booking.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Gets the end date and time of the booking.
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        /// Gets the end date and time of the booking.
        /// </summary>
        public Psychologist Psychologist { get; private set; }

        /// <summary>
        /// Gets the client associated with the booking.
        /// </summary>
        public Client Client { get; private set; }
        
        private Booking() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Booking"/> class.
        /// </summary>
        /// <param name="start">The start date and time of the booking.</param>
        /// <param name="end">The end date and time of the booking.</param>
        /// <param name="psychologist">The psychologist associated with the booking.</param>
        /// <param name="client">The client associated with the booking.</param>
        public Booking(DateTime start, DateTime end, Psychologist psychologist, Client client)
        {
            Start = start;
            End = end;
            Psychologist = psychologist;
            Client = client;
        }

        /// <summary>
        /// Determines if the booking overlaps with a time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to check for overlap.</param>
        /// <returns>True if the booking overlaps with the time slot, false otherwise.</returns>
        public bool Overlaps(TimeSlot timeSlot)
        {
            return Overlaps(timeSlot.Start, timeSlot.End);
        }

        /// <summary>
        /// Determines if the booking overlaps with a time slot.
        /// </summary>
        /// <param name="start">The start date and time of the time slot.</param>
        /// <param name="end">The end date and time of the time slot.</param>
        /// <returns>True if the booking overlaps with the time slot, false otherwise.</returns>
        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && End > start;
        }
    }
}