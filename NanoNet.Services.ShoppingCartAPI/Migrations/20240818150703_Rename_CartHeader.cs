using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoNet.Services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class Rename_CartHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartHeaders_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartHeaders_CartHeaderId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartHeaderId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartHeaders_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
