using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRecommender.Infrastructure.Migrations
{
    public partial class add_userrecomendationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRecomendations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    RateType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecomendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRecomendationToMovies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(nullable: false),
                    UserRecomendationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecomendationToMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRecomendationToMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRecomendationToMovies_UserRecomendations_UserRecomendationId",
                        column: x => x.UserRecomendationId,
                        principalTable: "UserRecomendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRecomendationToMovies_MovieId",
                table: "UserRecomendationToMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecomendationToMovies_UserRecomendationId",
                table: "UserRecomendationToMovies",
                column: "UserRecomendationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRecomendationToMovies");

            migrationBuilder.DropTable(
                name: "UserRecomendations");
        }
    }
}
