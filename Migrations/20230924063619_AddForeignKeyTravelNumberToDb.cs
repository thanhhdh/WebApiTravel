using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTravel.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyTravelNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelId",
                table: "TravelNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 36, 18, 984, DateTimeKind.Local).AddTicks(9479));

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 36, 18, 984, DateTimeKind.Local).AddTicks(9490));

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 36, 18, 984, DateTimeKind.Local).AddTicks(9492));

            migrationBuilder.CreateIndex(
                name: "IX_TravelNumbers_TravelId",
                table: "TravelNumbers",
                column: "TravelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelNumbers_Travels_TravelId",
                table: "TravelNumbers",
                column: "TravelId",
                principalTable: "Travels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelNumbers_Travels_TravelId",
                table: "TravelNumbers");

            migrationBuilder.DropIndex(
                name: "IX_TravelNumbers_TravelId",
                table: "TravelNumbers");

            migrationBuilder.DropColumn(
                name: "TravelId",
                table: "TravelNumbers");

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 12, 2, 125, DateTimeKind.Local).AddTicks(948));

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 12, 2, 125, DateTimeKind.Local).AddTicks(959));

            migrationBuilder.UpdateData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 9, 24, 13, 12, 2, 125, DateTimeKind.Local).AddTicks(961));
        }
    }
}
