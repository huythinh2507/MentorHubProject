using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "ProductChannel",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Buffer",
                table: "ProductChannel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketPlaceProductId",
                table: "ProductChannel",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketPlaceType",
                table: "ProductChannel",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "ProductChannel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RunAt",
                table: "ProductChannel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "ProductChannel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Unusable",
                table: "ProductChannel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "OrderInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountAmount",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPromotion",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FobPrice",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FulfillmentType",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HandlingFee",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketPlaceType",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "OrderInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShippingDiscountAmount",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxInfo",
                table: "OrderInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalSellerIncome",
                table: "OrderInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPerform",
                table: "MarketPlaceProduct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MarketPlaceProduct",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPerform",
                table: "MarketPlaceOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MarketPlaceOrder",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PerformTaskHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformTaskHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PerformTaskHistory__TaskInfo__634712eb",
                        column: x => x.TaskId,
                        principalTable: "TaskInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UpsertTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpsertName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RunAt = table.Column<int>(type: "int", nullable: true),
                    UpsertMetadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpsertTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductChannel_StoreId",
                table: "ProductChannel",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformTaskHistory_TaskId",
                table: "PerformTaskHistory",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK__ProductCh__Store__6754513E",
                table: "ProductChannel",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            var scriptsPath = AppContext.BaseDirectory;
            var createUpsertOrderData = File.ReadAllText(Path.Combine(scriptsPath, "Scripts", "UpsertOrderData.sql"));
            var createUpsertProductData = File.ReadAllText(Path.Combine(scriptsPath, "Scripts", "UpsertProductChannelData.sql"));
            migrationBuilder.Sql(createUpsertOrderData);
            migrationBuilder.Sql(createUpsertProductData);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ProductCh__Store__6754513E",
                table: "ProductChannel");

            migrationBuilder.DropTable(
                name: "PerformTaskHistory");

            migrationBuilder.DropTable(
                name: "UpsertTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProductChannel_StoreId",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "Buffer",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "MarketPlaceProductId",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "MarketPlaceType",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "RunAt",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "Unusable",
                table: "ProductChannel");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "DiscountPromotion",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "FobPrice",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "FulfillmentType",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "HandlingFee",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "MarketPlaceType",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "ShippingDiscountAmount",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "TaxInfo",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "TotalSellerIncome",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "IsPerform",
                table: "MarketPlaceProduct");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MarketPlaceProduct");

            migrationBuilder.DropColumn(
                name: "IsPerform",
                table: "MarketPlaceOrder");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MarketPlaceOrder");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "ProductChannel",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
