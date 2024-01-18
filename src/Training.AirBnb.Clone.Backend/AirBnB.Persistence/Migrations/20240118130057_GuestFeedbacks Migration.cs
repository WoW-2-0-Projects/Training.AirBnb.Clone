using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GuestFeedbacksMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestFeedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ListingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cleanliness = table.Column<byte>(type: "smallint", nullable: false),
                    Accuracy = table.Column<byte>(type: "smallint", nullable: false),
                    CheckIn = table.Column<byte>(type: "smallint", nullable: false),
                    Communication = table.Column<byte>(type: "smallint", nullable: false),
                    Location = table.Column<byte>(type: "smallint", nullable: false),
                    Value = table.Column<byte>(type: "smallint", nullable: false),
                    OverallRating = table.Column<double>(type: "double precision", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestFeedbacks_Listings_ListingId",
                        column: x => x.ListingId,
                        principalTable: "Listings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestFeedbacks_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestFeedbacks_GuestId",
                table: "GuestFeedbacks",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestFeedbacks_ListingId",
                table: "GuestFeedbacks",
                column: "ListingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestFeedbacks");
        }
    }
}
