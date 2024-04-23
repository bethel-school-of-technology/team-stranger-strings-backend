using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace team_stranger_strings_backend.Migrations
{
    /// <inheritdoc />
    public partial class UserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: 1,
                column: "UserEmail",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Vehicles");
        }
    }
}
