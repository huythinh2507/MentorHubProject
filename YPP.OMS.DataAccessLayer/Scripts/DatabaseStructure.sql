IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ApiMethod] (
    [Id] int NOT NULL IDENTITY,
    [MarketPlaceType] varchar(20) NULL,
    [Path] varchar(max) NULL,
    [MetaData] varchar(max) NULL,
    CONSTRAINT [PK_ApiMethod] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Country] (
    [Id] int NOT NULL IDENTITY,
    [CountryName] nvarchar(255) NULL,
    [ShortCode] varchar(2) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Provider] (
    [Id] int NOT NULL IDENTITY,
    [ProviderName] nvarchar(255) NULL,
    [FullAddress] nvarchar(255) NULL,
    [PhoneNumber] varchar(15) NULL,
    [Email] varchar(255) NULL,
    CONSTRAINT [PK_Provider] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TaskInfo] (
    [Id] int NOT NULL IDENTITY,
    [TaskName] varchar(20) NULL,
    [RunAt] varchar(20) NULL,
    [MarketPlaceType] varchar(20) NULL,
    [MetaData] varchar(max) NULL,
    [Path] varchar(max) NULL,
    CONSTRAINT [PK_TaskInfo] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserInfo] (
    [Id] int NOT NULL IDENTITY,
    [FullName] nvarchar(255) NULL,
    [PhoneNumber] varchar(15) NULL,
    [DOB] date NULL,
    [Gender] varchar(20) NULL,
    [Email] varchar(50) NULL,
    [UserRole] varchar(7) NULL,
    [FullAddress] varchar(255) NULL,
    [UserStatus] varchar(8) NULL,
    [Facebook] varchar(255) NULL,
    [Instagram] varchar(255) NULL,
    [UserName] varchar(16) NOT NULL,
    [UserPassword] varchar(255) NOT NULL,
    [Avatar] varbinary(max) NULL,
    CONSTRAINT [PK_UserInfo] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Variant] (
    [Id] int NOT NULL IDENTITY,
    [VariantName] varchar(255) NULL,
    CONSTRAINT [PK_Variant] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Warehouse] (
    [Id] int NOT NULL IDENTITY,
    [FullAddress] nvarchar(255) NULL,
    [WarehouseName] varchar(255) NULL,
    [PhoneNumber] varchar(15) NULL,
    [CreatedAt] datetime NULL,
    [LastUpdatedAt] datetime NULL,
    CONSTRAINT [PK_Warehouse] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Payment] (
    [Id] int NOT NULL IDENTITY,
    [PaymentMethod] varchar(255) NULL,
    [CardHolder] varchar(255) NULL,
    [CardNumber] varchar(16) NULL,
    [CountryId] int NULL,
    [CreatedByUserId] int NULL,
    [CreatedAt] datetime NULL,
    [LastUpdatedAt] datetime NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Payment__Country__778AC167] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__Payment__Created__787EE5A0] FOREIGN KEY ([CreatedByUserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [MarketPlace] (
    [Id] int NOT NULL IDENTITY,
    [MarketPlaceName] varchar(50) NULL,
    [MarketPlaceImage] varbinary(max) NULL,
    [Type] varchar(20) NULL,
    [URI] varchar(max) NULL,
    [AppId] varchar(max) NULL,
    [AppSecret] varchar(max) NULL,
    [MetaData] varchar(max) NULL,
    [PartnerId] int NULL,
    [RedirectURL] varchar(max) NULL,
    [ExpireDuration] int NULL,
    [Inactive] tinyint NULL,
    [Duration] int NULL,
    [ConfigName] varchar(255) NULL,
    [ConfigURL] varchar(255) NULL,
    [CountryId] int NULL,
    [PaymentId] int NULL,
    [CreatedAt] datetime NULL,
    [LiveStock] int NULL,
    [DelistedStock] int NULL,
    [OutOfStock] int NULL,
    [LowStock] int NULL,
    [OnDemand] int NULL,
    [LastUpdatedAt] datetime NULL,
    [IsDelete] tinyint NULL,
    CONSTRAINT [PK_MarketPlace] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__MarketPla__Count__6A30C649] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__MarketPla__Payme__6B24EA82] FOREIGN KEY ([PaymentId]) REFERENCES [Payment] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [CountryVariant] (
    [Id] int NOT NULL IDENTITY,
    [CountryId] int NULL,
    [ChannelId] int NULL,
    [Value] varchar(255) NULL,
    CONSTRAINT [PK_CountryVariant] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__CountryVa__Chann__75A278F5] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__CountryVa__Count__74AE54BC] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [Store] (
    [Id] int NOT NULL IDENTITY,
    [StoreName] varchar(50) NULL,
    [MarketPlaceId] int NULL,
    [Token] varchar(max) NULL,
    [CreatedAt] datetime NULL,
    [ExpiresAt] datetime NULL,
    [RefreshToken] varchar(max) NULL,
    [Location] varchar(50) NULL,
    [ShopId] varchar(max) NULL,
    [Inactive] tinyint NULL,
    [RunAt] varchar(20) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Store__MarketPla__7D439ABD] FOREIGN KEY ([MarketPlaceId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [Voucher] (
    [Id] int NOT NULL IDENTITY,
    [ChannelId] int NULL,
    [VoucherName] varchar(255) NULL,
    [VoucherCode] varchar(20) NULL,
    [VoucherType] varchar(20) NULL,
    [Apply] varchar(17) NULL,
    [VoucherDiscountType] varchar(20) NULL,
    [LimitPerCustomer] int NULL,
    [PeriodStartTime] date NULL,
    [PeriodEndTime] date NULL,
    [OrderUsedBudget] float NULL,
    [CollectStartAt] date NULL,
    [OfferingMoneyValueOff] float NULL,
    [MaxDiscountOfferingValueOff] int NULL,
    [CriteriaOverMoney] int NOT NULL,
    [VoucherStatus] varchar(9) NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Voucher__Channel__6C190EBB] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [MarketPlaceOrder] (
    [Id] int NOT NULL IDENTITY,
    [MetaData] varchar(max) NULL,
    [StoreId] int NULL,
    [RunAt] varchar(20) NULL,
    [RunDate] varchar(20) NULL,
    [OrderedAt] varchar(20) NULL,
    [MarketPlaceOrderNumber] int NULL,
    CONSTRAINT [PK_MarketPlaceOrder] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__MarketPla__Store__7C4F7684] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [OrderInfo] (
    [Id] int NOT NULL IDENTITY,
    [MetaData] varchar(max) NULL,
    [OrderedAt] datetime NULL,
    [ChannelId] int NULL,
    [UserId] int NULL,
    [TotalPrice] float NULL,
    [VoucherId] int NULL,
    [Note] varchar(255) NULL,
    [OrderNumber] varchar(20) NULL,
    [TaxCode] varchar(20) NULL,
    [CancelBy] varchar(20) NULL,
    [CancelReason] varchar(255) NULL,
    [BuyerId] int NULL,
    [BuyerName] nvarchar(255) NULL,
    [ShippingAddress] varchar(255) NULL,
    [RecipientName] nvarchar(255) NULL,
    [RecipientPhoneNumber] varchar(255) NULL,
    [Country] nvarchar(255) NULL,
    [City] nvarchar(255) NULL,
    [District] nvarchar(255) NULL,
    [Ward] nvarchar(255) NULL,
    [ZipCode] varchar(10) NULL,
    [ShippingServiceCost] float NULL,
    [ShippingFee] float NULL,
    [ShipmentProvider] nvarchar(255) NULL,
    [CustomerPaymentMethod] varchar(20) NULL,
    [OrderStatus] varchar(20) NULL,
    CONSTRAINT [PK_OrderInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__OrderInfo__Chann__6EF57B66] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__OrderInfo__UserI__6FE99F9F] FOREIGN KEY ([UserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__OrderInfo__Vouch__70DDC3D8] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [ProductName] varchar(255) NULL,
    [Barcode] varchar(50) NULL,
    [BasePrice] float NULL,
    [Cost] float NULL,
    [ProductHeight] int NULL,
    [ProductWidth] int NULL,
    [ProductLength] int NULL,
    [ProductWeight] int NULL,
    [ProductDescription] varchar(255) NULL,
    [CreatedAt] datetime NULL,
    [VoucherId] int NULL,
    [ProviderId] int NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Product__Provide__68487DD7] FOREIGN KEY ([ProviderId]) REFERENCES [Provider] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__Product__Voucher__693CA210] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [ReturnInfo] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NULL,
    [ReturnedAt] datetime NULL,
    [Reason] nvarchar(255) NULL,
    [UpdatedByUserId] int NULL,
    [LastUpdatedAt] datetime NULL,
    [ReturnStatus] varchar(9) NULL,
    CONSTRAINT [PK_ReturnInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__ReturnInf__Order__628FA481] FOREIGN KEY ([OrderId]) REFERENCES [OrderInfo] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__ReturnInf__Updat__6383C8BA] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [ProductImage] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NULL,
    [ProductImage] varbinary(max) NULL,
    [ImageDescription] varchar(255) NULL,
    CONSTRAINT [PK_ProductImage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__ProductIm__Produ__73BA3083] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [ProductVariant] (
    [Id] int NOT NULL IDENTITY,
    [ProductId] int NULL,
    [VariantId] int NULL,
    [ProductVariantValue] varchar(255) NULL,
    [SKU] varchar(20) NULL,
    [ProductVariantImage] varbinary(max) NULL,
    CONSTRAINT [PK_ProductVariant] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__ProductVa__Produ__71D1E811] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__ProductVa__Varia__72C60C4A] FOREIGN KEY ([VariantId]) REFERENCES [Variant] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [Feedback] (
    [Id] int NOT NULL IDENTITY,
    [ProductVariantId] int NULL,
    [BuyerName] nvarchar(50) NULL,
    [ChannelId] int NULL,
    [CreatedAt] datetime NULL,
    [FeedbackDescription] varchar(255) NULL,
    [Rating] tinyint NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__Feedback__Channe__6E01572D] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__Feedback__Produc__6D0D32F4] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [OrderDetail] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NULL,
    [ProductId] int NULL,
    [ProductVariantId] int NULL,
    [Quantity] int NULL,
    [PricePerItem] float NULL,
    [DiscountedPrice] float NULL,
    [PromotionType] varchar(50) NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__OrderDeta__Order__6477ECF3] FOREIGN KEY ([OrderId]) REFERENCES [OrderInfo] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__OrderDeta__Produ__656C112C] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__OrderDeta__Produ__76969D2E] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [ProductChannel] (
    [Id] int NOT NULL IDENTITY,
    [ProductVariantId] int NULL,
    [ChannelId] int NULL,
    [Quantity] int NULL,
    [MinimumThreshold] int NULL,
    [Price] float NULL,
    [Issue] int NULL,
    [Lost] int NULL,
    [InProcess] int NULL,
    [Sold] int NULL,
    [CSKU] varchar(20) NULL,
    [OnDemandRequired] bit NULL,
    [LastUpdatedAt] datetime NULL,
    [ProductChannelStatus] varchar(8) NULL,
    [LastSevenDaySalesAvg] int NULL,
    [SupplierLeadTimes] int NULL,
    CONSTRAINT [PK_ProductChannel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__ProductCh__Chann__6754599E] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__ProductCh__Produ__66603565] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [ProductWarehouse] (
    [Id] int NOT NULL IDENTITY,
    [ProductVariantId] int NULL,
    [ChannelId] int NULL,
    [WarehouseId] int NULL,
    [Quantity] int NULL,
    CONSTRAINT [PK_ProductWarehouse] PRIMARY KEY ([Id]),
    CONSTRAINT [FK__ProductWa__Chann__7A672E12] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__ProductWa__Produ__7B5B524B] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK__ProductWa__Wareh__797309D9] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouse] ([Id]) ON DELETE SET NULL
);
GO

CREATE INDEX [IX_CountryVariant_ChannelId] ON [CountryVariant] ([ChannelId]);
GO

CREATE INDEX [IX_CountryVariant_CountryId] ON [CountryVariant] ([CountryId]);
GO

CREATE INDEX [IX_Feedback_ChannelId] ON [Feedback] ([ChannelId]);
GO

CREATE INDEX [IX_Feedback_ProductVariantId] ON [Feedback] ([ProductVariantId]);
GO

CREATE INDEX [IX_MarketPlace_CountryId] ON [MarketPlace] ([CountryId]);
GO

CREATE INDEX [IX_MarketPlace_PaymentId] ON [MarketPlace] ([PaymentId]);
GO

CREATE INDEX [IX_MarketPlaceOrder_StoreId] ON [MarketPlaceOrder] ([StoreId]);
GO

CREATE INDEX [IX_OrderDetail_OrderId] ON [OrderDetail] ([OrderId]);
GO

CREATE INDEX [IX_OrderDetail_ProductId] ON [OrderDetail] ([ProductId]);
GO

CREATE INDEX [IX_OrderDetail_ProductVariantId] ON [OrderDetail] ([ProductVariantId]);
GO

CREATE INDEX [IX_OrderInfo_ChannelId] ON [OrderInfo] ([ChannelId]);
GO

CREATE INDEX [IX_OrderInfo_UserId] ON [OrderInfo] ([UserId]);
GO

CREATE INDEX [IX_OrderInfo_VoucherId] ON [OrderInfo] ([VoucherId]);
GO

CREATE INDEX [IX_Payment_CountryId] ON [Payment] ([CountryId]);
GO

CREATE INDEX [IX_Payment_CreatedByUserId] ON [Payment] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Product_ProviderId] ON [Product] ([ProviderId]);
GO

CREATE INDEX [IX_Product_VoucherId] ON [Product] ([VoucherId]);
GO

CREATE INDEX [IX_ProductChannel_ChannelId] ON [ProductChannel] ([ChannelId]);
GO

CREATE INDEX [IX_ProductChannel_ProductVariantId] ON [ProductChannel] ([ProductVariantId]);
GO

CREATE INDEX [IX_ProductImage_ProductId] ON [ProductImage] ([ProductId]);
GO

CREATE INDEX [IX_ProductVariant_ProductId] ON [ProductVariant] ([ProductId]);
GO

CREATE INDEX [IX_ProductVariant_VariantId] ON [ProductVariant] ([VariantId]);
GO

CREATE INDEX [IX_ProductWarehouse_ChannelId] ON [ProductWarehouse] ([ChannelId]);
GO

CREATE INDEX [IX_ProductWarehouse_ProductVariantId] ON [ProductWarehouse] ([ProductVariantId]);
GO

CREATE INDEX [IX_ProductWarehouse_WarehouseId] ON [ProductWarehouse] ([WarehouseId]);
GO

CREATE INDEX [IX_ReturnInfo_OrderId] ON [ReturnInfo] ([OrderId]);
GO

CREATE INDEX [IX_ReturnInfo_UpdatedByUserId] ON [ReturnInfo] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_Store_MarketPlaceId] ON [Store] ([MarketPlaceId]);
GO

CREATE UNIQUE INDEX [UQ__UserInfo__C9F28456D2DAD606] ON [UserInfo] ([UserName]);
GO

CREATE INDEX [IX_Voucher_ChannelId] ON [Voucher] ([ChannelId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230426031750_omsdbv1', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CountryVariant] DROP CONSTRAINT [FK__CountryVa__Chann__75A278F5];
GO

ALTER TABLE [CountryVariant] DROP CONSTRAINT [FK__CountryVa__Count__74AE54BC];
GO

ALTER TABLE [Feedback] DROP CONSTRAINT [FK__Feedback__Channe__6E01572D];
GO

ALTER TABLE [Feedback] DROP CONSTRAINT [FK__Feedback__Produc__6D0D32F4];
GO

ALTER TABLE [MarketPlace] DROP CONSTRAINT [FK__MarketPla__Count__6A30C649];
GO

ALTER TABLE [MarketPlace] DROP CONSTRAINT [FK__MarketPla__Payme__6B24EA82];
GO

ALTER TABLE [MarketPlaceOrder] DROP CONSTRAINT [FK__MarketPla__Store__7C4F7684];
GO

ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK__OrderDeta__Order__6477ECF3];
GO

ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK__OrderDeta__Produ__656C112C];
GO

ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK__OrderDeta__Produ__76969D2E];
GO

ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK__OrderInfo__Chann__6EF57B66];
GO

ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK__OrderInfo__UserI__6FE99F9F];
GO

ALTER TABLE [OrderInfo] DROP CONSTRAINT [FK__OrderInfo__Vouch__70DDC3D8];
GO

ALTER TABLE [Payment] DROP CONSTRAINT [FK__Payment__Country__778AC167];
GO

ALTER TABLE [Payment] DROP CONSTRAINT [FK__Payment__Created__787EE5A0];
GO

ALTER TABLE [Product] DROP CONSTRAINT [FK__Product__Provide__68487DD7];
GO

ALTER TABLE [Product] DROP CONSTRAINT [FK__Product__Voucher__693CA210];
GO

ALTER TABLE [ProductChannel] DROP CONSTRAINT [FK__ProductCh__Chann__6754599E];
GO

ALTER TABLE [ProductChannel] DROP CONSTRAINT [FK__ProductCh__Produ__66603565];
GO

ALTER TABLE [ProductImage] DROP CONSTRAINT [FK__ProductIm__Produ__73BA3083];
GO

ALTER TABLE [ProductVariant] DROP CONSTRAINT [FK__ProductVa__Produ__71D1E811];
GO

ALTER TABLE [ProductVariant] DROP CONSTRAINT [FK__ProductVa__Varia__72C60C4A];
GO

ALTER TABLE [ProductWarehouse] DROP CONSTRAINT [FK__ProductWa__Chann__7A672E12];
GO

ALTER TABLE [ProductWarehouse] DROP CONSTRAINT [FK__ProductWa__Produ__7B5B524B];
GO

ALTER TABLE [ProductWarehouse] DROP CONSTRAINT [FK__ProductWa__Wareh__797309D9];
GO

ALTER TABLE [ReturnInfo] DROP CONSTRAINT [FK__ReturnInf__Order__628FA481];
GO

ALTER TABLE [ReturnInfo] DROP CONSTRAINT [FK__ReturnInf__Updat__6383C8BA];
GO

ALTER TABLE [Store] DROP CONSTRAINT [FK__Store__MarketPla__7D439ABD];
GO

ALTER TABLE [Voucher] DROP CONSTRAINT [FK__Voucher__Channel__6C190EBB];
GO

EXEC sp_rename N'[UserInfo].[UQ__UserInfo__C9F28456D2DAD606]', N'UQ__UserInfo__C9F28456B7A4B4AD', N'INDEX';
GO

ALTER TABLE [OrderInfo] ADD [InsertedAt] varchar(20) NULL;
GO

ALTER TABLE [OrderInfo] ADD [MarketPlaceOrderId] int NULL;
GO

ALTER TABLE [OrderInfo] ADD [StoreId] int NULL;
GO

ALTER TABLE [OrderInfo] ADD [UpdatedAt] varchar(20) NULL;
GO

CREATE INDEX [IX_OrderInfo_StoreId] ON [OrderInfo] ([StoreId]);
GO

ALTER TABLE [CountryVariant] ADD CONSTRAINT [FK__CountryVa__Chann__60A75C0F] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [CountryVariant] ADD CONSTRAINT [FK__CountryVa__Count__5FB337D6] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Feedback] ADD CONSTRAINT [FK__Feedback__Channe__59063A47] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Feedback] ADD CONSTRAINT [FK__Feedback__Produc__5812160E] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [MarketPlace] ADD CONSTRAINT [FK__MarketPla__Count__5535A963] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [MarketPlace] ADD CONSTRAINT [FK__MarketPla__Payme__5629CD9C] FOREIGN KEY ([PaymentId]) REFERENCES [Payment] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [MarketPlaceOrder] ADD CONSTRAINT [FK__MarketPla__Store__6754599E] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK__OrderDeta__Order__4F7CD00D] FOREIGN KEY ([OrderId]) REFERENCES [OrderInfo] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK__OrderDeta__Produ__5070F446] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK__OrderDeta__Produ__619B8048] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderInfo] ADD CONSTRAINT [FK__OrderInfo__Chann__59FA5E80] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderInfo] ADD CONSTRAINT [FK__OrderInfo__Store__693CA210] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderInfo] ADD CONSTRAINT [FK__OrderInfo__UserI__5AEE82B9] FOREIGN KEY ([UserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [OrderInfo] ADD CONSTRAINT [FK__OrderInfo__Vouch__5BE2A6F2] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Payment] ADD CONSTRAINT [FK__Payment__Country__628FA481] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Payment] ADD CONSTRAINT [FK__Payment__Created__6383C8BA] FOREIGN KEY ([CreatedByUserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK__Product__Provide__534D60F1] FOREIGN KEY ([ProviderId]) REFERENCES [Provider] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK__Product__Voucher__5441852A] FOREIGN KEY ([VoucherId]) REFERENCES [Voucher] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductChannel] ADD CONSTRAINT [FK__ProductCh__Chann__52593CB8] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductChannel] ADD CONSTRAINT [FK__ProductCh__Produ__5165187F] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductImage] ADD CONSTRAINT [FK__ProductIm__Produ__5EBF139D] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductVariant] ADD CONSTRAINT [FK__ProductVa__Produ__5CD6CB2B] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductVariant] ADD CONSTRAINT [FK__ProductVa__Varia__5DCAEF64] FOREIGN KEY ([VariantId]) REFERENCES [Variant] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductWarehouse] ADD CONSTRAINT [FK__ProductWa__Chann__656C112C] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductWarehouse] ADD CONSTRAINT [FK__ProductWa__Produ__66603565] FOREIGN KEY ([ProductVariantId]) REFERENCES [ProductVariant] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ProductWarehouse] ADD CONSTRAINT [FK__ProductWa__Wareh__6477ECF3] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouse] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ReturnInfo] ADD CONSTRAINT [FK__ReturnInf__Order__4D94879B] FOREIGN KEY ([OrderId]) REFERENCES [OrderInfo] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [ReturnInfo] ADD CONSTRAINT [FK__ReturnInf__Updat__4E88ABD4] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [UserInfo] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Store] ADD CONSTRAINT [FK__Store__MarketPla__68487DD7] FOREIGN KEY ([MarketPlaceId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [Voucher] ADD CONSTRAINT [FK__Voucher__Channel__571DF1D5] FOREIGN KEY ([ChannelId]) REFERENCES [MarketPlace] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230426042915_update_omsdb_v1', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderInfo]') AND [c].[name] = N'OrderNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [OrderInfo] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [OrderInfo] DROP COLUMN [OrderNumber];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MarketPlace]') AND [c].[name] = N'PartnerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [MarketPlace] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [MarketPlace] DROP COLUMN [PartnerId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderInfo]') AND [c].[name] = N'MarketPlaceOrderId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [OrderInfo] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [OrderInfo] ALTER COLUMN [MarketPlaceOrderId] varchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MarketPlaceOrder]') AND [c].[name] = N'MarketPlaceOrderNumber');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [MarketPlaceOrder] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [MarketPlaceOrder] ALTER COLUMN [MarketPlaceOrderNumber] varchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230426083402_update_omsdb_v2', N'7.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Store] ADD [Code] varchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MarketPlaceOrder]') AND [c].[name] = N'RunDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [MarketPlaceOrder] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [MarketPlaceOrder] ALTER COLUMN [RunDate] datetime2 NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MarketPlaceOrder]') AND [c].[name] = N'RunAt');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [MarketPlaceOrder] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [MarketPlaceOrder] ALTER COLUMN [RunAt] datetime2 NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MarketPlaceOrder]') AND [c].[name] = N'OrderedAt');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [MarketPlaceOrder] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [MarketPlaceOrder] ALTER COLUMN [OrderedAt] datetime2 NULL;
GO

ALTER TABLE [MarketPlace] ADD [Scope] varchar(max) NULL;
GO

ALTER TABLE [MarketPlace] ADD [ServiceFullName] varchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230508050029_update_omsdb_v3', N'7.0.4');
GO

COMMIT;
GO

