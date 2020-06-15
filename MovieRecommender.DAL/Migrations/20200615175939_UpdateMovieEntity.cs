using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRecommender.Infrastructure.Migrations
{
    public partial class UpdateMovieEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImdbID",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImdbID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Movies");
        }
    }
}
