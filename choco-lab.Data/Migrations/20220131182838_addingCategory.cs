using Microsoft.EntityFrameworkCore.Migrations;

namespace choco_lab.Data.Migrations
{
    public partial class addingCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Chocolates",
                newName: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chocolates_CategoryId",
                table: "Chocolates",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chocolates_Categories_CategoryId",
                table: "Chocolates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chocolates_Categories_CategoryId",
                table: "Chocolates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Chocolates_CategoryId",
                table: "Chocolates");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Chocolates",
                newName: "Category");
        }
    }
}
