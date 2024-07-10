using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iPractice.Domain.Interfaces;
using iPractice.Domain.Models;
using iPractice.Api.Data;

namespace iPractice.Api.Controllers
{
    /// <summary>
    /// Handles client-related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="appointmentService">The appointment instance.</param>
        public ClientController(ILogger<ClientController> logger, IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves available time slots for a client.
        /// </summary>
        /// <param name="clientId">The client's identifier.</param>
        /// <returns>A list of available time slots.</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlot>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlots(long clientId)
        {
            try
            {
                var availableTimeSlots = await _appointmentService.GetAvailableTimeSlots(clientId);
                return new ActionResult<IEnumerable<TimeSlot>>(availableTimeSlots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get available time slots for client {clientId}", clientId);
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        /// <summary>
        /// Creates an appointment for a client with a psychologist.
        /// </summary>
        /// <param name="clientId">The client's identifier.</param>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="timeSlot">The time slot for the appointment.</param>
        /// <returns>A status indicating the success or failure of the operation.</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateAppointment(
            [FromRoute] long clientId,
            [FromBody] TimeSlotDto timeSlot)
        {
            if (timeSlot == null)
            {
                return BadRequest("Time slot is missing.");
            }

            try
            {
                await _appointmentService.CreateAppointment(clientId, new TimeSlot(timeSlot.PsychologistId, timeSlot.Start, timeSlot.End));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create appointment for client {clientId} and psychologist {psychologistId}", clientId, timeSlot.PsychologistId);
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }
    }
}

