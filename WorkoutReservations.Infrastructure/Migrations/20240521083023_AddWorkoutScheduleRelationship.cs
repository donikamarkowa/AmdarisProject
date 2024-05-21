using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutScheduleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutId",
                table: "Schedules",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 21, 8, 30, 21, 766, DateTimeKind.Utc).AddTicks(9925),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 12, 8, 40, 918, DateTimeKind.Utc).AddTicks(9159));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "ab971811-14a3-4f28-b917-573564c828e8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "14e0ad7b-3e38-4f1d-b5a3-dd4777d5ad2c");

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("353070a1-6234-4341-be19-4e9c28cfdb16"),
                column: "WorkoutId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("62829231-8f59-4316-b7b5-70ce7e309008"),
                column: "WorkoutId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_WorkoutId",
                table: "Schedules",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Workouts_WorkoutId",
                table: "Schedules",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Workouts_WorkoutId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_WorkoutId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Schedules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 12, 8, 40, 918, DateTimeKind.Utc).AddTicks(9159),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 21, 8, 30, 21, 766, DateTimeKind.Utc).AddTicks(9925));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "e3ce31aa-8540-4a8c-b7ad-a96ecfeaae99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "d3d91663-9bd0-42cb-a3b5-b219a06231bc");
        }
    }
}
