using iPractice.Domain.Exceptions;
using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    public class Psychologist
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public List<Client> Clients { get; private set; }
        public List<Booking> Bookings { get; private set; } = new List<Booking>();
        public List<AvailabilityEntity> Availabilities { get; private set; } = new List<AvailabilityEntity>();

        private Psychologist() { }

        public Psychologist(string name)
        {
            Name = name;
        }

        public void AddAvailability(Availability availability)
        {
            Availabilities.Add(new AvailabilityEntity(availability.Start, availability.End));
        }

        public void UpdateAvailability(long availabilityId, Availability availability)
        {
            var availabilityEntity = Availabilities.FirstOrDefault(a => a.Id == availabilityId) 
                                     ?? throw new EntityNotFoundException(nameof(AvailabilityEntity), availabilityId);
            
            availabilityEntity.Update(availability);
        }

        public void AddBooking(Booking newBooking)
        {
            Bookings.Add(newBooking);
        }

        public bool IsAvailable(TimeSlot timeSlot)
        {
            return Availabilities.Any(a => a.Start <= timeSlot.Start && a.End >= timeSlot.End);
        }

        public bool IsBooked(TimeSlot timeSlot)
        {
            return Bookings.Any(b => b.Overlaps(timeSlot));
        }
    }
}