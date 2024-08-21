using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YPP.MH.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class omsdbv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketPlaceType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Path = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShortCode = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    RunAt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MarketPlaceType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Path = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UserRole = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: true),
                    FullAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserStatus = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    Facebook = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Instagram = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WarehouseName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CardHolder = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Payment__Country__778AC167",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Payment__Created__787EE5A0",
                        column: x => x.CreatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MarketPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarketPlaceName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MarketPlaceImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    URI = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AppId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AppSecret = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    RedirectURL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ExpireDuration = table.Column<int>(type: "int", nullable: true),
                    Inactive = table.Column<byte>(type: "tinyint", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    ConfigName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ConfigURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    LiveStock = table.Column<int>(type: "int", nullable: true),
                    DelistedStock = table.Column<int>(type: "int", nullable: true),
                    OutOfStock = table.Column<int>(type: "int", nullable: true),
                    LowStock = table.Column<int>(type: "int", nullable: true),
                    OnDemand = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDelete = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MarketPla__Count__6A30C649",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__MarketPla__Payme__6B24EA82",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CountryVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CountryVa__Chann__75A278F5",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__CountryVa__Count__74AE54BC",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MarketPlaceId = table.Column<int>(type: "int", nullable: true),
                    Token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ShopId = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Inactive = table.Column<byte>(type: "tinyint", nullable: true),
                    RunAt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Store__MarketPla__7D439ABD",
                        column: x => x.MarketPlaceId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    VoucherName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    VoucherCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    VoucherType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apply = table.Column<string>(type: "varchar(17)", unicode: false, maxLength: 17, nullable: true),
                    VoucherDiscountType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LimitPerCustomer = table.Column<int>(type: "int", nullable: true),
                    PeriodStartTime = table.Column<DateTime>(type: "date", nullable: true),
                    PeriodEndTime = table.Column<DateTime>(type: "date", nullable: true),
                    OrderUsedBudget = table.Column<double>(type: "float", nullable: true),
                    CollectStartAt = table.Column<DateTime>(type: "date", nullable: true),
                    OfferingMoneyValueOff = table.Column<double>(type: "float", nullable: true),
                    MaxDiscountOfferingValueOff = table.Column<int>(type: "int", nullable: true),
                    CriteriaOverMoney = table.Column<int>(type: "int", nullable: false),
                    VoucherStatus = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Voucher__Channel__6C190EBB",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MarketPlaceOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    RunAt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    RunDate = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    OrderedAt = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    MarketPlaceOrderNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlaceOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MarketPla__Store__7C4F7684",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaData = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    OrderedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    OrderNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TaxCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CancelBy = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CancelReason = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BuyerId = table.Column<int>(type: "int", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShippingAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RecipientPhoneNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    District = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ShippingServiceCost = table.Column<double>(type: "float", nullable: true),
                    ShippingFee = table.Column<double>(type: "float", nullable: true),
                    ShipmentProvider = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CustomerPaymentMethod = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    OrderStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderInfo__Chann__6EF57B66",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__OrderInfo__UserI__6FE99F9F",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__OrderInfo__Vouch__70DDC3D8",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    BasePrice = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    ProductHeight = table.Column<int>(type: "int", nullable: true),
                    ProductWidth = table.Column<int>(type: "int", nullable: true),
                    ProductLength = table.Column<int>(type: "int", nullable: true),
                    ProductWeight = table.Column<int>(type: "int", nullable: true),
                    ProductDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Product__Provide__68487DD7",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Product__Voucher__693CA210",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReturnInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReturnStatus = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ReturnInf__Order__628FA481",
                        column: x => x.OrderId,
                        principalTable: "OrderInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__ReturnInf__Updat__6383C8BA",
                        column: x => x.UpdatedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductIm__Produ__73BA3083",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    ProductVariantValue = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SKU = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ProductVariantImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductVa__Produ__71D1E811",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__ProductVa__Varia__72C60C4A",
                        column: x => x.VariantId,
                        principalTable: "Variant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    FeedbackDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Rating = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Feedback__Channe__6E01572D",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__Feedback__Produc__6D0D32F4",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PricePerItem = table.Column<double>(type: "float", nullable: true),
                    DiscountedPrice = table.Column<double>(type: "float", nullable: true),
                    PromotionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__6477ECF3",
                        column: x => x.OrderId,
                        principalTable: "OrderInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Produ__656C112C",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Produ__76969D2E",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    MinimumThreshold = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Issue = table.Column<int>(type: "int", nullable: true),
                    Lost = table.Column<int>(type: "int", nullable: true),
                    InProcess = table.Column<int>(type: "int", nullable: true),
                    Sold = table.Column<int>(type: "int", nullable: true),
                    CSKU = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    OnDemandRequired = table.Column<bool>(type: "bit", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductChannelStatus = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    LastSevenDaySalesAvg = table.Column<int>(type: "int", nullable: true),
                    SupplierLeadTimes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductCh__Chann__6754599E",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__ProductCh__Produ__66603565",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductWa__Chann__7A672E12",
                        column: x => x.ChannelId,
                        principalTable: "MarketPlace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__ProductWa__Produ__7B5B524B",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__ProductWa__Wareh__797309D9",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryVariant_ChannelId",
                table: "CountryVariant",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryVariant_CountryId",
                table: "CountryVariant",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ChannelId",
                table: "Feedback",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ProductVariantId",
                table: "Feedback",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlace_CountryId",
                table: "MarketPlace",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlace_PaymentId",
                table: "MarketPlace",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlaceOrder_StoreId",
                table: "MarketPlaceOrder",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductVariantId",
                table: "OrderDetail",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_ChannelId",
                table: "OrderInfo",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_UserId",
                table: "OrderInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInfo_VoucherId",
                table: "OrderInfo",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CountryId",
                table: "Payment",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreatedByUserId",
                table: "Payment",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProviderId",
                table: "Product",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VoucherId",
                table: "Product",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductChannel_ChannelId",
                table: "ProductChannel",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductChannel_ProductVariantId",
                table: "ProductChannel",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_VariantId",
                table: "ProductVariant",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_ChannelId",
                table: "ProductWarehouse",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_ProductVariantId",
                table: "ProductWarehouse",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                table: "ProductWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnInfo_OrderId",
                table: "ReturnInfo",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnInfo_UpdatedByUserId",
                table: "ReturnInfo",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_MarketPlaceId",
                table: "Store",
                column: "MarketPlaceId");

            migrationBuilder.CreateIndex(
                name: "UQ__UserInfo__C9F28456D2DAD606",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_ChannelId",
                table: "Voucher",
                column: "ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiMethod");

            migrationBuilder.DropTable(
                name: "CountryVariant");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "MarketPlaceOrder");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductChannel");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductWarehouse");

            migrationBuilder.DropTable(
                name: "ReturnInfo");

            migrationBuilder.DropTable(
                name: "TaskInfo");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "OrderInfo");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Variant");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "MarketPlace");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
