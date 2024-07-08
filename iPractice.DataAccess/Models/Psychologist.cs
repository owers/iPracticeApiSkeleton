using System.Collections.Generic;

namespace iPractice.DataAccess.Models
{
    public class Psychologist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Client> Clients { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<AvailabilityEntity> Availabilities { get; set; } = new List<AvailabilityEntity>();
    }
}