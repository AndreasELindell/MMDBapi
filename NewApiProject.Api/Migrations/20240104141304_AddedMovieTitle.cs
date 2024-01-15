using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewApiProject.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedMovieTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "movieTitle",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "movieTitle",
                table: "Reviews");
        }
    }
}
