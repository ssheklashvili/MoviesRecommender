using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRecommender.Infrastructure.Migrations
{
    public partial class AddMoviePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PosterPath",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterPath",
                table: "Movies");
        }
    }
}
