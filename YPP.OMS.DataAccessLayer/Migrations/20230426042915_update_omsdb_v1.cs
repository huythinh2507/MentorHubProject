using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_omsdb_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CountryVa__Chann__75A278F5",
                table: "CountryVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__CountryVa__Count__74AE54BC",
                table: "CountryVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__Feedback__Channe__6E01572D",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK__Feedback__Produc__6D0D32F4",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Count__6A30C649",
                table: "MarketPlace");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Payme__6B24EA82",
                table: "MarketPlace");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Store__7C4F7684",
                table: "MarketPlaceOrder");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Order__6477ECF3",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Produ__656C112C",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Produ__76969D2E",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__Chann__6EF57B66",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__UserI__6FE99F9F",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__Vouch__70DDC3D8",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__Payment__Country__778AC167",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK__Payment__Created__787EE5A0",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__Provide__68487DD7",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__Voucher__693CA210",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductCh__Chann__6754599E",
                table: "ProductChannel");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductCh__Produ__66603565",
                table: "ProductChannel");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductIm__Produ__73BA3083",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductVa__Produ__71D1E811",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductVa__Varia__72C60C4A",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Chann__7A672E12",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Produ__7B5B524B",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Wareh__797309D9",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ReturnInf__Order__628FA481",
                table: "ReturnInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__ReturnInf__Updat__6383C8BA",
                table: "ReturnInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__Store__MarketPla__7D439ABD",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK__Voucher__Channel__6C190EBB",
                table: "Voucher");

            migrationBuilder.RenameIndex(
                name: "UQ__UserInfo__C9F28456D2DAD606",
                table: "User",
                newName: "UQ__UserInfo__C9F28456B7A4B4AD");

            migrationBuilder.AddColumn<string>(
                name: "InsertedAt",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarketPlaceOrderId",
                table: "OrderInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "OrderInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "OrderInfo",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_StoreId",
                table: "OrderInfo",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK__CountryVa__Chann__60A75C0F",
                table: "CountryVariant",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__CountryVa__Count__5FB337D6",
                table: "CountryVariant",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Feedback__Channe__59063A47",
                table: "Feedback",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Feedback__Produc__5812160E",
                table: "Feedback",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Count__5535A963",
                table: "MarketPlace",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Payme__5629CD9C",
                table: "MarketPlace",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Store__6754599E",
                table: "MarketPlaceOrder",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Order__4F7CD00D",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Produ__5070F446",
                table: "OrderDetail",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Produ__619B8048",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__Chann__59FA5E80",
                table: "OrderInfo",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__Store__693CA210",
                table: "OrderInfo",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__UserI__5AEE82B9",
                table: "OrderInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__Vouch__5BE2A6F2",
                table: "OrderInfo",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Payment__Country__628FA481",
                table: "Payment",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Payment__Created__6383C8BA",
                table: "Payment",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Product__Provide__534D60F1",
                table: "Product",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Product__Voucher__5441852A",
                table: "Product",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductCh__Chann__52593CB8",
                table: "ProductChannel",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductCh__Produ__5165187F",
                table: "ProductChannel",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductIm__Produ__5EBF139D",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductVa__Produ__5CD6CB2B",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductVa__Varia__5DCAEF64",
                table: "ProductVariant",
                column: "VariantId",
                principalTable: "Variant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Chann__656C112C",
                table: "ProductWarehouse",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Produ__66603565",
                table: "ProductWarehouse",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Wareh__6477ECF3",
                table: "ProductWarehouse",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ReturnInf__Order__4D94879B",
                table: "ReturnInfo",
                column: "OrderId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ReturnInf__Updat__4E88ABD4",
                table: "ReturnInfo",
                column: "UpdatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Store__MarketPla__68487DD7",
                table: "Store",
                column: "MarketPlaceId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Voucher__Channel__571DF1D5",
                table: "Voucher",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CountryVa__Chann__60A75C0F",
                table: "CountryVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__CountryVa__Count__5FB337D6",
                table: "CountryVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__Feedback__Channe__59063A47",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK__Feedback__Produc__5812160E",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Count__5535A963",
                table: "MarketPlace");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Payme__5629CD9C",
                table: "MarketPlace");

            migrationBuilder.DropForeignKey(
                name: "FK__MarketPla__Store__6754599E",
                table: "MarketPlaceOrder");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Order__4F7CD00D",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Produ__5070F446",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderDeta__Produ__619B8048",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__Chann__59FA5E80",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__Store__693CA210",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__UserI__5AEE82B9",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__OrderInfo__Vouch__5BE2A6F2",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__Payment__Country__628FA481",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK__Payment__Created__6383C8BA",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__Provide__534D60F1",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__Product__Voucher__5441852A",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductCh__Chann__52593CB8",
                table: "ProductChannel");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductCh__Produ__5165187F",
                table: "ProductChannel");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductIm__Produ__5EBF139D",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductVa__Produ__5CD6CB2B",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductVa__Varia__5DCAEF64",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Chann__656C112C",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Produ__66603565",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ProductWa__Wareh__6477ECF3",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK__ReturnInf__Order__4D94879B",
                table: "ReturnInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__ReturnInf__Updat__4E88ABD4",
                table: "ReturnInfo");

            migrationBuilder.DropForeignKey(
                name: "FK__Store__MarketPla__68487DD7",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK__Voucher__Channel__571DF1D5",
                table: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_OrderInfo_StoreId",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "InsertedAt",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "MarketPlaceOrderId",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderInfo");

            migrationBuilder.RenameIndex(
                name: "UQ__UserInfo__C9F28456B7A4B4AD",
                table: "User",
                newName: "UQ__UserInfo__C9F28456D2DAD606");

            migrationBuilder.AddForeignKey(
                name: "FK__CountryVa__Chann__75A278F5",
                table: "CountryVariant",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__CountryVa__Count__74AE54BC",
                table: "CountryVariant",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Feedback__Channe__6E01572D",
                table: "Feedback",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Feedback__Produc__6D0D32F4",
                table: "Feedback",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Count__6A30C649",
                table: "MarketPlace",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Payme__6B24EA82",
                table: "MarketPlace",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__MarketPla__Store__7C4F7684",
                table: "MarketPlaceOrder",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Order__6477ECF3",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Produ__656C112C",
                table: "OrderDetail",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderDeta__Produ__76969D2E",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__Chann__6EF57B66",
                table: "OrderInfo",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__UserI__6FE99F9F",
                table: "OrderInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__OrderInfo__Vouch__70DDC3D8",
                table: "OrderInfo",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Payment__Country__778AC167",
                table: "Payment",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Payment__Created__787EE5A0",
                table: "Payment",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Product__Provide__68487DD7",
                table: "Product",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Product__Voucher__693CA210",
                table: "Product",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductCh__Chann__6754599E",
                table: "ProductChannel",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductCh__Produ__66603565",
                table: "ProductChannel",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductIm__Produ__73BA3083",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductVa__Produ__71D1E811",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductVa__Varia__72C60C4A",
                table: "ProductVariant",
                column: "VariantId",
                principalTable: "Variant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Chann__7A672E12",
                table: "ProductWarehouse",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Produ__7B5B524B",
                table: "ProductWarehouse",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ProductWa__Wareh__797309D9",
                table: "ProductWarehouse",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ReturnInf__Order__628FA481",
                table: "ReturnInfo",
                column: "OrderId",
                principalTable: "OrderInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ReturnInf__Updat__6383C8BA",
                table: "ReturnInfo",
                column: "UpdatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Store__MarketPla__7D439ABD",
                table: "Store",
                column: "MarketPlaceId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__Voucher__Channel__6C190EBB",
                table: "Voucher",
                column: "ChannelId",
                principalTable: "MarketPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
