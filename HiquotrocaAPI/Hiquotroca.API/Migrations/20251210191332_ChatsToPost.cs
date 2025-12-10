using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiquotroca.API.Migrations
{
    /// <inheritdoc />
    public partial class ChatsToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chats",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_PostId",
                table: "Chats",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Posts_PostId",
                table: "Chats",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Posts_PostId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_PostId",
                table: "Chats");

            migrationBuilder.AddColumn<string>(
                name: "Chats",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
