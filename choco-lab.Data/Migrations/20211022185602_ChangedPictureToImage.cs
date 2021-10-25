using Microsoft.EntityFrameworkCore.Migrations;

namespace choco_lab.Data.Migrations
{
    public partial class ChangedPictureToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Chocolates",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Chocolates",
                newName: "Picture");
        }
    }
}
