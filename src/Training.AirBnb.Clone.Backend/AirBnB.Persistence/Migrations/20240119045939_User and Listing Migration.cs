using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserandListingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedByUserId",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Listings_HostId",
                table: "Listings",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_HostId",
                table: "Listings",
                column: "HostId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_HostId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_HostId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Listings");
        }
    }
}
