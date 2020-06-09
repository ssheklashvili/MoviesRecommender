using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRecommender.Infrastructure.Migrations
{
    public partial class changeFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActor",
                table: "Dictionaries");

            migrationBuilder.AddColumn<bool>(
                name: "IsArtist",
                table: "Dictionaries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArtist",
                table: "Dictionaries");

            migrationBuilder.AddColumn<bool>(
                name: "IsActor",
                table: "Dictionaries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
