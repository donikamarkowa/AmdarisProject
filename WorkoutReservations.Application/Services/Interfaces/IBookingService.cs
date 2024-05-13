using WorkoutReservations.Application.Models.Booking;

namespace WorkoutReservations.Application.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<bool> ExistsByIdAsync(Guid id);
        public Task<BookingDetailsDto> BookingDetailsAsync(Guid id);
        public Task Confirm(Guid id);
        public Task Cancel(Guid id);
    }
}
