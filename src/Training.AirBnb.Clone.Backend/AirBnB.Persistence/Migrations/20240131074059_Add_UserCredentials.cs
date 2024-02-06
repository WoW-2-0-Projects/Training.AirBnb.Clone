using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirBnB.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");
            
            migrationBuilder.AddColumn<string>(
                name: "UserCredentials_PasswordHash",
                table: "Users",
                type: "character varying(128)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(128)",
                nullable: false);
            
            migrationBuilder.DropColumn(
                name: "Credentials_PasswordHash",
                table: "Users");
        }
    }
}
