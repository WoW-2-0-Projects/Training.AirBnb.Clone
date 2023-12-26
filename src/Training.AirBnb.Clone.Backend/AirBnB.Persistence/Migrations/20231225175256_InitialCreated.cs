using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBnB.Persistence.Migrations.IdentityDb;
    /// <inheritdoc />
    public partial class InitialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                schema: "identity",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(129536)", maxLength: 129536, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    TemplateType = table.Column<int>(type: "integer", nullable: false),
                    Subject = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "identity",
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

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "IsDisable", "ModifiedTime", "Type" },
                values: new object[,]
                {
                    { new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"), new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9805), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"), new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9813), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"), new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9811), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "identity",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Type_TemplateType",
                schema: "identity",
                table: "NotificationTemplates",
                columns: new[] { "Type", "TemplateType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Type",
                schema: "identity",
                table: "Role",
                column: "Type",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                schema: "identity",
                table: "Users",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropTable(
                name: "NotificationTemplates",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "identity");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "identity",
                table: "Users");
        }
    }
