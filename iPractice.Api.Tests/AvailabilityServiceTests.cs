using iPractice.Api.Models;
using iPractice.Api.Services;
using iPractice.DataAccess;
using Moq;

namespace iPractice.Api.Tests;

public class AvailabilityServiceTest
{
    private readonly Mock<IAvailabilityService> _availabilityServiceMock;
    private readonly Mock<ApplicationDbContext> _dbContextMock;
    private readonly AvailabilityService _availabilityService;

    public AvailabilityServiceTest()
    {
        _availabilityServiceMock = new Mock<IAvailabilityService>();
        _dbContextMock = new Mock<ApplicationDbContext>();
        _availabilityService = new AvailabilityService(_dbContextMock.Object);
    }

    [Fact]
    public async Task CreateAvailability_ShouldDelegateToAvailabilityService()
    {
        // Arrange
        var psychologistId = 1;
        var availability = new Availability();

        // Act
        await _availabilityService.CreateAvailability(psychologistId, availability);

        // Assert
        _availabilityServiceMock.Verify(x => x.CreateAvailability(psychologistId, availability), Times.Once);
    }

    [Fact]
    public async Task CreateAppointment_ShouldDelegateToAvailabilityService()
    {
        // Arrange
        var clientId = 1;
        var timeSlot = new TimeSlot();

        // Act
        await _availabilityService.CreateAppointment(clientId, timeSlot);

        // Assert
        _availabilityServiceMock.Verify(x => x.CreateAppointment(clientId, timeSlot), Times.Once);
    }

    [Fact]
    public async Task GetAvailableTimeSlots_ShouldDelegateToAvailabilityService()
    {
        // Arrange
        var clientId = 1;

        // Act
        await _availabilityService.GetAvailableTimeSlots(clientId);

        // Assert
        _availabilityServiceMock.Verify(x => x.GetAvailableTimeSlots(clientId), Times.Once);
    }

    [Fact]
    public async Task UpdateAvailability_ShouldDelegateToAvailabilityService()
    {
        // Arrange
        var psychologistId = 1;
        var availabilityId = 1;
        var availability = new Availability();

        // Act
        await _availabilityService.UpdateAvailability(psychologistId, availabilityId, availability);

        // Assert
        _availabilityServiceMock.Verify(x => x.UpdateAvailability(psychologistId, availabilityId, availability), Times.Once);
    }
}
