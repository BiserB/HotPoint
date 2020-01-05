using Microsoft.EntityFrameworkCore.Migrations;

namespace HotPoint.Data.Migrations
{
    public partial class AddQtyToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AvailableQty",
                table: "Products",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQty",
                table: "Products");
        }
    }
}
