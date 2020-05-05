using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMarket.Data.Migrations
{
    public partial class AddedSeatsCountPropertyInListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatsCount",
                table: "Listings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatsCount",
                table: "Listings");
        }
    }
}
