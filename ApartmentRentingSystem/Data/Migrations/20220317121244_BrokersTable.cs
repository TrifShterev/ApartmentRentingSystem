using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentingSystem.Data.Migrations
{
    public partial class BrokersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrokerId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brokers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_BrokerId",
                table: "Apartments",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UserId",
                table: "Brokers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Brokers_BrokerId",
                table: "Apartments",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Brokers_BrokerId",
                table: "Apartments");

            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_BrokerId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "Apartments");
        }
    }
}
