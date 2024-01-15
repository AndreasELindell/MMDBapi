using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewApiProject.Api.Migrations
{
    /// <inheritdoc />
    public partial class Updatedwishlistitem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster_path",
                table: "Wishlist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Release_date",
                table: "Wishlist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "vote_average",
                table: "Wishlist",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "vote_count",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster_path",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "Release_date",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "vote_average",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "vote_count",
                table: "Wishlist");
        }
    }
}
