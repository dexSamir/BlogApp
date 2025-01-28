using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_PublisherId",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Blogs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_PublisherId",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Blogs",
                newName: "PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                newName: "IX_Blogs_PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_PublisherId",
                table: "Blogs",
                column: "PublisherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
