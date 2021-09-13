using Microsoft.EntityFrameworkCore.Migrations;

namespace core.data.Migrations
{
    public partial class ProductRateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Products",
                type: "double",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Products");
        }
    }
}
