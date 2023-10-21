using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeProductivityApi.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccessed",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "Validity",
                table: "Operations");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Operations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Operations");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccessed",
                table: "Operations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Validity",
                table: "Operations",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
