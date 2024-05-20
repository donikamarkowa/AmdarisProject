using WorkoutReservations.Application.Models.Booking;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Domain.Enums;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;

namespace WorkoutReservations.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking, WorkoutReservationsDbContext> _bookingRepository;
        private readonly IGenericRepository<Schedule, WorkoutReservationsDbContext> _scheduleRepository;
        public BookingService(IGenericRepository<Booking, WorkoutReservationsDbContext> bookingRepository, IGenericRepository<Schedule, WorkoutReservationsDbContext> scheduleRepository)
        {
            _bookingRepository = bookingRepository;
            _scheduleRepository = scheduleRepository;
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
        public async Task AddBookingAsync(Guid workoutId, Guid scheduleId)
        {
            var schedule = await _scheduleRepository.GetById(scheduleId);
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Status = BookingStatus.Created,
                WorkoutId = workoutId,
                ScheduleId = scheduleId
            };

            schedule.Capacity--;
            _scheduleRepository.Edit(schedule);
            await _bookingRepository.Add(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        public async Task CancelAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.GetById(bookingId);

            booking.Status = BookingStatus.CancelledByUser;
            _bookingRepository.Edit(booking);

            await _bookingRepository.SaveChangesAsync();
        }
        public async Task ConfirmAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.GetById(bookingId);

            booking.Status = BookingStatus.Confirmed;
            _bookingRepository.Edit(booking);

            await _bookingRepository.SaveChangesAsync();
        }


        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _bookingRepository.GetById(id) != null;

        }

    }
}
