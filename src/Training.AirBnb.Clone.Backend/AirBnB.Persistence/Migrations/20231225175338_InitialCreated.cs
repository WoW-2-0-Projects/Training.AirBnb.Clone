using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBnB.Persistence.Migrations.NotificationDb
{
    /// <inheritdoc />
    public partial class InitialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                schema: "notification",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsDisable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageFile",
                schema: "notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFile", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "notification",
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "IsDisable", "ModifiedTime", "Type" },
                values: new object[,]
                {
                    { new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"), new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9564), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"), new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9573), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"), new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9570), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "notification",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Type",
                schema: "notification",
                table: "Role",
                column: "Type",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "notification",
                table: "User",
                column: "RoleId",
                principalSchema: "notification",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                schema: "notification",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "notification");

            migrationBuilder.DropTable(
                name: "StorageFile",
                schema: "notification");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                schema: "notification",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "notification",
                table: "User");
        }
    }
}
