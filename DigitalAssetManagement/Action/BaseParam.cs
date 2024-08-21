using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPP.MH.DigitalAssetManagement.Action
{
    public class BaseParam
    {
        private SortedDictionary<string, object> customParams = new SortedDictionary<string, object>();

        protected static void AddParam(SortedDictionary<string, object> dict, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(key, value);
            }
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, DateTime value)
        {
            if (value != DateTime.MinValue)
            {
                dict.Add(key, value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            }
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, float value)
        {
            dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
        }

        protected static void AddParam(SortedDictionary<string, object> dict, string key, long value)
        {
            dict.Add(key, value.ToString(CultureInfo.InvariantCulture));
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, IEnumerable<string> value)
        {
            if (value != null)
            {
                dict.Add(key, value);
            }
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, bool value)
        {
            dict.Add(key, value ? "true" : "false");
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, bool? value)
        {
            if (!value.HasValue)
            {
                return;
            }

            AddParam(dict, key, value.Value);
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string fileKey, IFormFile file)
        {
            if (file == null)
            {
                return;
            }

            dict.Add(fileKey, file);
        }
        protected static void AddParam(SortedDictionary<string, object> dict, string key, BaseParam parameters)
        {
            if (parameters != null)
            {
                dict.Add(key, parameters.ToParamsDictionary());
            }
        }
        public virtual SortedDictionary<string, object> ToParamsDictionary()
        {
            var dictionary = new SortedDictionary<string, object>(customParams);
            AddParamsToDictionary(dictionary);
            return dictionary;
        }

        protected virtual void AddParamsToDictionary(SortedDictionary<string, object> dict)
        {
        }
    }
}
