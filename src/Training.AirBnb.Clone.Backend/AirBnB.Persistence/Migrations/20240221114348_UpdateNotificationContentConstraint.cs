using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationContentConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "NotificationTemplates",
                type: "character varying(1036288)",
                maxLength: 1036288,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(129536)",
                oldMaxLength: 129536);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "NotificationTemplates",
                type: "character varying(129536)",
                maxLength: 129536,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1036288)",
                oldMaxLength: 1036288);
        }
    }
}
