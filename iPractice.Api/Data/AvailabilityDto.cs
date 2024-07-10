using System;

namespace iPractice.Api.Data;

/// <summary>
/// The availability of the psychologist.
/// </summary>
public class AvailabilityDto
{
    /// <summary>
    /// The start date and time of the availability.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// The end date and time of the availability.
    /// </summary>
    public DateTime End { get; set; }
}