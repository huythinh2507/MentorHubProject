using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "MarketPlaceProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MarketPlaceProductId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RunDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlaceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MarketPlacePro__Store__6754512E",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlaceProduct_StoreId",
                table: "MarketPlaceProduct",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketPlaceProduct");

        }
    }
}
