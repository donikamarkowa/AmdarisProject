using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Exceptions;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("details/{id}")]
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
