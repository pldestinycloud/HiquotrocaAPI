using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiquotroca.API.Migrations
{
    /// <inheritdoc />
    public partial class UserPromoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowing_Users_FollowedId",
                table: "UserFollowing");

            migrationBuilder.DropColumn(
                name: "PromotionalCodes",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnersId",
                table: "PromotionalCodes");

            migrationBuilder.CreateTable(
                name: "UserPromotionalCodes",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PromoCodeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPromotionalCodes", x => new { x.UserId, x.PromoCodeId });
                    table.ForeignKey(
                        name: "FK_UserPromotionalCodes_PromotionalCodes_PromoCodeId",
                        column: x => x.PromoCodeId,
                        principalTable: "PromotionalCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPromotionalCodes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPromotionalCodes_PromoCodeId",
                table: "UserPromotionalCodes",
                column: "PromoCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowing_Users_FollowedId",
                table: "UserFollowing",
                column: "FollowedId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowing_Users_FollowedId",
                table: "UserFollowing");

            migrationBuilder.DropTable(
                name: "UserPromotionalCodes");

            migrationBuilder.AddColumn<string>(
                name: "PromotionalCodes",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnersId",
                table: "PromotionalCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowing_Users_FollowedId",
                table: "UserFollowing",
                column: "FollowedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
