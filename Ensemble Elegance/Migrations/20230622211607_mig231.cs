using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class mig231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ShopItems");

            migrationBuilder.AddColumn<string>(
                name: "CategoriesJson",
                table: "ShopItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriesJson",
                table: "ShopItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ShopItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
