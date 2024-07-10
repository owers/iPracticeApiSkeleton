using iPractice.Domain.Exceptions;
using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    /// <summary>
    /// Represents an entity for a psychologist.
    /// </summary>
    public class Psychologist
    {
        /// <summary>
        /// Gets the identifier of the psychologist.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the name of the psychologist.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the list of clients associated with the psychologist.
        /// </summary>
        public List<Client> Clients { get; private set; }

        /// <summary>
        /// Gets the list of bookings associated with the psychologist.
        /// </summary>
        public List<Booking> Bookings { get; private set; } = new List<Booking>();

        /// <summary>
        /// Gets the list of availabilities associated with the psychologist.
        /// </summary>
        public List<AvailabilityEntity> Availabilities { get; private set; } = new List<AvailabilityEntity>();

        private Psychologist() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Psychologist"/> class.
        /// </summary>
        /// <param name="name">The name of the psychologist.</param>
        public Psychologist(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Adds an availability to the psychologist.
        /// </summary>
        /// <param name="availability">The availability to add.</param>
        public void AddAvailability(Availability availability)
        {
            Availabilities.Add(new AvailabilityEntity(availability.Start, availability.End));
        }

        /// <summary>
        /// Updates an availability of the psychologist.
        /// </summary>
        /// <param name="availabilityId">The identifier of the availability to update.</param>
        /// <param name="availability">The availability to update.</param>
        public void UpdateAvailability(long availabilityId, Availability availability)
        {
            var availabilityEntity = Availabilities.FirstOrDefault(a => a.Id == availabilityId)
                                     ?? throw new EntityNotFoundException(nameof(AvailabilityEntity), availabilityId);

            availabilityEntity.Update(availability);
        }

        /// <summary>
        /// Adds a booking to the psychologist.
        /// </summary>
        /// <param name="newBooking">The booking to add.</param>
        public void AddBooking(Booking newBooking)
        {
            Bookings.Add(newBooking);
        }

        /// <summary>
        /// Determines if the psychologist is available for a time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to check for availability.</param>
        /// <returns>True if the psychologist is available for the time slot, false otherwise.</returns>
        public bool IsAvailable(TimeSlot timeSlot)
        {
            return Availabilities.Any(a => a.Start <= timeSlot.Start && a.End >= timeSlot.End);
        }


        /// <summary>
        /// Determines if the psychologist is booked for a time slot.
        /// </summary>
        /// <param name="timeSlot">The time slot to check for booking.</param>
        /// <returns>True if the psychologist is booked for the time slot, false otherwise.</returns>
        public bool IsBooked(TimeSlot timeSlot)
        {
            return Bookings.Any(b => b.Overlaps(timeSlot));
        }
    }
}