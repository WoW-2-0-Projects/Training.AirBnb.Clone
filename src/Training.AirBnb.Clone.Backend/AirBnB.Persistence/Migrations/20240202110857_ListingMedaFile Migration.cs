using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ListingMedaFileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListingMediaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<byte>(type: "smallint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingMediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingMediaFiles_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingMediaFiles_StorageFiles_StorageFileId",
                        column: x => x.StorageFileId,
                        principalTable: "StorageFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingMediaFiles_ListingId",
                table: "ListingMediaFiles",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingMediaFiles_StorageFileId",
                table: "ListingMediaFiles",
                column: "StorageFileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingMediaFiles");
        }
    }
}
