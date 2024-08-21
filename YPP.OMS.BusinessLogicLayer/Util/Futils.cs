using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace YPP.MH.BusinessLogicLayer.Util
{
    public static class Futils
    {
        /// <summary>
        /// Get percentage diffrency between two periods
        /// </summary>
        /// <param name="currentData"></param>
        /// <param name="previousData"></param>
        /// <returns></returns>
        public static double? GetPercentDiffOverPeriod(double? currentData, double? previousData)
        {
            if (currentData == 0 && previousData > 0) return -100;
            if (previousData == 0 && currentData == 0) return currentData;
            if ((previousData == 0 || !previousData.HasValue) && currentData > 0) return 100;
            return 100 * (currentData / previousData) - 100;
        }

        public static int GetNumberOfMonths(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(-1);
            int months = (toDate.Year - fromDate.Year) * 12 + toDate.Month - fromDate.Month + 1;
            return months;
        }

        public static string GetChannelImageUrl(int id)
        {
            return Constants.CHANNEL_IMAGE_URL + $"{id}";
        }

        public static string GetProductImageUrl(int id)
        {
            return Constants.PRODUCT_IMAGE_URL + $"{id}";
        }

        public static string GetProductThumnailUrl(int id)
        {
            return Constants.PRODUCT_THUMBNAIL_URL + $"{id}";
        }

        public static string GetUserImageUrl(int id)
        {
            return Constants.USER_IMAGE_URL + $"{id}";
        }

        public static string GetStoreImageUrl(int id)
        {
            return Constants.STORE_IMAGE_URL + $"{id}";
        }

        public static void GetImgHighResolution(byte[] imgData)
        {
            throw new NotImplementedException();
        }

        public static DateTime? ConvertIntToDateTime(dynamic value)
        {
            if (value is null)
            {
                return null;
            }
            long parsedValue = (value is string) ? long.Parse(value) : (long)value;
            return (value is null) ? null : DateTimeOffset.FromUnixTimeMilliseconds(parsedValue).DateTime;
        }

        public static DateTime? ConvertStringToDateTime(dynamic? value)
        {
            return (value is null) ? null : DateTime.Parse((string)value!);
        }

        public static string ConvertToString(dynamic value)
        {
            return value + "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">parse string("4.00") to int</param>
        /// <returns></returns>
        public static int ConvertStringToInt(dynamic value)
        {
            if (value is null)
            {
                return 0;
            }
            var parsedValue = Double.Parse(value);
            return (int)parsedValue;
        }
    }
}
