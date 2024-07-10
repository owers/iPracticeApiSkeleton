namespace iPractice.Domain.Entities
{
    public class Client
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public List<Psychologist> Psychologists { get; private set; }
        public List<Booking> Bookings { get; private set; }

        private Client() { }

        public Client(string name, List<Psychologist> psychologists){
            Name = name;
            Psychologists = psychologists;
            Bookings = new List<Booking>();
        }
    }
}