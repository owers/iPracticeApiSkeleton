using iPractice.Domain.Models;

namespace iPractice.Domain.Entities
{
    public class Booking
    {
        public long Id { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Psychologist Psychologist { get; private set; }
        public Client Client { get; private set; }
        
        private Booking() { }

        public Booking(DateTime start, DateTime end, Psychologist psychologist, Client client)
        {
            Start = start;
            End = end;
            Psychologist = psychologist;
            Client = client;
        }

        public bool Overlaps(TimeSlot timeSlot)
        {
            return Overlaps(timeSlot.Start, timeSlot.End);
        }
        
        public bool Overlaps(DateTime start, DateTime end)
        {
            return Start < end && End > start;
        }
    }
}