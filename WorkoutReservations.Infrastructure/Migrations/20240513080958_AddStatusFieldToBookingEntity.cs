using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusFieldToBookingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 13, 8, 9, 57, 911, DateTimeKind.Utc).AddTicks(4992),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 12, 28, 58, 933, DateTimeKind.Utc).AddTicks(2981));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "7f402240-cee4-499e-8540-7f1a2f3ac1dd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "c90d09cd-776d-4120-ac75-961cbc27200a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 12, 28, 58, 933, DateTimeKind.Utc).AddTicks(2981),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 13, 8, 9, 57, 911, DateTimeKind.Utc).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "0a8b8204-8187-41f4-a117-65a9e0245fbf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "e62c427d-391f-48fa-9354-2b9646bd1394");
        }
    }
}
