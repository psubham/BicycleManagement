using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MapPointApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hubs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    postal = table.Column<int>(nullable: false),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    sub_locality_l2 = table.Column<string>(nullable: true),
                    sub_locality_l1 = table.Column<string>(nullable: true),
                    locality = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    short_sub_locality_l2 = table.Column<string>(nullable: true),
                    short_sub_locality_l1 = table.Column<string>(nullable: true),
                    short_locality = table.Column<string>(nullable: true),
                    short_country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hubs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hubs");
        }
    }
}
