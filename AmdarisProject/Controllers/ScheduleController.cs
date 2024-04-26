using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.Services;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService; 
        }

        [HttpGet("location/{id}")]
        public async Task<IActionResult> GetLocationsByWorkoutId(Guid id)
        {
            try
            {
                var schedules = await _scheduleService.SchedulesByLocationIdAsync(id);

                return Ok(schedules);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get location's schedules by location id! Please try again later!" });

            }
        }

        [HttpGet("trainer/workout/{id}")]
        public async Task<IActionResult> GetLocationsByTrainerId(Guid id)
        {
            try
            {
                var locations = await _scheduleService.SchedulesByTrainerIdAsync(id);

                return Ok(locations);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get trainer's schedules by id! Please try again later!" });

            }
        }
    }
}
