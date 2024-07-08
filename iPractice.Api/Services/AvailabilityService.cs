using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.DataAccess;
using System.Linq;
using iPractice.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Api.Services
{
    /// <summary>
    /// Handles availability related operations.
    /// </summary>
    public class AvailabilityService : IAvailabilityService
    {
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AvailabilityService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availability">The availability to create.</param>
        /// <returns>The created availability.</returns>
        /// <exception cref="Exception">Thrown when the psychologist is not found.</exception>
        public async Task CreateAvailability(long psychologistId, Availability availability)
        {
            if (availability.Start >= availability.End)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            var psychologist = await _dbContext.Psychologists.FindAsync(psychologistId) ?? throw new Exception("Psychologist not found.");
            var newAvailability = new AvailabilityEntity
            {
                Start = availability.Start,
                End = availability.End
            };

            psychologist.Availabilities.Add(newAvailability);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Creates an appointment for a client with a psychologist.
        /// </summary>
        /// <param name="clientId">The client's identifier.</param>
        /// <param name="timeSlot">The time slot for the appointment.</param>
        /// <returns>The created appointment.</returns>
        /// <exception cref="Exception">Thrown when the psychologist or client is not found.</exception>
        public async Task CreateAppointment(long clientId, TimeSlot timeSlot)
        {
            if (timeSlot.Start >= timeSlot.End)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            var psychologist = await _dbContext.Psychologists
                .Include(p => p.Availabilities)
                .Include(p => p.Bookings)
                .Include(p => p.Clients)
                .FirstOrDefaultAsync(p => p.Id == timeSlot.PsychologistId) ?? throw new Exception("Psychologist not found.");

            var client = psychologist.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new Exception("Client not found.");
            var availability = FindAvailability(psychologist.Availabilities, timeSlot) ?? throw new Exception("Availability not found.");

            foreach (var booking in psychologist.Bookings)
            {
                if (Overlaps(booking, timeSlot))
                {
                    throw new Exception("Booking overlaps with another booking.");
                }
            }

            var newBooking = new Booking
            {
                Start = timeSlot.Start,
                End = timeSlot.End,
                Psychologist = psychologist,
                Client = client
            };

            psychologist.Bookings.Add(newBooking);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the available time slots for a client.
        /// </summary>
        /// <param name="clientId">The client's identifier.</param>
        /// <returns>The available time slots.</returns>
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlots(long clientId)
        {
            var psychologists = await _dbContext.Psychologists
                .Include(p => p.Availabilities)
                .Include(p => p.Bookings)
                .Where(p => p.Clients.Any(c => c.Id == clientId))
                .ToListAsync();

            if (psychologists.Count == 0)
            {
                return Enumerable.Empty<TimeSlot>();
            }

            var ans = new List<(AvailabilityEntity, long)>();

            foreach (var psychologist in psychologists)
            {
                foreach (var availability in psychologist.Availabilities)
                {
                    var start = availability.Start;
                    var end = availability.End;
                    var orderedBookings = psychologist.Bookings
                        .Where(b => Overlaps(b, start, end))
                        .OrderBy(b => b.Start)
                        .ToList();
                    
                    var nextStart = start;
                    foreach (var booking in orderedBookings)
                    {
                        if (booking.Start > nextStart)
                        {
                            ans.Add((new AvailabilityEntity { Start = nextStart, End = booking.Start }, psychologist.Id));
                            nextStart = booking.End;
                        }
                    }

                    if (end > nextStart)
                    {
                        ans.Add((new AvailabilityEntity { Start = nextStart, End = end }, psychologist.Id));
                    }
                }
            }

            return ans.SelectMany(_ => CreateTimeSlots(_.Item1, _.Item2)).ToList();
        }

        /// <summary>
        /// Updates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availabilityId">The availability's identifier.</param>
        /// <param name="availability">The availability to update.</param>
        /// <returns>The updated availability.</returns>
        /// <exception cref="Exception">Thrown when the psychologist is not found.</exception>
        public async Task UpdateAvailability(long psychologistId, long availabilityId, Availability availability)
        {
            if (availability.Start >= availability.End)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            var psychologist = await _dbContext.Psychologists
                .Include(p => p.Availabilities)
                .FirstOrDefaultAsync(p => p.Id == psychologistId) ?? throw new Exception("Psychologist not found.");

            var availabilityEntity = psychologist.Availabilities
                .FirstOrDefault(a => a.Id == availabilityId) ?? throw new Exception("Availability not found.");

            availabilityEntity.Start = availability.Start;
            availabilityEntity.End = availability.End;

            await _dbContext.SaveChangesAsync();
        }

        private static bool Overlaps(Booking booking, TimeSlot timeSlot)
        {
            return booking.Start < timeSlot.End && booking.End > timeSlot.Start;
        }

        private static bool Overlaps(Booking booking, DateTime start, DateTime end)
        {
            return booking.Start < end && booking.End > start;
        }

        private static AvailabilityEntity FindAvailability(List<AvailabilityEntity> availabilities, TimeSlot timeSlot)
        {
            return availabilities.FirstOrDefault(a => a.Start <= timeSlot.Start && a.End >= timeSlot.End);
        }

        private static List<TimeSlot> CreateTimeSlots(AvailabilityEntity availability, long psychologistId)
        {
            var start = availability.Start;
            var end = availability.End;
            var slots = new List<TimeSlot>();

            while (start < end)
            {
                var slotEnd = start.AddMinutes(30);
                if (slotEnd > end)
                {
                    slotEnd = end;
                }

                // minimum session time
                if (slotEnd - start >= TimeSpan.FromMinutes(30))
                {
                    slots.Add(new TimeSlot
                    {
                        Start = start,
                        End = slotEnd,
                        PsychologistId = psychologistId
                    });
                }

                start = slotEnd;
            }

            return slots;
        }
    }
}
