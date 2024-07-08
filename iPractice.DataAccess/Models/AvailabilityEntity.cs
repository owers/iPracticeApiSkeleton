using System;

namespace iPractice.DataAccess.Models
{
    public class AvailabilityEntity
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}