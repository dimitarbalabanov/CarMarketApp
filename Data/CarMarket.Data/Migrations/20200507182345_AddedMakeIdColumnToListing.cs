namespace CarMarket.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedMakeIdColumnToListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "Listings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_MakeId",
                table: "Listings",
                column: "MakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Makes_MakeId",
                table: "Listings",
                column: "MakeId",
                principalTable: "Makes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Makes_MakeId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_MakeId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "Listings");
        }
    }
}
