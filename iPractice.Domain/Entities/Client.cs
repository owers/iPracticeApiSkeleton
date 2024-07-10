namespace iPractice.Domain.Entities
{
    /// <summary>
    /// Represents an entity for a client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets the identifier of the client.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the name of the client.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the list of psychologists associated with the client.
        /// </summary>
        public List<Psychologist> Psychologists { get; private set; }

        /// <summary>
        /// Gets the list of bookings associated with the client.
        /// </summary>
        public List<Booking> Bookings { get; private set; }

        private Client() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="name">The name of the client.</param>
        /// <param name="psychologists">The list of psychologists associated with the client.</param>
        public Client(string name, List<Psychologist> psychologists)
        {
            Name = name;
            Psychologists = psychologists;
            Bookings = new List<Booking>();
        }
    }
}