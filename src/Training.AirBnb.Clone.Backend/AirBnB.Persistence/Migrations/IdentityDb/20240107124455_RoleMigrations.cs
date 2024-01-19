using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class RoleMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "identity",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "identity",
                newName: "Roles",
                newSchema: "identity");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Type",
                schema: "identity",
                table: "Roles",
                newName: "IX_Roles_Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 44, 52, 577, DateTimeKind.Utc).AddTicks(6274));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 44, 52, 577, DateTimeKind.Utc).AddTicks(6282));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 44, 52, 577, DateTimeKind.Utc).AddTicks(6279));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "identity",
                table: "Users",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "identity",
                newName: "Role",
                newSchema: "identity");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Type",
                schema: "identity",
                table: "Role",
                newName: "IX_Role_Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "identity",
                table: "Role",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 17, 38, 445, DateTimeKind.Utc).AddTicks(5311));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 17, 38, 445, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 17, 38, 445, DateTimeKind.Utc).AddTicks(5314));

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
    }
}
