using System;

namespace iPractice.Api.Data;

/// <summary>
/// The time slot of the psychologist.
/// </summary>
public class TimeSlotDto
{
    /// <summary>
    /// The identifier of psychologist.
    /// </summary>
    public long PsychologistId { get; set; }

    /// <summary>
    /// The start date and time of the time slot.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// The end date and time of the time slot.
    /// </summary>
    public DateTime End { get; set; }
}