using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.Models.Booking;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Infrastructure.Database;

namespace WorkoutReservations.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        public BookingService(WorkoutReservationsDbContext workoutReservationsDbContext)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
        }

        public async Task<BookingDetailsDto> BookingDetailsAsync(Guid id)
        {
            var booking = await _workoutReservationsDbContext
                .Bookings
                .Include(b => b.Workout)  
                .ThenInclude(w => w.Trainers)
                .Include(b => b.Workout)  
                .ThenInclude(w => w.Locations)
                .FirstOrDefaultAsync(b => b.Id == id);

            var trainer = booking!.Workout.Trainers.FirstOrDefault();
            var location = booking.Workout.Locations.FirstOrDefault();

            var dto = new BookingDetailsDto
            {
                Id = booking.Id.ToString(),
                WorkoutTitle = booking.Workout.Title,
                City = location!.City,
                Address = location!.Address,
                TrainerFullName = trainer != null ? $"{trainer.FirstName} {trainer.LastName}" : null!,
                TrainerPhoneNumber = trainer!.PhoneNumber!
            };

            return dto;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _workoutReservationsDbContext
                .Bookings
                .AnyAsync(b => b.Id == id);
        }
    }
}
