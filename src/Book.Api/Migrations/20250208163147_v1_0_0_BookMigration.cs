using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1_0_0_BookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingSagaData",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    TravelerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    HotelName = table.Column<string>(type: "text", nullable: false),
                    FlightCode = table.Column<string>(type: "text", nullable: false),
                    CarPlateNumber = table.Column<string>(type: "text", nullable: false),
                    HotelBooked = table.Column<bool>(type: "boolean", nullable: false),
                    FlightBooked = table.Column<bool>(type: "boolean", nullable: false),
                    CarRented = table.Column<bool>(type: "boolean", nullable: false),
                    BookingFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSagaData", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSagaData");
        }
    }
}
