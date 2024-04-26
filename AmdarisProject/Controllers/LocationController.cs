using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("workkout/{id}")]
        public async Task<IActionResult> GetLocationsByWorkoutId(Guid id)
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
                return BadRequest(new { Message = "Unexpected error occurred while trying to get workout by id! Please try again later!" });

            }
        }
    }


}
