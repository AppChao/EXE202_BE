using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXE202_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class gamification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Streak",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Streak",
                table: "UserProfiles");
        }
    }
}
