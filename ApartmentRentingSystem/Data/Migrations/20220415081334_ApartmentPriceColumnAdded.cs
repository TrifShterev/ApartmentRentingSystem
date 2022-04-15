using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentingSystem.Data.Migrations
{
    public partial class ApartmentPriceColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Apartments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Apartments");
        }
    }
}
