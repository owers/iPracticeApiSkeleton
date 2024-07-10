using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using iPractice.Domain.Interfaces;
using iPractice.Domain.Models;
using iPractice.Api.Data;

namespace iPractice.Api.Controllers
{
    /// <summary>
    /// Handles psychologist availability related operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PsychologistController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityHandler;
        private readonly ILogger<PsychologistController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PsychologistController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="availabilityHandler">The availability handler.</param>
        public PsychologistController(ILogger<PsychologistController> logger, IAvailabilityService availabilityHandler)
        {
            _availabilityHandler = availabilityHandler;
            _logger = logger;
        }

        /// <summary>
        /// Creates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availability"></param>
        [HttpPost("{psychologistId}/availability")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateAvailability([FromRoute] long psychologistId, [FromBody] AvailabilityDto availability)
        {
            if (availability == null)
            {
                return BadRequest("Availability is missing.");
            }

            try
            {
                await _availabilityHandler.CreateAvailability(psychologistId, new Availability(availability.Start, availability.End));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create availability for psychologist {psychologistId}", psychologistId);
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }

        /// <summary>
        /// Updates the availability of a psychologist.
        /// </summary>
        /// <param name="psychologistId">The psychologist's identifier.</param>
        /// <param name="availabilityId">The availability's identifier.</param>
        /// <param name="availability">The availability to update.</param>
        /// <returns>The updated availability.</returns>
        [HttpPut("{psychologistId}/availability/{availabilityId}")]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Availability>> UpdateAvailability(
            [FromRoute] long psychologistId,
            [FromRoute] long availabilityId,
            [FromBody] AvailabilityDto availability)
        {
            if (availability == null)
            {
                return BadRequest("Availability is missing.");
            }

            try
            {
                await _availabilityHandler.UpdateAvailability(psychologistId, availabilityId, new Availability(availability.Start, availability.End));
                return Ok(availability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update availability {availabilityId} for psychologist {psychologistId}", availabilityId, psychologistId);
                return StatusCode(500, "An unexpected error occurred on the server.");
            }
        }
    }
}
