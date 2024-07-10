using iPractice.Domain.Models;

namespace iPractice.Domain.Interfaces
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
        /// Updates an availability for a psychologist.
        /// </summary>
        /// <param name="psychologistId">The ID of the psychologist.</param>
        /// <param name="availabilityId">The ID of the availability to update.</param>
        /// <param name="availability">The availability to update.</param>
        Task UpdateAvailability(long psychologistId, long availabilityId, Availability availability);
    }
}
