using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NanoNet.Services.ShoppingCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Cart_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.RenameTable(
                name: "CartHeaders",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "CartDetails",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartHeaderId",
                table: "CartItems",
                newName: "IX_CartItems_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartHeaderId",
                table: "CartItems",
                column: "CartHeaderId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartHeaderId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "CartHeaders");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartHeaderId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeaders",
                table: "CartHeaders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                table: "CartDetails",
                column: "CartHeaderId",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
