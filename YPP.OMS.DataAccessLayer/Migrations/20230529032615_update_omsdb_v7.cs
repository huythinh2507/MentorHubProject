using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketPlaceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarketPlaceCategoryId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    AttributeMetadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlaceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MarketPlaceCate__Store__6754523E",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MarketPlaceSeller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarketplaceSellerId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlaceSeller", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MarketPlaceSell__Store__6754522E",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlaceCategory_StoreId",
                table: "MarketPlaceCategory",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlaceSeller_StoreId",
                table: "MarketPlaceSeller",
                column: "StoreId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketPlaceCategory");

            migrationBuilder.DropTable(
                name: "MarketPlaceSeller");
        }
    }
}
