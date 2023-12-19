using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class AddListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumberId",
                schema: "identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "identity",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Listings",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DescriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PropertyTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    RulesId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvailabilityId = table.Column<Guid>(type: "uuid", nullable: true),
                    HostId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    InstantBook = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Listings",
                columns: new[] { "Id", "AvailabilityId", "DeletedTime", "DescriptionId", "HostId", "InstantBook", "IsDeleted", "LocationId", "Price", "PropertyTypeId", "RulesId", "Status", "Title" },
                values: new object[] { new Guid("ac1645ba-d1a8-4449-a4c3-eea260ca1fde"), new Guid("f0ca3288-d1c8-40e0-9fc6-43c57a11acac"), null, new Guid("509c4df8-549d-4ad6-a3fb-58c96f40fc9f"), new Guid("4a9c647e-dc43-47fe-a7ee-7d16e4077db6"), true, false, new Guid("4391e832-5f7a-42c9-8ed0-fa8c092577b6"), 99.333m, new Guid("a152ab83-27d1-4809-9f42-d1b267b631ee"), new Guid("1857ea6b-2431-4595-afa8-23e149567622"), 2, "FirstSeedData" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings",
                schema: "identity");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "identity",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "PhoneNumberId",
                schema: "identity",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
