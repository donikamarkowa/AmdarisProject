using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutReservations.Domain.Entities;


namespace WorkoutReservations.Infrastructure.Database
{
    public class WorkoutReservationsDbContext : IdentityDbContext<User, Role, Guid>
    {
        public WorkoutReservationsDbContext()
        {
            
        }
        public WorkoutReservationsDbContext(DbContextOptions<WorkoutReservationsDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Booking>()
                .Property(b => b.Status)
                .HasConversion<string>();

            builder
                .Entity<Workout>()
                .Property(w => w.Status)
                .HasConversion<string>();

            builder
                .Entity<Rating>()
                .Property(r => r.Date)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Workout>()
                .HasOne(w => w.WorkoutCategory)
                .WithMany(wc => wc.Workouts)
                .HasForeignKey(w => w.WorkoutCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Schedule>()
                .HasOne(s => s.Workout)
                .WithMany(w => w.Schedules)
                .HasForeignKey(w => w.WorkoutId)
                .OnDelete(DeleteBehavior.NoAction);


            //builder
            //    .Entity<Workout>()
            //    .HasMany(w => w.Tags)
            //    .WithMany(t => t.Workouts);



            //Seed many to many tables

            builder
                .Entity<Workout>()
                .HasMany(w => w.Locations)
                .WithMany(l => l.Workouts)
                .UsingEntity(j => j.ToTable("LocationsWorkouts"));

            builder
               .Entity<Workout>()
               .HasMany(w => w.Trainers)
               .WithMany(t => t.Workouts)
               .UsingEntity(j => j.ToTable("TrainersWorkouts"));

            builder
               .Entity<Location>()
               .HasMany(l => l.Trainers)
               .WithMany(t => t.Locations)
               .UsingEntity(j => j.ToTable("TrainersLocations"));


            builder
                .Entity<Workout>()
                .HasMany(w => w.Trainers)
                .WithMany(t => t.Workouts)
                .UsingEntity<Dictionary<string, object>>("TrainersWorkouts",
                    w => w.HasOne<User>().WithMany().HasForeignKey("TrainersId"),
                    l => l.HasOne<Workout>().WithMany().HasForeignKey("WorkoutsId"),
                    j =>
                    {
                        j.HasKey("TrainersId", "WorkoutsId");
                        j.HasData(//TODO: extract seeding in DataSeeder
                            new { TrainersId = Guid.Parse("34beea57-664e-418c-88c5-5fad2d0a10df"),
                                  WorkoutsId = Guid.Parse("fa405850-875f-402d-9f57-72712d702a3f") }, //body shaping
                            new { TrainersId = Guid.Parse("34beea57-664e-418c-88c5-5fad2d0a10df"),
                                  WorkoutsId = Guid.Parse("65283b8b-ffc4-4893-9d5e-040b3270ac91") }); //aerobic
                    });

            builder
               .Entity<Workout>()
               .HasMany(w => w.Locations)
               .WithMany(t => t.Workouts)
               .UsingEntity<Dictionary<string, object>>("LocationsWorkouts",
                   w => w.HasOne<Location>().WithMany().HasForeignKey("LocationsId"),
                   l => l.HasOne<Workout>().WithMany().HasForeignKey("WorkoutsId"),
                   j =>
                   {
                       j.HasKey("LocationsId", "WorkoutsId");
                       j.HasData(
                           new
                           {
                               LocationsId = Guid.Parse("d887a48c-5163-45cf-b097-39f3e1bba52e"), //Sofia
                               WorkoutsId = Guid.Parse("fa405850-875f-402d-9f57-72712d702a3f") //body shaping
                           }, 
                           new
                           {
                               LocationsId = Guid.Parse("753ed14c-c702-445e-8f3d-8c08f843e7be"), //Plovdiv
                               WorkoutsId = Guid.Parse("65283b8b-ffc4-4893-9d5e-040b3270ac91") //aerobic
                           }); 
                   });

            base.OnModelCreating(builder);
        }
        

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Location> Locations { get; set; }

        //public DbSet<Rating> Ratings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        //public DbSet<Tag> Tags { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutCategory> WorkoutCategories { get; set; }
    }
}
