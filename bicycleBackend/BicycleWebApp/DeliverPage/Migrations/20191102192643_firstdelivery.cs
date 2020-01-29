using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverPage.Migrations
{
    public partial class firstdelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "delivery",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "delivery",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "Cancelationtime",
                table: "delivery",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationTime",
                table: "delivery",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliverTime",
                table: "delivery",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTime",
                table: "delivery",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelationtime",
                table: "delivery");

            migrationBuilder.DropColumn(
                name: "ConfirmationTime",
                table: "delivery");

            migrationBuilder.DropColumn(
                name: "DeliverTime",
                table: "delivery");

            migrationBuilder.DropColumn(
                name: "OrderTime",
                table: "delivery");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "delivery",
                newName: "status");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "delivery",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
