using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRPr.Migrations
{
    public partial class AddedPhotoFieldToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhotoName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhotoName",
                table: "Products");
        }
    }
}
