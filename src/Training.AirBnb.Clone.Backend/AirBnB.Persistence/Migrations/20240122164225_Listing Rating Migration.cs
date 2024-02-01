using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ListingRatingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating_Accuracy",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_CheckIn",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_Cleanliness",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_Communication",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_Location",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_OverallRating",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating_Value",
                table: "Listings",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating_Accuracy",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_CheckIn",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_Cleanliness",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_Communication",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_Location",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_OverallRating",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "Rating_Value",
                table: "Listings");
        }
    }
}
