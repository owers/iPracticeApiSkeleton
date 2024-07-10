using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Domain.Interfaces;
using iPractice.Domain.Models;

namespace iPractice.Domain.Services
{
    /// <summary>
    /// Handles availability related operations.
    /// </summary>
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IPsychologistRepository _psychologistRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityService"/> class.
        /// </summary>
        /// <param name="psychologistRepository">The database context.</param>
        public AvailabilityService(IPsychologistRepository psychologistRepository)
        {
            _psychologistRepository = psychologistRepository;
        }

        /// <summary>
        /// Creates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availability">The availability to create.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when the start time is after the end time.</exception>
        /// <exception cref="EntityNotFoundException">Thrown when the psychologist is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the availability is null.</exception>
        public async Task CreateAvailability(long psychologistId, Availability availability)
        {
            if (availability == null)
            {
                throw new ArgumentNullException(nameof(availability));
            }

            if (availability.Start >= availability.End)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            var psychologist = await _psychologistRepository.FindAsync(psychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), psychologistId);

            psychologist.AddAvailability(availability);

            await _psychologistRepository.UpdateAsync(psychologist);
        }
        
        /// <summary>
        /// Updates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availabilityId">The availability's identifier.</param>
        /// <param name="availability">The availability to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentException">Thrown when the start time is after the end time.</exception>
        /// <exception cref="EntityNotFoundException">Thrown when the psychologist is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the availability is null.</exception>
        public async Task UpdateAvailability(long psychologistId, long availabilityId, Availability availability)
        {
            if (availability == null)
            {
                throw new ArgumentNullException(nameof(availability));
            }

            if (availability.Start >= availability.End)
            {
                throw new ArgumentException("Start time must be before end time");
            }

            var psychologist = await _psychologistRepository.FindAsync(psychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), psychologistId);

            psychologist.UpdateAvailability(availabilityId, availability);

            await _psychologistRepository.UpdateAsync(psychologist);
        }
    }
}
