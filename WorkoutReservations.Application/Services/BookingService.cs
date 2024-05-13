using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.Models.Booking;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking, WorkoutReservationsDbContext> _bookingRepository;
        public BookingService(IGenericRepository<Booking, WorkoutReservationsDbContext> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDetailsDto> BookingDetailsAsync(Guid id)
        {
            var booking = await _bookingRepository
                .GetByWithInclude(b => b.Id == id, b => b.Workout.Trainers, b => b.Workout.Locations);

            var trainer = booking.Workout.Trainers.FirstOrDefault();
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
            return await _bookingRepository.GetById(id) != null;

        }
    }
}
