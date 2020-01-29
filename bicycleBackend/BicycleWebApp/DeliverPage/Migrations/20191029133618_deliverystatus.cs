using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverPage.Migrations
{
    public partial class deliverystatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryUserName",
                table: "delivery",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "delivery",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryUserName",
                table: "delivery");

            migrationBuilder.DropColumn(
                name: "status",
                table: "delivery");
        }
    }
}
