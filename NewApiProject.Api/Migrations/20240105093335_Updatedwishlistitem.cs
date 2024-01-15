using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewApiProject.Api.Migrations
{
    /// <inheritdoc />
    public partial class Updatedwishlistitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Movies_MovieId",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_MovieId",
                table: "Wishlist");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Wishlist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Wishlist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Wishlist");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_MovieId",
                table: "Wishlist",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Movies_MovieId",
                table: "Wishlist",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
