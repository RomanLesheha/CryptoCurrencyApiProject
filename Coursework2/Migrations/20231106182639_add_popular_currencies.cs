using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework2.Migrations
{
    /// <inheritdoc />
    public partial class add_popular_currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopularCurrencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyID = table.Column<int>(type: "int", nullable: false),
                    NumberOfVisits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularCurrencies", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopularCurrencies");
        }
    }
}
