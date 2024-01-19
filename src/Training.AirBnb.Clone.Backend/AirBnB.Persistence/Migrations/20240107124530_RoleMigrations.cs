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
            migrationBuilder.CreateTable(
                name: "Listing",
                schema: "notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BuiltDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Address_City = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Address_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Address_Longitude = table.Column<double>(type: "double precision", nullable: false),
                    PricePerNight_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PricePerNight_Currency = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                schema: "notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferredTheme = table.Column<int>(type: "integer", nullable: false),
                    PreferredNotificationType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "notification",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificationCode",
                schema: "notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeType = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExpiryTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    VerificationLink = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationCode_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "notification",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("29e62346-1bb7-4fd4-833f-8ebd85734570"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 45, 29, 823, DateTimeKind.Utc).AddTicks(3415));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c93760c5-03ed-4845-b3c9-01c125ef326a"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 45, 29, 823, DateTimeKind.Utc).AddTicks(3422));

            migrationBuilder.UpdateData(
                schema: "notification",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("eec07fc2-2a0d-4e63-b084-1975e836793c"),
                column: "CreatedTime",
                value: new DateTime(2024, 1, 7, 12, 45, 29, 823, DateTimeKind.Utc).AddTicks(3420));

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                schema: "notification",
                table: "UserSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCode_UserId",
                schema: "notification",
                table: "VerificationCode",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listing",
                schema: "notification");

            migrationBuilder.DropTable(
                name: "UserSettings",
                schema: "notification");

            migrationBuilder.DropTable(
                name: "VerificationCode",
                schema: "notification");

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
    }
}
