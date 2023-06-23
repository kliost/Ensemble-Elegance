using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ordersupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderItemList",
                table: "Orders",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "OrderListJson",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderListJson",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "OrderItemList");
        }
    }
}
