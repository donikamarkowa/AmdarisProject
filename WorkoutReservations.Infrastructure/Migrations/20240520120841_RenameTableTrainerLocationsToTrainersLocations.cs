using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutReservations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableTrainerLocationsToTrainersLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerLocations_AspNetUsers_TrainersId",
                table: "TrainerLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainerLocations_Locations_LocationsId",
                table: "TrainerLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerLocations",
                table: "TrainerLocations");

            migrationBuilder.RenameTable(
                name: "TrainerLocations",
                newName: "TrainersLocations");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerLocations_TrainersId",
                table: "TrainersLocations",
                newName: "IX_TrainersLocations_TrainersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 12, 8, 40, 918, DateTimeKind.Utc).AddTicks(9159),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 11, 42, 53, 66, DateTimeKind.Utc).AddTicks(4425));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainersLocations",
                table: "TrainersLocations",
                columns: new[] { "LocationsId", "TrainersId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_TrainersLocations_AspNetUsers_TrainersId",
                table: "TrainersLocations",
                column: "TrainersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainersLocations_Locations_LocationsId",
                table: "TrainersLocations",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainersLocations_AspNetUsers_TrainersId",
                table: "TrainersLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainersLocations_Locations_LocationsId",
                table: "TrainersLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainersLocations",
                table: "TrainersLocations");

            migrationBuilder.RenameTable(
                name: "TrainersLocations",
                newName: "TrainerLocations");

            migrationBuilder.RenameIndex(
                name: "IX_TrainersLocations_TrainersId",
                table: "TrainerLocations",
                newName: "IX_TrainerLocations_TrainersId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Rating",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 5, 20, 11, 42, 53, 66, DateTimeKind.Utc).AddTicks(4425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 5, 20, 12, 8, 40, 918, DateTimeKind.Utc).AddTicks(9159));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerLocations",
                table: "TrainerLocations",
                columns: new[] { "LocationsId", "TrainersId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerLocations_AspNetUsers_TrainersId",
                table: "TrainerLocations",
                column: "TrainersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerLocations_Locations_LocationsId",
                table: "TrainerLocations",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
