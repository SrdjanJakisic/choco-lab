using Microsoft.EntityFrameworkCore.Migrations;

namespace choco_lab.Data.Migrations
{
    public partial class addedQuantityToChocolate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Chocolates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Chocolates");
        }
    }
}
