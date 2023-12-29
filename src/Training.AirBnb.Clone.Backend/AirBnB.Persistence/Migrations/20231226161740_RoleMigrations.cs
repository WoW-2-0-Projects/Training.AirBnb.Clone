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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9805));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 52, 55, 773, DateTimeKind.Utc).AddTicks(9811));
        }
    }
}
