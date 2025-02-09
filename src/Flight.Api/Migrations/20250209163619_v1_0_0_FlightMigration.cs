using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flight.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1_0_0_FlightMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TravelerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRegistration", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightRegistration_Code_From_To",
                table: "FlightRegistration",
                columns: new[] { "Code", "From", "To" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightRegistration");
        }
    }
}
