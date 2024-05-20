using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IScheduleService _scheduleService;
        public BookingController(IBookingService bookingService, IScheduleService scheduleService)
        {
            _bookingService = bookingService;
            _scheduleService = scheduleService;

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Guid workoutId, Guid scheduleId)
        {
            try
            {
                var scheduleHasCapacity = await _scheduleService.HasCapacityAsync(scheduleId);
                if (!scheduleHasCapacity)
                {
                    return BadRequest(new { message = "Schedule is fully booked." });
                }

                await _bookingService.AddBookingAsync(workoutId, scheduleId);

                return Ok(new { message = "Booking created successfully." });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Failed to create booking." });
            }
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var bookingDetails = await _bookingService.BookingDetailsAsync(id);

                if (bookingDetails == null)
                {
                    return NotFound(new { Message = $"Booking with id {id} not found." });
                }

                return Ok(bookingDetails);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get booking's details! Please try again later!" });

            }
        }

    }
}
