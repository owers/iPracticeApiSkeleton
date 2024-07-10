using iPractice.Domain.Models;

namespace iPractice.Domain.Interfaces;

public interface IAppointmentService
{
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
}