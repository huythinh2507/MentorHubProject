using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Store",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RunDate",
                table: "MarketPlaceOrder",
                type: "datetime2",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RunAt",
                table: "MarketPlaceOrder",
                type: "datetime2",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderedAt",
                table: "MarketPlaceOrder",
                type: "datetime2",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "MarketPlace",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceFullName",
                table: "MarketPlace",
                type: "varchar(max)",
                unicode: false,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "MarketPlace");

            migrationBuilder.DropColumn(
                name: "ServiceFullName",
                table: "MarketPlace");

            migrationBuilder.AlterColumn<string>(
                name: "RunDate",
                table: "MarketPlaceOrder",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RunAt",
                table: "MarketPlaceOrder",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderedAt",
                table: "MarketPlaceOrder",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
