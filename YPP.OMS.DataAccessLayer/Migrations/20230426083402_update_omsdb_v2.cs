using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "MarketPlace");

            migrationBuilder.AlterColumn<string>(
                name: "MarketPlaceOrderId",
                table: "OrderInfo",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarketPlaceOrderNumber",
                table: "MarketPlaceOrder",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MarketPlaceOrderId",
                table: "OrderInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MarketPlaceOrderNumber",
                table: "MarketPlaceOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "MarketPlace",
                type: "int",
                nullable: true);
        }
    }
}
