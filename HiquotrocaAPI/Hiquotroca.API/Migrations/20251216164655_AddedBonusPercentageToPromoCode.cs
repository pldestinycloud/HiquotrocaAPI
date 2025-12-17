using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiquotroca.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedBonusPercentageToPromoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BonusPercentage",
                table: "PromotionalCodes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusPercentage",
                table: "PromotionalCodes");
        }
    }
}
