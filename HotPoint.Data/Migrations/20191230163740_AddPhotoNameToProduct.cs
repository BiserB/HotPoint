using Microsoft.EntityFrameworkCore.Migrations;

namespace HotPoint.Data.Migrations
{
    public partial class AddPhotoNameToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Products",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Products");
        }
    }
}
