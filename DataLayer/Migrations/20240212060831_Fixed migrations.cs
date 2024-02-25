using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class Fixedmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriesValues_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new { x.Date, x.CategoryId, x.GameId });
                    table.ForeignKey(
                        name: "FK_Answers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_CategoriesValues_CategoryValueId",
                        column: x => x.CategoryValueId,
                        principalTable: "CategoriesValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroProfiles",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    HeroId = table.Column<int>(type: "int", nullable: false),
                    ValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroProfiles", x => new { x.ValueId, x.GameId, x.HeroId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_HeroProfiles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroProfiles_CategoriesValues_ValueId",
                        column: x => x.ValueId,
                        principalTable: "CategoriesValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HeroProfiles_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroProfiles_HeroMetadata_HeroId",
                        column: x => x.HeroId,
                        principalTable: "HeroMetadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CategoryId",
                table: "Answers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_CategoryValueId",
                table: "Answers",
                column: "CategoryValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_GameId",
                table: "Answers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesValues_CategoryId",
                table: "CategoriesValues",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroProfiles_CategoryId",
                table: "HeroProfiles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroProfiles_GameId",
                table: "HeroProfiles",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroProfiles_HeroId",
                table: "HeroProfiles",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "HeroProfiles");

            migrationBuilder.DropTable(
                name: "CategoriesValues");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "HeroMetadata");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
