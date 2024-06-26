﻿using System.Globalization;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Domain.Enums;

namespace WorkoutReservations.Infrastructure.Database
{
    public class DataSeeder
    {
        private readonly WorkoutReservationsDbContext _dbContext;
        public DataSeeder(WorkoutReservationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            SeedRoles();
            SeedWorkoutCategories();
            SeedLocations();
            SeedUsers();
            SeedSchedules();
            SeedWorkouts();

            _dbContext.SaveChangesAsync();
        }

        private void SeedRoles()
        {
            _dbContext.Roles.AddRange(
                new Role
                {
                    Id = Guid.Parse("5228b6f2-322a-4554-a534-e3cc61cbcc68"),
                    Name = "Admin",
                    Description = "Administrator role with full access to system functionalities.",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = Guid.Parse("eae5654b-fd83-4e58-b380-e10eb18498c1"),
                    Name = "Trainer",
                    Description = "Trainer role responsible for creating and managing workouts, schedules, and user interactions.",
                    NormalizedName = "TRAINER"
                },
                new Role
                {
                    Id = Guid.Parse("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"),
                    Name = "Customer",
                    Description = "Customer role for individuals interested in booking workouts and viewing schedules.",
                    NormalizedName = "CUSTOMER"
                });
        }

        private void SeedWorkouts()
        {
            _dbContext.Workouts.AddRange(
                new Workout
                {
                    Id = Guid.Parse("fa405850-875f-402d-9f57-72712d702a3f"),
                    Title = "Body shaping",
                    Description = "A body shaping workout focuses on toning and sculpting muscles to enhance overall body appearance and symmetry. It typically involves a combination of strength training exercises, cardio exercises, and flexibility training targeted at specific muscle groups to achieve a leaner, more defined physique.",
                    Duration = TimeSpan.FromMinutes(60),
                    Gender = "All",
                    IntensityLevel = 5,
                    Status = WorkoutStatus.Active,
                    Picture = "https://media.istockphoto.com/id/1149241593/photo/man-doing-cross-training-exercise-with-rope.jpg?s=1024x1024&w=is&k=20&c=La_Z7H2yY9DTOcJnWDQDh6K6HIjPw6eWkfEIiXquTdw=",
                    RecommendedFrequency = "3 times per week",
                    Price = 10m,
                    EquipmentNeeded = "Comfortable clothing and shoes",
                    WorkoutCategoryId = Guid.Parse("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c") //Strength
                },
                new Workout
                {
                    Id = Guid.Parse("65283b8b-ffc4-4893-9d5e-040b3270ac91"),
                    Title = "Aerobic Fitness",
                    Description = "Aerobic workouts, also known as cardio exercises, are dynamic activities that elevate your heart rate and breathing rate to enhance cardiovascular health and endurance. These exercises typically involve repetitive movements of large muscle groups over an extended period. Aerobic workouts can encompass a wide range of activities, such as brisk walking, jogging, cycling, and jumping rope. By promoting oxygen circulation and boosting stamina, aerobic workouts contribute to overall fitness and well-being.",
                    Duration = TimeSpan.FromMinutes(45),
                    Gender = "Female",
                    IntensityLevel = 4,
                    Status = WorkoutStatus.Active,
                    Picture = "https://media.istockphoto.com/id/841069776/photo/happy-people-in-an-aerobics-class-at-the-gym.jpg?s=612x612&w=0&k=20&c=Msbb_TNBDZWWZfnuaZubcgE7Qa-qimYrl4D3aFQv9PY=",
                    RecommendedFrequency = "5 times per week",
                    Price = 8m,
                    EquipmentNeeded = "Comfortable clothing and shoes",
                    WorkoutCategoryId = Guid.Parse("4939cc2e-1e08-46e1-a573-9767d025f731") //Aerobic
                });
        }

        private void SeedWorkoutCategories()
        {
            _dbContext.WorkoutCategories.AddRange(
                 new WorkoutCategory { Id = Guid.Parse("4939cc2e-1e08-46e1-a573-9767d025f731"), Name = "Aerobic" },
                 new WorkoutCategory { Id = Guid.Parse("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c"), Name = "Strength" });
        }

        private void SeedUsers()
        {
            _dbContext.Users.AddRange(
                new User
                {
                    Id = Guid.Parse("34beea57-664e-418c-88c5-5fad2d0a10df"),
                    FirstName = "Alexandra",
                    LastName = "Petrova",
                    Age = 30,
                    Gender = "Female",
                    Bio = "I'm a dedicated fitness professional with a passion for aerobic and strength training. With years of experience, I specialize in crafting personalized workout programs to help clients achieve their fitness goals. Whether you're aiming to improve cardiovascular health, build strength, or enhance overall fitness, I'm here to support you every step of the way. My dynamic training sessions are tailored to your needs, combining effective aerobic exercises with targeted strength training techniques. Let's work together to unlock your full potential and achieve lasting results!\r\n",
                    Weight = 50,
                    Height = 1.68,
                    Email = "alexandra.trainer@gmail.com",
                    PhoneNumber = "0897689004",
                    Picture = "https://media.istockphoto.com/id/876704262/photo/smiling-female-fitness-instructor-with-clipboard-showing-thumb-up-in-gym.jpg?s=1024x1024&w=is&k=20&c=zVGwh8CbigQYwVahGnn9DK48_zYEJyJ7ab7ptOL4bUQ="
                },
                new User
                {
                    Id = Guid.Parse("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                    FirstName = "Kalina",
                    LastName = "Ivanova",
                    Age = 23,
                    Gender = "Female",
                    Weight = 62,
                    Height = 1.70,
                    Email = "kalina.customer@gmail.com",
                    PhoneNumber = "0899761124"
                });              
        }

        private void SeedSchedules()
        {
            _dbContext.Schedules.AddRange(
                new Schedule
                {
                    Id = Guid.Parse("353070a1-6234-4341-be19-4e9c28cfdb16"),
                    Date = DateTime.Parse("06/25/2024 08:00", CultureInfo.InvariantCulture),
                    Capacity = 10,
                    LocationId = Guid.Parse("753ed14c-c702-445e-8f3d-8c08f843e7be"), //Plovdiv
                    UserId = Guid.Parse("34beea57-664e-418c-88c5-5fad2d0a10df") //trainer
                },
                new Schedule
                {
                    Id = Guid.Parse("62829231-8f59-4316-b7b5-70ce7e309008"),
                    Date = DateTime.Parse("06/27/2024 19:00", CultureInfo.InvariantCulture),
                    Capacity = 5,
                    LocationId = Guid.Parse("753ed14c-c702-445e-8f3d-8c08f843e7be"), //Plovdiv
                    UserId = Guid.Parse("34beea57-664e-418c-88c5-5fad2d0a10df") //trainer
                });
        }

        private void SeedLocations()
        {
            _dbContext.Locations.AddRange(
                 new Location
                 {
                     Id = Guid.Parse("d887a48c-5163-45cf-b097-39f3e1bba52e"),
                     City = "Sofia",
                     Address = "Vasil Levski Stadium, sector A, entrance. 1, hall 2",
                     Region = "Lozenets",
                     Latitude = "42.68821875816085",
                     Longitude = "3.334551002232402",
                     MaxCapacity = 20,
                     ZipCode = "1164"
                 },
                new Location
                {
                    Id = Guid.Parse("753ed14c-c702-445e-8f3d-8c08f843e7be"),
                    City = "Plovdiv",
                    Address = "58 Tsar Simeon St., 2nd floor, Styler building",
                    Region = "Trakia",
                    Latitude = "42.12699522870005",
                    Longitude = "24.793825738752535",
                    MaxCapacity = 15,
                    ZipCode = "4023"
                });
        }
    }
}
