using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyAndMoneyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerNight_Currency",
                table: "Listings");

            migrationBuilder.AddColumn<Guid>(
                name: "PricePerNight_CurrencyId",
                table: "Listings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listings_PricePerNight_CurrencyId",
                table: "Listings",
                column: "PricePerNight_CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Currencies_PricePerNight_CurrencyId",
                table: "Listings",
                column: "PricePerNight_CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Currencies_PricePerNight_CurrencyId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Listings_PricePerNight_CurrencyId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "PricePerNight_CurrencyId",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "PricePerNight_Currency",
                table: "Listings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
