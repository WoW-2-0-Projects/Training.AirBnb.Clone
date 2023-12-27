using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations.NotificationDb
{
    /// <inheritdoc />
    public partial class RoleMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 18, 46, 309, DateTimeKind.Utc).AddTicks(4396));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 18, 46, 309, DateTimeKind.Utc).AddTicks(4400));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 26, 16, 18, 46, 309, DateTimeKind.Utc).AddTicks(4399));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9564));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2023, 12, 25, 17, 53, 37, 305, DateTimeKind.Utc).AddTicks(9570));
        }
    }
}
