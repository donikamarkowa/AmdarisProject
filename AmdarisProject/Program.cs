

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Application.Services;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;
using WorkoutReservations.Infrastructure.Repositories;


namespace AmdarisProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<WorkoutReservationsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WorkoutReservationsDbContext>()
                .AddDefaultTokenProviders();


            //builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped<IWorkoutService, WorkoutService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IWorkoutCategoryService, WorkoutCategoryService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
