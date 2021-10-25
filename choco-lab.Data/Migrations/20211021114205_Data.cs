using Microsoft.EntityFrameworkCore.Migrations;

namespace choco_lab.Data.Migrations
{
    public partial class Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Chocolates",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<string>(
                name: "DetailedDescription",
                table: "Chocolates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailedDescription",
                table: "Chocolates");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Chocolates",
                newName: "Description");
        }
    }
}
