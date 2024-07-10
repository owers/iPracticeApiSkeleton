using iPractice.Domain.Entities;
using iPractice.Domain.Exceptions;
using iPractice.Domain.Interfaces;
using iPractice.Domain.Models;

namespace iPractice.Domain.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IPsychologistRepository _psychologistRepository;

    public AppointmentService(IPsychologistRepository psychologistRepository)
    {
        _psychologistRepository = psychologistRepository;
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

        var psychologist = await _psychologistRepository.FindAsync(timeSlot.PsychologistId) ?? throw new EntityNotFoundException(nameof(Psychologist), timeSlot.PsychologistId);
        var client = psychologist.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new Exception("Client does not belong to psychologist.");
            
        if (!psychologist.IsAvailable(timeSlot))
        {
            throw new Exception("Psychologist is not available.");
        }
        if (psychologist.IsBooked(timeSlot))
        {
            throw new Exception("Booking overlaps with another booking.");
        }

        var newBooking = new Booking(timeSlot.Start, timeSlot.End, psychologist, client);

        psychologist.AddBooking(newBooking);

        await _psychologistRepository.UpdateAsync(psychologist);
    }

    /// <summary>
    /// Retrieves the available time slots for a client.
    /// </summary>
    /// <param name="clientId">The client's identifier.</param>
    /// <returns>The available time slots.</returns>
    public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlots(long clientId)
    {
        var psychologists = await _psychologistRepository.FindAllByClientId(clientId);

        if (psychologists.Count == 0)
        {
            return [];
        }

        var ans = new List<TimeSlot>();

        foreach (var psychologist in psychologists)
        {
            foreach (var availability in psychologist.Availabilities)
            {
                var start = availability.Start;
                var end = availability.End;
                var orderedBookings = psychologist.Bookings
                    .Where(b => b.Overlaps(start, end))
                    .OrderBy(b => b.Start)
                    .ToList();

                var nextStart = start;
                foreach (var booking in orderedBookings.Where(booking => booking.Start > nextStart))
                {
                    ans.Add(new TimeSlot { Start = nextStart, End = booking.Start, PsychologistId = psychologist.Id });
                    nextStart = booking.End;
                }

                if (end > nextStart)
                {
                    ans.Add(new TimeSlot { Start = nextStart, End = end, PsychologistId =psychologist.Id });
                }
            }
        }

        return ans.SelectMany(PartitionTimeSlots).ToList();
    }

    private static List<TimeSlot> PartitionTimeSlots(TimeSlot timeSlot)
    {
        var start = timeSlot.Start;
        var end = timeSlot.End;
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
                    PsychologistId = timeSlot.PsychologistId
                });
            }

            start = slotEnd;
        }

        return slots;
    }
}