using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "MarketPlaceSeller",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPerform",
                table: "MarketPlaceSeller",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SellerInfoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketPlaceSellerId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    MarketPlaceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RunAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerInfoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SellerInfo__Store__6744512C",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellerInfoes_StoreId",
                table: "SellerInfoes",
                column: "StoreId");

            var scriptsPath = AppContext.BaseDirectory;
            var createUpsertSellerData = File.ReadAllText(Path.Combine(scriptsPath, "Scripts", "UpsertSellerData.sql"));
            migrationBuilder.Sql(createUpsertSellerData);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerInfoes");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "MarketPlaceSeller");

            migrationBuilder.DropColumn(
                name: "IsPerform",
                table: "MarketPlaceSeller");
        }
    }
}
