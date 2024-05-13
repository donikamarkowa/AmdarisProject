﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutReservations.Infrastructure.Database;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    [DbContext(typeof(WorkoutReservationsDbContext))]
    [Migration("20240513080958_AddStatusFieldToBookingEntity")]
    partial class AddStatusFieldToBookingEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LocationsWorkouts", b =>
                {
                    b.Property<Guid>("LocationsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkoutsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LocationsId", "WorkoutsId");

                    b.HasIndex("WorkoutsId");

                    b.ToTable("LocationsWorkouts");

                    b.HasData(
                        new
                        {
                            LocationsId = new Guid("d887a48c-5163-45cf-b097-39f3e1bba52e"),
                            WorkoutsId = new Guid("fa405850-875f-402d-9f57-72712d702a3f")
                        },
                        new
                        {
                            LocationsId = new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"),
                            WorkoutsId = new Guid("65283b8b-ffc4-4893-9d5e-040b3270ac91")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TrainersWorkouts", b =>
                {
                    b.Property<Guid>("TrainersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkoutsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TrainersId", "WorkoutsId");

                    b.HasIndex("WorkoutsId");

                    b.ToTable("TrainersWorkouts");

                    b.HasData(
                        new
                        {
                            TrainersId = new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                            WorkoutsId = new Guid("fa405850-875f-402d-9f57-72712d702a3f")
                        },
                        new
                        {
                            TrainersId = new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                            WorkoutsId = new Guid("65283b8b-ffc4-4893-9d5e-040b3270ac91")
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d887a48c-5163-45cf-b097-39f3e1bba52e"),
                            Address = "Vasil Levski Stadium, sector A, entrance. 1, hall 2",
                            City = "Sofia",
                            Latitude = "42.68821875816085",
                            Longitude = "3.334551002232402",
                            MaxCapacity = 20,
                            Region = "Lozenets",
                            ZipCode = "1164"
                        },
                        new
                        {
                            Id = new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"),
                            Address = "58 Tsar Simeon St., 2nd floor, Styler building",
                            City = "Plovdiv",
                            Latitude = "42.12699522870005",
                            Longitude = "24.793825738752535",
                            MaxCapacity = 15,
                            Region = "Trakia",
                            ZipCode = "4023"
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 5, 13, 8, 9, 57, 911, DateTimeKind.Utc).AddTicks(4992));

                    b.Property<int>("RatingCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5228b6f2-322a-4554-a534-e3cc61cbcc68"),
                            Description = "Administrator role with full access to system functionalities.",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("eae5654b-fd83-4e58-b380-e10eb18498c1"),
                            Description = "Trainer role responsible for creating and managing workouts, schedules, and user interactions.",
                            Name = "Trainer"
                        },
                        new
                        {
                            Id = new Guid("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"),
                            Description = "Customer role for individuals interested in booking workouts and viewing schedules.",
                            Name = "Customer"
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Schedules");

                    b.HasData(
                        new
                        {
                            Id = new Guid("353070a1-6234-4341-be19-4e9c28cfdb16"),
                            Capacity = 10,
                            Date = new DateTime(2024, 6, 25, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            LocationId = new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"),
                            UserId = new Guid("34beea57-664e-418c-88c5-5fad2d0a10df")
                        },
                        new
                        {
                            Id = new Guid("62829231-8f59-4316-b7b5-70ce7e309008"),
                            Capacity = 5,
                            Date = new DateTime(2024, 6, 27, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            LocationId = new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"),
                            UserId = new Guid("34beea57-664e-418c-88c5-5fad2d0a10df")
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                            AccessFailedCount = 0,
                            Age = 30,
                            Bio = "I'm a dedicated fitness professional with a passion for aerobic and strength training. With years of experience, I specialize in crafting personalized workout programs to help clients achieve their fitness goals. Whether you're aiming to improve cardiovascular health, build strength, or enhance overall fitness, I'm here to support you every step of the way. My dynamic training sessions are tailored to your needs, combining effective aerobic exercises with targeted strength training techniques. Let's work together to unlock your full potential and achieve lasting results!\r\n",
                            ConcurrencyStamp = "7f402240-cee4-499e-8540-7f1a2f3ac1dd",
                            Email = "alexandra.trainer@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Alexandra",
                            Gender = "Female",
                            Height = 1.6799999999999999,
                            LastName = "Petrova",
                            LockoutEnabled = false,
                            PhoneNumber = "0897689004",
                            PhoneNumberConfirmed = false,
                            Picture = "https://media.istockphoto.com/id/876704262/photo/smiling-female-fitness-instructor-with-clipboard-showing-thumb-up-in-gym.jpg?s=1024x1024&w=is&k=20&c=zVGwh8CbigQYwVahGnn9DK48_zYEJyJ7ab7ptOL4bUQ=",
                            RoleId = new Guid("eae5654b-fd83-4e58-b380-e10eb18498c1"),
                            TwoFactorEnabled = false,
                            Weight = 50.0
                        },
                        new
                        {
                            Id = new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                            AccessFailedCount = 0,
                            Age = 23,
                            ConcurrencyStamp = "c90d09cd-776d-4120-ac75-961cbc27200a",
                            Email = "kalina.customer@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Kalina",
                            Gender = "Female",
                            Height = 1.7,
                            LastName = "Ivanova",
                            LockoutEnabled = false,
                            PhoneNumber = "0899761124",
                            PhoneNumberConfirmed = false,
                            RoleId = new Guid("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"),
                            TwoFactorEnabled = false,
                            Weight = 62.0
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("EquipmentNeeded")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IntensityLevel")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecommendedFrequency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WorkoutCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutCategoryId");

                    b.ToTable("Workouts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa405850-875f-402d-9f57-72712d702a3f"),
                            Description = "A body shaping workout focuses on toning and sculpting muscles to enhance overall body appearance and symmetry. It typically involves a combination of strength training exercises, cardio exercises, and flexibility training targeted at specific muscle groups to achieve a leaner, more defined physique.",
                            Duration = new TimeSpan(0, 1, 0, 0, 0),
                            EquipmentNeeded = "Comfortable clothing and shoes",
                            Gender = "All",
                            IntensityLevel = 5,
                            Picture = "https://media.istockphoto.com/id/1149241593/photo/man-doing-cross-training-exercise-with-rope.jpg?s=1024x1024&w=is&k=20&c=La_Z7H2yY9DTOcJnWDQDh6K6HIjPw6eWkfEIiXquTdw=",
                            Price = 10m,
                            RecommendedFrequency = "3 times per week",
                            Status = "Active",
                            Title = "Body shaping",
                            WorkoutCategoryId = new Guid("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c")
                        },
                        new
                        {
                            Id = new Guid("65283b8b-ffc4-4893-9d5e-040b3270ac91"),
                            Description = "Aerobic workouts, also known as cardio exercises, are dynamic activities that elevate your heart rate and breathing rate to enhance cardiovascular health and endurance. These exercises typically involve repetitive movements of large muscle groups over an extended period. Aerobic workouts can encompass a wide range of activities, such as brisk walking, jogging, cycling, and jumping rope. By promoting oxygen circulation and boosting stamina, aerobic workouts contribute to overall fitness and well-being.",
                            Duration = new TimeSpan(0, 0, 45, 0, 0),
                            EquipmentNeeded = "Comfortable clothing and shoes",
                            Gender = "Female",
                            IntensityLevel = 4,
                            Picture = "https://media.istockphoto.com/id/841069776/photo/happy-people-in-an-aerobics-class-at-the-gym.jpg?s=612x612&w=0&k=20&c=Msbb_TNBDZWWZfnuaZubcgE7Qa-qimYrl4D3aFQv9PY=",
                            Price = 8m,
                            RecommendedFrequency = "5 times per week",
                            Status = "Active",
                            Title = "Aerobic Fitness",
                            WorkoutCategoryId = new Guid("4939cc2e-1e08-46e1-a573-9767d025f731")
                        });
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.WorkoutCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkoutCategories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4939cc2e-1e08-46e1-a573-9767d025f731"),
                            Name = "Aerobic"
                        },
                        new
                        {
                            Id = new Guid("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c"),
                            Name = "Strength"
                        });
                });

            modelBuilder.Entity("LocationsWorkouts", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainersWorkouts", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("TrainersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Booking", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Rating", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Schedule", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutReservations.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.User", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Workout", b =>
                {
                    b.HasOne("WorkoutReservations.Domain.Entities.WorkoutCategory", "WorkoutCategory")
                        .WithMany("Workouts")
                        .HasForeignKey("WorkoutCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("WorkoutCategory");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WorkoutReservations.Domain.Entities.WorkoutCategory", b =>
                {
                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
