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
                values: new object[] { new Guid("ce8135be-0823-43db-a0f6-285712ec64eb"), new Guid("d5e64519-3dff-44d5-a78c-454598c14402"), null, new Guid("79cb833b-225b-4592-b771-7d6c81aef4cf"), new Guid("72fa997b-ad98-4aa4-802a-e8701437c7ad"), true, false, new Guid("15bef96f-5363-4b26-ada9-12c2d709a0ee"), 99.333m, new Guid("57287cf5-7c5e-40f5-91f7-a3c9108d860a"), new Guid("343e57de-00a8-4ea6-954b-0d68fc7a7097"), 2, "FirstSeedData" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings",
                schema: "identity");
        }
    }
}
