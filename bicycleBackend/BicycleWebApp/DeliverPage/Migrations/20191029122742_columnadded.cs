using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverPage.Migrations
{
    public partial class columnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    BookingId = table.Column<int>(nullable: false),
                    BicycleNumber = table.Column<string>(nullable: true),
                    BicycleId = table.Column<int>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    Deliverylat = table.Column<double>(nullable: false),
                    Deliverylng = table.Column<double>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DeliveryUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery", x => x.DeliveryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delivery");
        }
    }
}
