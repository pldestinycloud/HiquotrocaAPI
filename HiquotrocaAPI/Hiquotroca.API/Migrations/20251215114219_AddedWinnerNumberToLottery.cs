using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiquotroca.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedWinnerNumberToLottery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinnerNumber",
                table: "Lotteries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinnerNumber",
                table: "Lotteries");
        }
    }
}
