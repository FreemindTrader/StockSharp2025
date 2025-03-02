using Ecng.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;

namespace StockSharp.Web.Api.Http
{
    public static class IHeaderDictionaryExtensions
    {
        public static bool IsEmpty(this StringValues str)
        {
            return str == StringValues.Empty;
        }

        public static bool IsTokenEmpty(this string token)
        {
            if (!token.IsEmpty())
                return token == "x";
            return true;
        }

        public static bool HasAuthHeaders(this IHeaderDictionary headers)
        {
            headers.CheckOnNull("value");
            if (headers.ContainsKey("SS-AUTH"))
                return true;
            if (!headers.ContainsKey("SS-LGN"))
                return false;
            if (!headers.ContainsKey("SS-PWD"))
                return headers.ContainsKey("SS-PWD-64");
            return true;
        }

        public static StringValues GetToken(this IHeaderDictionary headers)
        {
            return headers.CheckOnNull("value")["SS-AUTH"];
        }

        public static IPAddress TryGetRealAddress(this IHeaderDictionary headers)
        {
            StringValues stringValues;
            if (!headers.CheckOnNull("value").TryGetValue("SS-ADDR", out stringValues))
                return null;
            return stringValues.ToString().To<IPAddress>();
        }

        public static bool IsExtended(this IHeaderDictionary headers)
        {
            StringValues str;
            if (headers.CheckOnNull("value").TryGetValue("SS-EX", out str) && !str.IsEmpty())
                return str.To<bool>();
            return false;
        }

        public static bool AsUser(this IHeaderDictionary headers, bool defaultValue = false)
        {
            StringValues str;
            if (!headers.CheckOnNull("value").TryGetValue("SS-ASU", out str) || str.IsEmpty())
                return defaultValue;
            return str.To<bool>();
        }

        public static bool TryGetReferer(this IHeaderDictionary headers, out string value)
        {
            return headers.TryGet("Referer", out value);
        }

        public static bool TryGetAuthorization(this IHeaderDictionary headers, out string value)
        {
            return headers.TryGet("Authorization", out value);
        }

        public static void AddWWWAuthenticate(this IHeaderDictionary headers, string value)
        {
            headers.CheckOnNull(nameof(value)).Add("WWW-Authenticate", value);
        }

        private static bool TryGet(this IHeaderDictionary headers, string header, out string value)
        {
            StringValues stringValues;
            if (headers.CheckOnNull(nameof(value)).TryGetValue(header, out stringValues))
            {
                value = stringValues;
                return true;
            }
            value = null;
            return false;
        }
    }
}
