using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vinylify.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectionItems",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DiscogsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItems", x => new { x.UserId, x.DiscogsId });
                });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DiscogsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => new { x.UserId, x.DiscogsId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItems");

            migrationBuilder.DropTable(
                name: "WishListItems");
        }
    }
}
