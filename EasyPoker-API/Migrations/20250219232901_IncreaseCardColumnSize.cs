using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPoker_API.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseCardColumnSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Winner = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Games__3214EC073CB3E97B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    PlayerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Players__3214EC07C4E2E9F4", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Players__GameId__3A81B327",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemainingCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    Card = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Remainin__3214EC079F8CAE2F", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Remaining__GameI__4F7CD00D",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerHands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Card1 = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false),
                    Card2 = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false),
                    Card3 = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false),
                    Card4 = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false),
                    Card5 = table.Column<string>(type: "nvarchar(50)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlayerHa__3214EC07A148B41B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PlayerHan__GameI__412EB0B6",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__PlayerHan__Playe__4222D4EF",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_GameId",
                table: "PlayerHands",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerHands_PlayerId",
                table: "PlayerHands",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_RemainingCards_GameId",
                table: "RemainingCards",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerHands");

            migrationBuilder.DropTable(
                name: "RemainingCards");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
