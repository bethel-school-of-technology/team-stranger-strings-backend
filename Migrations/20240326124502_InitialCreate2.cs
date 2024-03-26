using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace team_stranger_strings_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "Colour", "Make", "Model", "Photo", "Price", "Year" },
                values: new object[] { 1, "blue", "Audi", "Q8", "https://www.motortrend.com/uploads/2022/09/2023-Audi-RS-Q8-PVOTY22-24.jpg", 79100, 2023 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: 1);
        }
    }
}
