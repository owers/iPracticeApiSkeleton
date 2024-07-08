using System.Collections.Generic;
using System.Threading.Tasks;
using iPractice.Api.Models;

namespace iPractice.Api.Services
{
    /// <summary>
    /// Defines the interface for the availability service.
    /// </summary>
    public interface IAvailabilityService
    {
        /// <summary>
        /// Creates a new availability for a psychologist.
        /// </summary>
        /// <param name="psychologistId">The ID of the psychologist.</param>
        /// <param name="availability">The availability to create.</param>
        Task CreateAvailability(long psychologistId, Availability availability);
        
        /// <summary>
        /// Creates a new appointment for a client.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <param name="timeSlot">The time slot for the appointment.</param>
        Task CreateAppointment(long clientId, TimeSlot timeSlot);

        /// <summary>
        /// Gets the available time slots for a client.
        /// </summary>
        /// <param name="clientId">The ID of the client.</param>
        /// <returns>The available time slots for the client.</returns>
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlots(long clientId);

        /// <summary>
        /// Updates an availability for a psychologist.
        /// </summary>
        /// <param name="psychologistId">The ID of the psychologist.</param>
        /// <param name="availabilityId">The ID of the availability to update.</param>
        /// <param name="availability">The availability to update.</param>
        Task UpdateAvailability(long psychologistId, long availabilityId, Availability availability);
    }
}
