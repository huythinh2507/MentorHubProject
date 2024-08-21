namespace YPP.MH.BusinessLogicLayer.Util
{
    public sealed class Constants
    {
        public const string GET_TOKEN_BY_CODE = "NewToken";
        public const string GET_TOKEN_BY_REFRESH_TOKEN = "RefeshToken";
        public const string GET_SIGN = "sign";
        public const string GET_TIKI_SERVICE = "TIKI";
        public const string GET_TIKI_ORDER_DATA_SERVICE = "TIKI.Orders";
        public const string GET_TIKI_PRODUCT_DATA_SERVICE = "TIKI.Products";
        public const string GET_SHOPEE_SERVICE = "SHOPEE";
        public const string GET_SHOPEE_ORDER_DATA_SERVICE = "SHOPEE.Orders";
        public const string GET_SHOPEE_UPDATE_ORDER_DATA_SERVICE = "SHOPEE.UpdateOrder";
        public const string GET_SHOPEE_ORDER_DETAIL_DATA_SERVICE = "SHOPEE.OrderDetail";
        public const string GET_SHOPEE_PRODUCT_DATA_SERVICE = "SHOPEE.Products";
        public const string GET_SHOPEE_PRODUCT_DETAIL_DATA_SERVICE = "SHOPEE.ProductDetail";
        public const string GET_LAZADA_SERVICE = "LAZADA";
        public const string GET_LAZADA_ORDER_DATA_SERVICE = "LAZADA.Orders";
        public const string GET_LAZADA_PRODUCT_DATA_SERVICE = "LAZADA.Products";
        public const string GET_ORDER_SN_LIST = "<<order_sn_list>>";
        public const string GET_ITEM_ID_LIST = "<<item_id_list>>";
        public const string GET_TIKI_TOKEN = "<<tiki_token>>";
        public const string GET_ACCESS_TOKEN = "<<access_token>>";
        public const string GET_REDIRECT_URI = "<<redirect_uri>>";
        public const string GET_CLIENT_ID = "<<client_id>>";
        public const string GET_CODE = "<<code>>";
        public const string GET_TIMESTAMP = "<<timestamp>>";
        public const string GET_SHOP_ID = "<<shop_id>>";
        public const string GET_REFRESH_TOKEN = "<<refresh_token>>";
        public const string GET_AUTHORIZATION_BASIC = "<<authorization_basic>>";
        public const string GET_TIME_FROM = "<<time_from>>";
        public const string GET_TIME_TO = "<<time_to>>";
        public const string GET_SHOPEE_PAGE_SIZE = "<<shopee_page_size>>";
        public const string GET_TIME_RANGE_FIELD = "<<time_range_field>>";
        public const string GET_SCOPE = "<<scope>";
        public const string GET_STATE = "<<state>";
        public const string GET_UPDATE_TIME = "update_time";
        public const string GET_CREATE_TIME = "create_time";
        public const string GET_LANGUAGE = "<<language>>";
        public const string GET_LANGUAGE_VN = "en/vi";
        public const string GET_PAGE_SIZE = "<<page_size>>";
        public const string GET_PAGE_SIZE_VALUE = "50";
        public const string GET_CURSOR = "<<cursor>>";
        public const string GET_OFFSET = "<<offset>>";
        public const string GET_CURRENT_PAGE = "<<page>>";
        public const string GET_DATA_SN_LIST = "<<data_sn_list>>";

        #region status
        public const string GET_BANNED_STATUS = "Banned";
        public const string GET_DELETED_STATUS = "Deleted";
        public const string GET_UNLIST_STATUS = "Unlist";
        public const string GET_HIDDEN_STATUS = "Hidden";
        public const string GET_DEACTIVE_STATUS = "Inactive";
        public const string GET_HOLDED_STATUS = "Holded";
        public const string GET_LOST_STATUS = "Lost";
        public const string GET_PROCESSING_STATUS = "Processing";
        public const string GET_PROCESSED_STATUS = "Processed";
        public const string GET_WATING_PAYMENT_STATUS = "Waiting_payment";
        public const string GET_PACKAGING_STATUS = "Packaging";
        public const string GET_PICKING_STATUS = "Picking";
        public const string GET_SHIPPING_STATUS = "Shipping";
        public const string GET_PAID_STATUS = "Paid";
        public const string GET_UNPAID_STATUS = "UNPAID";
        public const string GET_RETURN_STATUS = "Returned";
        public const string GET_FINISHED_PACKING_STATUS = "Finished_packing";
        public const string GET_TO_PACK_STATUS = "Topack";
        public const string GET_TO_SHIP_STATUS = "Toship";
        public static readonly string[] GET_LIVE_STATUS = { "Active" };
        public static readonly string[] GET_INACTIVE_STATUS = { GET_BANNED_STATUS, GET_DELETED_STATUS, GET_HIDDEN_STATUS, GET_UNLIST_STATUS, GET_DEACTIVE_STATUS };
        public static readonly string[] GET_PROBLEM_STATUS = { GET_HOLDED_STATUS, GET_UNPAID_STATUS, GET_LOST_STATUS, GET_WATING_PAYMENT_STATUS };
        #endregion

        public const string GET_ORDER_COMPLETED_STATUS = "Completed";
        public const string GET_ORDER_RETURNED_STATUS = "Return";
        public const string GET_ORDER_FAILED_STATUS = "Failed";
        public const string GET_ORDER_CANCELLED_STATUS = "Cancelled";

        public const string GET_ACTIVE_STATUS = "Active";

        public static readonly string[] GET_ORDER_INVALID_STATUS = { GET_ORDER_RETURNED_STATUS, GET_ORDER_CANCELLED_STATUS, GET_ORDER_FAILED_STATUS };

        public const string GET_HIGH_LOYALTY = "High loyalty";
        public const string GET_NORMAL_LOYALTY = "Normal loyalty";
        public const string GET_LOW_LOYALTY = "Low loyalty";

        public const string LIMIT_KEY_NAME = "<<limit>>";
        public const string PAGE_KEY_NAME = "page";
        public const string OFFSET_KEY_NAME = "offset";
        public const string SIGN_KEY_NAME = "sign";
        public const int LIMIT_DEFAULT = 50;
        public const string DEFAULT_VALUE = "0";


        #region upsert
        public const string TIKI = "TIKI";
        public const string SHOPEE = "SHOPEE";
        public const string LAZADA = "LAZADA";
        public const string UPSERT_ORDER_DATA = "UpsertOrderData";
        public const string SP_UPSERT_ORDER_DATA = "sp_UpsertOrderData";
        public const string UPSERT_PRODUCT_DATA = "UpsertProductData";
        public const string SP_UPSERT_PRODUCT_DATA = "sp_UpsertProductData";
        public const string UPSERT_SELLER_DATA = "UpsertSellerData";
        public const string SP_UPSERT_SELLER_DATA = "sp_UpsertSellerData";
        public const string TIKI_ORDER = "TikiOrder";
        public const string SHOPEE_ORDER = "ShopeeOrder";
        public const string LAZADA_ORDER = "LazadaOrder";
        public const string TIKI_PRODUCT = "TikiProduct";
        public const string SHOPEE_PRODUCT = "ShopeeProduct";
        public const string LAZADA_PRODUCT = "LazadaProduct";
        public const string TIKI_SELLER = "TikiSeller";
        public const string SHOPEE_SELLER = "MerchantInfo";
        public const string LAZADA_SELLER = "LazadaSeller";
        #endregion

        #region meta mapping
        public const string TIKI_PRODUCT_CHANNEL_MAPPING_METADATA = "MappingMetadata:ProductChannel:TIKI";
        public const string SHOPEE_PRODUCT_CHANNEL_MAPPING_METADATA = "MappingMetadata:ProductChannel:TIKI";
        public const string LAZADA_PRODUCT_CHANNEL_MAPPING_METADATA = "MappingMetadata:ProductChannel:TIKI";

        public const string TIKI_SELLER_MAPPING_METADATA = "MappingMetadata:SellerInfo:TIKI";
        public const string SHOPEE_SELLER_MAPPING_METADATA = "MappingMetadata:SellerInfo:SHOPEE";
        public const string LAZADA_SELLER_MAPPING_METADATA = "MappingMetadata:SellerInfo:LAZADA";

        public const string TIKI_ORDER_MAPPING_METADATA = "MappingMetadata:OrderInfo:TIKI";
        public const string SHOPEE_ORDER_MAPPING_METADATA = "MappingMetadata:OrderInfo:SHOPEE";
        public const string LAZADA_ORDER_MAPPING_METADATA = "MappingMetadata:OrderInfo:LAZADA";

        #endregion


        #region user role
        public const string GET_MANAGER_ROLE = "MANAGER";
        public const string GET_SALER_ROLE = "SALER";
        public const string GET_ADMIN_ROLE = "ADMIN";
        #endregion

        #region title
        public const string GET_TOTAL_SALES_TITLE = "Total Sales";
        public const string GET_NUMBER_OF_SOLD_PRODUCT_TITLE = "#Sold Product";
        public const string GET_GROSS_PROFIT_TITLE = "Gross Profit";
        public const string GET_GROSS_PROFIT_MARGIN_TITLE = "Gross Profit Margin";
        public const string GET_AVG_ORDER_TITLE = "Avg Order";
        public const string GET_TOTAL_RETURN_TITLE = "Total Return";

        #endregion

        #region stock status
        public const string GET_AVAILABLE_STOCK_STATUS = "stockavailable";
        public const string GET_LOW_STOCK_STATUS = "lowofstock";
        public const string GET_OUT_OF_STOCK_STATUS = "outofstock";
        public const string GET_LIVE_STOCK_STATUS = "live";
        public const string GET_ON_DEMAND_STOCK_STATUS = "ondemand";
        #endregion

        public const string CHANNEL_IMAGE_URL = "Channel/image?id=";
        public const string PRODUCT_IMAGE_URL = "Product/image?id=";
        public const string PRODUCT_THUMBNAIL_URL = "Product/thumbnail?id=";
        public const string USER_IMAGE_URL = "User/image?id=";
        public const string STORE_IMAGE_URL = "Channel/store/image?id=";

    }
}
