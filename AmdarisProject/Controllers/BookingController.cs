using Microsoft.AspNetCore.Mvc;
using WorkoutReservations.Application.Services.Interfaces;

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
            catch (Exception)
            {
                return BadRequest(new { Message = "Unexpected error occurred while trying to get booking details! Please try again later!" });
            }
        }
    }
}
