using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coursework2.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyIdToCryproCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "CryproCurrency",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "CryproCurrency");
        }
    }
}
