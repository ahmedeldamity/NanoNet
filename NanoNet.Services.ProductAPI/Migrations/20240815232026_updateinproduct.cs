using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoNet.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateinproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageUrl");
        }
    }
}
