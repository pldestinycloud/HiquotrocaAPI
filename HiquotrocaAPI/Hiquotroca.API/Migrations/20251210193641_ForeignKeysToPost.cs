using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hiquotroca.API.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeysToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PostTaxonomyData_SubCategoryId",
                table: "Posts",
                newName: "PostTaxonomyData_SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Location_CountryId",
                table: "Posts",
                column: "Location_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTaxonomyData_ActionTypeId",
                table: "Posts",
                column: "PostTaxonomyData_ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTaxonomyData_CategoryId",
                table: "Posts",
                column: "PostTaxonomyData_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostTaxonomyData_SubcategoryId",
                table: "Posts",
                column: "PostTaxonomyData_SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ActionTypes_PostTaxonomyData_ActionTypeId",
                table: "Posts",
                column: "PostTaxonomyData_ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_PostTaxonomyData_CategoryId",
                table: "Posts",
                column: "PostTaxonomyData_CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Countries_Location_CountryId",
                table: "Posts",
                column: "Location_CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubCategories_PostTaxonomyData_SubcategoryId",
                table: "Posts",
                column: "PostTaxonomyData_SubcategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ActionTypes_PostTaxonomyData_ActionTypeId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_PostTaxonomyData_CategoryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Countries_Location_CountryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubCategories_PostTaxonomyData_SubcategoryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Location_CountryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTaxonomyData_ActionTypeId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTaxonomyData_CategoryId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostTaxonomyData_SubcategoryId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PostTaxonomyData_SubcategoryId",
                table: "Posts",
                newName: "PostTaxonomyData_SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
