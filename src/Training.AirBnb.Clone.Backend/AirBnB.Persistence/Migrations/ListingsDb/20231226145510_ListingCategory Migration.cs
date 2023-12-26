﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations.ListingsDb
{
    /// <inheritdoc />
    public partial class ListingCategoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "listings");

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
                        name: "FK_ListingCategories_StorageFile_StorageFileId",
                        column: x => x.StorageFileId,
                        principalSchema: "identity",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingCategories",
                schema: "listings");
        }
    }
}
