using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntityGenderNullableAndRemoveReferenceToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5228b6f2-322a-4554-a534-e3cc61cbcc68"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("d887a48c-5163-45cf-b097-39f3e1bba52e"));

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("353070a1-6234-4341-be19-4e9c28cfdb16"));

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("62829231-8f59-4316-b7b5-70ce7e309008"));

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: new Guid("65283b8b-ffc4-4893-9d5e-040b3270ac91"));

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: new Guid("fa405850-875f-402d-9f57-72712d702a3f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"));

            migrationBuilder.DeleteData(
                table: "WorkoutCategories",
                keyColumn: "Id",
                keyValue: new Guid("4939cc2e-1e08-46e1-a573-9767d025f731"));

            migrationBuilder.DeleteData(
                table: "WorkoutCategories",
                keyColumn: "Id",
                keyValue: new Guid("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eae5654b-fd83-4e58-b380-e10eb18498c1"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 12, 14, 20, 54, 232, DateTimeKind.Utc).AddTicks(1012),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 21, 8, 30, 21, 766, DateTimeKind.Utc).AddTicks(9925));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 21, 8, 30, 21, 766, DateTimeKind.Utc).AddTicks(9925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 12, 14, 20, 54, 232, DateTimeKind.Utc).AddTicks(1012));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"), null, "Customer role for individuals interested in booking workouts and viewing schedules.", "Customer", "CUSTOMER" },
                    { new Guid("5228b6f2-322a-4554-a534-e3cc61cbcc68"), null, "Administrator role with full access to system functionalities.", "Admin", "ADMIN" },
                    { new Guid("eae5654b-fd83-4e58-b380-e10eb18498c1"), null, "Trainer role responsible for creating and managing workouts, schedules, and user interactions.", "Trainer", "TRAINER" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "Latitude", "Longitude", "MaxCapacity", "Region", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"), "58 Tsar Simeon St., 2nd floor, Styler building", "Plovdiv", "42.12699522870005", "24.793825738752535", 15, "Trakia", "4023" },
                    { new Guid("d887a48c-5163-45cf-b097-39f3e1bba52e"), "Vasil Levski Stadium, sector A, entrance. 1, hall 2", "Sofia", "42.68821875816085", "3.334551002232402", 20, "Lozenets", "1164" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4939cc2e-1e08-46e1-a573-9767d025f731"), "Aerobic" },
                    { new Guid("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c"), "Strength" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "Bio", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "Height", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"), 0, 30, "I'm a dedicated fitness professional with a passion for aerobic and strength training. With years of experience, I specialize in crafting personalized workout programs to help clients achieve their fitness goals. Whether you're aiming to improve cardiovascular health, build strength, or enhance overall fitness, I'm here to support you every step of the way. My dynamic training sessions are tailored to your needs, combining effective aerobic exercises with targeted strength training techniques. Let's work together to unlock your full potential and achieve lasting results!\r\n", "ab971811-14a3-4f28-b917-573564c828e8", "alexandra.trainer@gmail.com", false, "Alexandra", "Female", 1.6799999999999999, "Petrova", false, null, null, null, null, "0897689004", false, "https://media.istockphoto.com/id/876704262/photo/smiling-female-fitness-instructor-with-clipboard-showing-thumb-up-in-gym.jpg?s=1024x1024&w=is&k=20&c=zVGwh8CbigQYwVahGnn9DK48_zYEJyJ7ab7ptOL4bUQ=", new Guid("eae5654b-fd83-4e58-b380-e10eb18498c1"), null, false, null, 50.0 },
                    { new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"), 0, 23, null, "14e0ad7b-3e38-4f1d-b5a3-dd4777d5ad2c", "kalina.customer@gmail.com", false, "Kalina", "Female", 1.7, "Ivanova", false, null, null, null, null, "0899761124", false, null, new Guid("1ce6918e-f21f-494b-aa5e-cb26a78a5b3e"), null, false, null, 62.0 }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Description", "Duration", "EquipmentNeeded", "Gender", "IntensityLevel", "Picture", "Price", "RecommendedFrequency", "Status", "Title", "WorkoutCategoryId" },
                values: new object[,]
                {
                    { new Guid("65283b8b-ffc4-4893-9d5e-040b3270ac91"), "Aerobic workouts, also known as cardio exercises, are dynamic activities that elevate your heart rate and breathing rate to enhance cardiovascular health and endurance. These exercises typically involve repetitive movements of large muscle groups over an extended period. Aerobic workouts can encompass a wide range of activities, such as brisk walking, jogging, cycling, and jumping rope. By promoting oxygen circulation and boosting stamina, aerobic workouts contribute to overall fitness and well-being.", new TimeSpan(0, 0, 45, 0, 0), "Comfortable clothing and shoes", "Female", 4, "https://media.istockphoto.com/id/841069776/photo/happy-people-in-an-aerobics-class-at-the-gym.jpg?s=612x612&w=0&k=20&c=Msbb_TNBDZWWZfnuaZubcgE7Qa-qimYrl4D3aFQv9PY=", 8m, "5 times per week", "Active", "Aerobic Fitness", new Guid("4939cc2e-1e08-46e1-a573-9767d025f731") },
                    { new Guid("fa405850-875f-402d-9f57-72712d702a3f"), "A body shaping workout focuses on toning and sculpting muscles to enhance overall body appearance and symmetry. It typically involves a combination of strength training exercises, cardio exercises, and flexibility training targeted at specific muscle groups to achieve a leaner, more defined physique.", new TimeSpan(0, 1, 0, 0, 0), "Comfortable clothing and shoes", "All", 5, "https://media.istockphoto.com/id/1149241593/photo/man-doing-cross-training-exercise-with-rope.jpg?s=1024x1024&w=is&k=20&c=La_Z7H2yY9DTOcJnWDQDh6K6HIjPw6eWkfEIiXquTdw=", 10m, "3 times per week", "Active", "Body shaping", new Guid("8a07d8ea-51dd-4cd5-9daf-e5acc0d6e29c") }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Capacity", "Date", "LocationId", "UserId", "WorkoutId" },
                values: new object[,]
                {
                    { new Guid("353070a1-6234-4341-be19-4e9c28cfdb16"), 10, new DateTime(2024, 6, 25, 8, 0, 0, 0, DateTimeKind.Unspecified), new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"), new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("62829231-8f59-4316-b7b5-70ce7e309008"), 5, new DateTime(2024, 6, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), new Guid("753ed14c-c702-445e-8f3d-8c08f843e7be"), new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
