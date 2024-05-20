using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusEnumToWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 8, 25, 31, 314, DateTimeKind.Utc).AddTicks(2174),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 14, 12, 16, 36, 883, DateTimeKind.Utc).AddTicks(9481));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "62a64996-cffd-44b5-b03b-2dcac3b745e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "03640ee9-5b31-43b7-bf99-10ff20181a4e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 14, 12, 16, 36, 883, DateTimeKind.Utc).AddTicks(9481),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 8, 25, 31, 314, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "e3cf73af-34c7-4676-8739-88ff12002000");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "219c98e8-64f0-44cf-a281-38a8852c9484");
        }
    }
}
