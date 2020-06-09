using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieRecommender.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsActor = table.Column<bool>(nullable: false),
                    IsDirector = table.Column<bool>(nullable: false),
                    IsGenre = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmdbID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserArtists",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ArtistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArtists", x => new { x.UserId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_UserArtists_Dictionaries_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Dictionaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArtists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDirectors",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    DirectorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDirectors", x => new { x.UserId, x.DirectorId });
                    table.ForeignKey(
                        name: "FK_UserDirectors_Dictionaries_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Dictionaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDirectors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGenres",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGenres", x => new { x.UserId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_UserGenres_Dictionaries_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Dictionaries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGenres_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRates",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    IsCreate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRates", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserRates_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserArtists_ArtistId",
                table: "UserArtists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectors_DirectorId",
                table: "UserDirectors",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGenres_GenreId",
                table: "UserGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRates_MovieId",
                table: "UserRates",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArtists");

            migrationBuilder.DropTable(
                name: "UserDirectors");

            migrationBuilder.DropTable(
                name: "UserGenres");

            migrationBuilder.DropTable(
                name: "UserRates");

            migrationBuilder.DropTable(
                name: "Dictionaries");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
