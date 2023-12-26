using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations.ListingsDb
{
    /// <inheritdoc />
    public partial class ListingCategoriesandStorageFilesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "listings");

            migrationBuilder.CreateTable(
                name: "StorageFiles",
                schema: "listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListingCategories",
                schema: "listings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsSpecialCategory = table.Column<bool>(type: "boolean", nullable: false),
                    StorageFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingCategories_StorageFiles_StorageFileId",
                        column: x => x.StorageFileId,
                        principalSchema: "listings",
                        principalTable: "StorageFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingCategories_Name",
                schema: "listings",
                table: "ListingCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListingCategories_StorageFileId",
                schema: "listings",
                table: "ListingCategories",
                column: "StorageFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageFiles_Id",
                schema: "listings",
                table: "StorageFiles",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingCategories",
                schema: "listings");

            migrationBuilder.DropTable(
                name: "StorageFiles",
                schema: "listings");
        }
    }
}
