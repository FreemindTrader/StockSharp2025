// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.PaymentExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class PaymentExtensions
{
    private const string _orderInfo = "orderInfo";
    private const string _client = "client";
    private const string _amount = "amount";
    private const string _repeatAmount = "repeatAmount";
    private const string _currency = "curr";
    private const string _priceType = "priceType";
    private const string _description = "description";
    private const string _test = "test";
    public const string Data = "data";

    public static string GetData(this IDictionary<string, string> qs)
    {
        string str;
        return !qs.TryGetValue("data", out str) ? (string)null : str;
    }

    public static string GetData(this QueryString qs) => qs.TryGetValue<string>("data");

    public static void SetData(this QueryString qs, string value) => qs["data"] = (object)value;

    public static (Type type, long id) GetOrderInfo(this QueryString qs)
    {
        string str = qs.TryGetValue<string>("orderInfo");
        if (StringHelper.IsEmpty(str))
            return new ValueTuple<Type, long>();

        string[] strArray = str.Split('-');
        return (UrlExtensions.GetEntityType(strArray[0], false), Converter.To<long>((object)strArray[1]));
    }

    public static (string key, string value) CreateOrderInfo(Type type, long id)
    {
        return ("orderInfo", $"{UrlExtensions.GetIdentity(type, false)}-{id}");
    }

    public static void SetOrderInfo(this QueryString qs, Type type, long id)
    {
        (string key, string value) = PaymentExtensions.CreateOrderInfo(type, id);
        qs[key] = (object)value;
    }

    public static string GetDescription(this QueryString qs) => qs.TryGetValue<string>("description");

    public static void SetDescription(this QueryString qs, string value)
    {
        qs["description"] = (object)value;
    }

    public static long? GetClient(this QueryString qs) => qs.TryGetValue<long?>("client");

    public static void SetClient(this QueryString qs, long value) => qs["client"] = (object)value;

    public static Decimal? GetAmount(this QueryString qs)
    {
        return qs.TryGetValue<string>("amount").ToPrice();
    }

    public static void SetAmount(this QueryString qs, Decimal value)
    {
        qs["amount"] = (object)PaymentExtensions.ToString(value);
    }

    public static Decimal? GetRepeatAmount(this QueryString qs)
    {
        return qs.TryGetValue<string>("repeatAmount").ToPrice();
    }

    public static void SetRepeatAmount(this QueryString qs, Decimal value)
    {
        qs["repeatAmount"] = (object)PaymentExtensions.ToString(value);
    }

    public static CurrencyTypes? GetCurrency(this QueryString qs)
    {
        return qs.TryGetValue<CurrencyTypes?>("curr");
    }

    public static void SetCurrency(this QueryString qs, CurrencyTypes value)
    {
        qs["curr"] = (object)value;
    }

    public static ProductPriceTypes? GetPriceType(this QueryString qs)
    {
        return new ProductPriceTypes?(qs.TryGetValue<ProductPriceTypes>("priceType"));
    }

    public static void SetPriceType(this QueryString qs, ProductPriceTypes priceType)
    {
        qs[nameof(priceType)] = (object)priceType;
    }

    public static bool? GetTest(this QueryString qs) => qs.TryGetValue<bool?>("test");

    public static void SetTest(this QueryString qs, bool value)
    {
        qs["test"] = (object)(value ? 1 : 0);
    }

    private static string ToString(Decimal d)
    {
        return StringHelper.RemoveMultipleWhitespace(d.ToString()).Replace(',', '.');
    }

    public static Decimal? ToPrice(this string str)
    {
        return str == null || StringHelper.IsEmpty(str.Trim()) ? new Decimal?() : new Decimal?(Decimal.Parse(str.Replace(',', '.'), (IFormatProvider)CultureInfo.InvariantCulture));
    }
}
