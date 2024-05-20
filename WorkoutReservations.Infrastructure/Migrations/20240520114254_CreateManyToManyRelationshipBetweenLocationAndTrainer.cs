using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateManyToManyRelationshipBetweenLocationAndTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 11, 42, 53, 66, DateTimeKind.Utc).AddTicks(4425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 8, 25, 31, 314, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.CreateTable(
                name: "TrainerLocations",
                columns: table => new
                {
                    LocationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerLocations", x => new { x.LocationsId, x.TrainersId });
                    table.ForeignKey(
                        name: "FK_TrainerLocations_AspNetUsers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerLocations_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("34beea57-664e-418c-88c5-5fad2d0a10df"),
                column: "ConcurrencyStamp",
                value: "4af7b4ed-055c-457f-a715-0712f3c345f5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fa6b2fb6-0645-434e-90a9-7c9c1c9e701d"),
                column: "ConcurrencyStamp",
                value: "8e913846-586e-478f-bb71-933a7867a610");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerLocations_TrainersId",
                table: "TrainerLocations",
                column: "TrainersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerLocations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 8, 25, 31, 314, DateTimeKind.Utc).AddTicks(2174),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 11, 42, 53, 66, DateTimeKind.Utc).AddTicks(4425));

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
    }
}
