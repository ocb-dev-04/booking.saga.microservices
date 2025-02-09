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
                    HotelRegistrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    HotelName = table.Column<string>(type: "text", nullable: false),
                    HotelBooked = table.Column<bool>(type: "boolean", nullable: false),
                    FlightRegistrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FlightFrom = table.Column<string>(type: "text", nullable: false),
                    FlightTo = table.Column<string>(type: "text", nullable: false),
                    FlightCode = table.Column<string>(type: "text", nullable: false),
                    FlightBooked = table.Column<bool>(type: "boolean", nullable: false),
                    CarRegistrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CarPlateNumber = table.Column<string>(type: "text", nullable: false),
                    CarRented = table.Column<bool>(type: "boolean", nullable: false),
                    BookingFinished = table.Column<bool>(type: "boolean", nullable: false),
                    SuccessOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    SomeErrorOcurred = table.Column<bool>(type: "boolean", nullable: false),
                    FailedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSagaData", x => x.CorrelationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingSagaData_CorrelationId_TravelerId_SuccessOnUtc_Faile~",
                table: "BookingSagaData",
                columns: new[] { "CorrelationId", "TravelerId", "SuccessOnUtc", "FailedOnUtc", "SomeErrorOcurred" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingSagaData");
        }
    }
}
