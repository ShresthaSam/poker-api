using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerApi.Repositories.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    card_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    suit = table.Column<string>(type: "TEXT", nullable: true),
                    number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.card_id);
                });

            migrationBuilder.CreateTable(
                name: "card_hand",
                columns: table => new
                {
                    hand_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    card_id_1 = table.Column<int>(type: "INTEGER", nullable: false),
                    card_id_2 = table.Column<int>(type: "INTEGER", nullable: false),
                    card_id_3 = table.Column<int>(type: "INTEGER", nullable: false),
                    card_id_4 = table.Column<int>(type: "INTEGER", nullable: false),
                    card_id_5 = table.Column<int>(type: "INTEGER", nullable: false),
                    player_user_id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_hand", x => x.hand_id);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    player_user_id = table.Column<string>(type: "TEXT", nullable: false),
                    player_user_name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.player_user_id);
                });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 1, "2", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 29, "9", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 30, "9", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 31, "9", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 32, "9", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 33, "10", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 34, "10", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 35, "10", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 36, "10", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 37, "J", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 38, "J", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 28, "8", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 39, "J", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 41, "Q", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 42, "Q", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 43, "Q", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 44, "Q", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 45, "K", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 46, "K", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 47, "K", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 48, "K", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 49, "A", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 50, "A", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 40, "J", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 27, "8", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 26, "8", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 25, "8", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 2, "2", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 3, "2", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 4, "2", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 5, "3", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 6, "3", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 7, "3", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 8, "3", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 9, "4", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 10, "4", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 11, "4", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 12, "4", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 13, "5", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 14, "5", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 15, "5", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 16, "5", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 17, "6", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 18, "6", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 19, "6", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 20, "6", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 21, "7", "H" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 22, "7", "D" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 23, "7", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 24, "7", "S" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 51, "A", "C" });

            migrationBuilder.InsertData(
                table: "card",
                columns: new[] { "card_id", "number", "suit" },
                values: new object[] { 52, "A", "S" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "card_hand");

            migrationBuilder.DropTable(
                name: "player");
        }
    }
}
