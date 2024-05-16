using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.DTOs.Location;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddLocationDto dto)
        {
            try
            {
                await _locationService.AddLocationAsync(dto);

                return Ok("Location added successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Failed to add location.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-workout")]
        public async Task<IActionResult> AddWorkout(Guid locationId, Guid workoutId)
        {
            try
            {
                await _locationService.AddWorkoutToLocationAsync(locationId, workoutId);

                return Ok("Workout added to location successfully.");

            }
            catch (UnauthorizedAccessException ex)
            {
                // 403 Forbidden if the user is not authorized
                return StatusCode(403, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Failed to add workout.");
            }
        }


        [Authorize(Roles = "Customer")]
        [HttpGet("workout/{id}")]
        public async Task<IActionResult> LocationsByWorkout(Guid id)
        {
            try
            {
                var locations = await _locationService.LocationsByWorkoutIdAsync(id);

                return Ok(locations);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout's locations by id! Please try again later!" });

            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("addresses/{id}/{city}")]
        public async Task<IActionResult> GetAddressesByCityAndWorkout(Guid id, string city)
        {
            try
            {
                var addresses = await _locationService.AddressesByCityAndWorkoutAsync(id, city);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve addresses: {ex.Message}");
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("cities/{workoutId}")]
        public async Task<IActionResult> GetCitiesByWorkout(Guid workoutId)
        {
            try
            {
                var cities = await _locationService.CitiesByWorkoutAsync(workoutId);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve cities: {ex.Message}");
            }
        }
    }


}
