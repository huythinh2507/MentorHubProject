using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiMethod");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "Store",
                newName: "TokenExpiredAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RunAt",
                table: "Store",
                type: "datetime2",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshExpiredAt",
                table: "Store",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderInfo",
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
                name: "InsertedAt",
                table: "OrderInfo",
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
                name: "AuthURI",
                table: "MarketPlace",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApiMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketPlaceType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Path = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ApiName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMetadata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiMetadata");

            migrationBuilder.DropColumn(
                name: "RefreshExpiredAt",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "AuthURI",
                table: "MarketPlace");

            migrationBuilder.RenameColumn(
                name: "TokenExpiredAt",
                table: "Store",
                newName: "ExpiresAt");

            migrationBuilder.AlterColumn<string>(
                name: "RunAt",
                table: "Store",
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
                name: "UpdatedAt",
                table: "OrderInfo",
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
                name: "InsertedAt",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApiMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketPlaceType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Path = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethod", x => x.Id);
                });
        }
    }
}
