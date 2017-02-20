using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaApi.Migrations
{
    public partial class ChangedPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Person",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Person",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Person");
        }
    }
}
