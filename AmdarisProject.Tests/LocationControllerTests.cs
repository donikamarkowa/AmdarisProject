using AmdarisProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using WorkoutReservations.Application.DTOs.Location;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Tests
{
    public class LocationControllerTests
    {
        public class ErrorResponse
        {
            public string Message { get; set; }
        }

        private readonly Mock<ILocationService> _locationServiceMock;
        private readonly Mock<IWorkoutService> _workoutServiceMock;
        private readonly LocationController _controller;
        public LocationControllerTests()
        {
            _locationServiceMock = new Mock<ILocationService>();
            _workoutServiceMock = new Mock<IWorkoutService>();
            _controller = new LocationController(_locationServiceMock.Object, _workoutServiceMock.Object);
        }

        [Fact]
        public async Task Add_ShouldReturnOk_WhenLocationIsAdded()
        {
            var dto = new AddLocationDto();
            _locationServiceMock.Setup(s => s.AddLocationAsync(dto)).Returns(Task.CompletedTask);

            var result = await _controller.Add(dto);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Location added successfully.", actionResult.Value);
        }

        [Fact]
        public async Task Add_ShouldReturnForbidden_WhenUnauthorizedAccessExceptionIsThrown()
        {
            var dto = new AddLocationDto();
            _locationServiceMock.Setup(s => s.AddLocationAsync(dto)).Throws<UnauthorizedAccessException>();

            var result = await _controller.Add(dto);

            var actionResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, actionResult.StatusCode);
        }

        [Fact]
        public async Task Add_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var dto = new AddLocationDto();
            var expectedMessage = "Unexpected error occurred while trying to add location! Please try again later!";
            _locationServiceMock.Setup(s => s.AddLocationAsync(dto)).Throws<Exception>();

            var result = await _controller.Add(dto);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var value = Assert.IsType<WorkoutReservations.Domain.Exceptions.ErrorResponse>(actionResult.Value);
            Assert.Equal(expectedMessage, value.Message);
        }

        [Fact]
        public async Task LocationsByWorkout_ShouldReturnOk_WhenWorkoutExists()
        {
            var workoutId = Guid.NewGuid();
            _workoutServiceMock.Setup(s => s.ExistsByIdAsync(workoutId)).ReturnsAsync(true);
            _locationServiceMock.Setup(s => s.LocationsByWorkoutIdAsync(workoutId)).ReturnsAsync(new List<LocationDto>());

            var result = await _controller.LocationsByWorkout(workoutId);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Empty((List<LocationDto>)actionResult.Value!);
        }

        [Fact]
        public async Task LocationsByWorkout_ShouldReturnBadRequest_WhenWorkoutDoesNotExist()
        {
            var workoutId = Guid.NewGuid();
            _workoutServiceMock.Setup(s => s.ExistsByIdAsync(workoutId)).ReturnsAsync(false);

            var result = await _controller.LocationsByWorkout(workoutId);

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Workout does not exist.", actionResult.Value);
        }

        [Fact]
        public async Task All_ShouldReturnOk_WhenLocationsExist()
        {
            _locationServiceMock.Setup(s => s.AllLocationsAsync()).ReturnsAsync(new List<LocationDto>());

            var result = await _controller.All();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Empty((List<LocationDto>)actionResult.Value!);
        }

        [Fact]
        public async Task All_ShouldReturnNotFound_WhenNotFoundExceptionIsThrown()
        {
            _locationServiceMock.Setup(s => s.AllLocationsAsync()).Throws<NotFoundException>();

            var result = await _controller.All();

            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task All_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var expectedMessage = "Unexpected error occurred while trying to get all locations! Please try again later!";
            _locationServiceMock.Setup(s => s.AllLocationsAsync()).Throws<Exception>();

            var result = await _controller.All();

            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var value = Assert.IsType<WorkoutReservations.Domain.Exceptions.ErrorResponse>(actionResult.Value);
            Assert.Equal(expectedMessage, value.Message.ToString());
        }
    }
}