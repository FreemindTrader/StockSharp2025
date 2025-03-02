// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.PaymentExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.Net;
using Newtonsoft.Json.Linq;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StockSharp.Web.Common
{
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

        public static string GetData( this IDictionary<string, string> qs )
        {
            string str;
            if ( !qs.TryGetValue( "data", out str ) )
                return ( string ) null;
            return str;
        }

        public static string GetData( this QueryString qs )
        {
            return qs.TryGetValue<string>( "data" );
        }

        public static void SetData( this QueryString qs, string value )
        {
            qs ["data"] = ( object ) value;
        }

        
        public static (Type type, long id) GetOrderInfo( this QueryString qs )
        {
            string str = qs.TryGetValue<string>("orderInfo");
            if ( StringHelper.IsEmpty( str ) )
                return new ValueTuple<Type, long>();
            string[] strArray = str.Split('-');
            return new ValueTuple<Type, long>( UrlExtensions.GetEntityType( strArray [0], false ), ( long ) Converter.To<long>( ( object ) strArray [1] ) );
        }

        
        public static (string key, string value) CreateOrderInfo( Type type, long id )
        {
            return new ValueTuple<string, string>( "orderInfo", string.Format( "{0}-{1}", ( object ) UrlExtensions.GetIdentity( type, false ), ( object ) id ) );
        }

        public static void SetOrderInfo( this QueryString qs, Type type, long id )
        {
            ValueTuple<string, string> orderInfo = PaymentExtensions.CreateOrderInfo(type, id);
            string index = orderInfo.Item1;
            string str = orderInfo.Item2;
            qs [index] = ( object ) str;
        }

        public static string GetDescription( this QueryString qs )
        {
            return qs.TryGetValue<string>( "description" );
        }

        public static void SetDescription( this QueryString qs, string value )
        {
            qs ["description"] = ( object ) value;
        }

        public static long? GetClient( this QueryString qs )
        {
            return qs.TryGetValue<long?>( "client" );
        }

        public static void SetClient( this QueryString qs, long value )
        {
            qs ["client"] = ( object ) value;
        }

        public static Decimal? GetAmount( this QueryString qs )
        {
            return qs.TryGetValue<string>( "amount" ).ToPrice();
        }

        public static void SetAmount( this QueryString qs, Decimal value )
        {
            qs ["amount"] = ( object ) PaymentExtensions.ToString( value );
        }

        public static Decimal? GetRepeatAmount( this QueryString qs )
        {
            return qs.TryGetValue<string>( "repeatAmount" ).ToPrice();
        }

        public static void SetRepeatAmount( this QueryString qs, Decimal value )
        {
            qs ["repeatAmount"] = ( object ) PaymentExtensions.ToString( value );
        }

        public static CurrencyTypes? GetCurrency( this QueryString qs )
        {
            return qs.TryGetValue<CurrencyTypes?>( "curr" );
        }

        public static void SetCurrency( this QueryString qs, CurrencyTypes value )
        {
            qs ["curr"] = ( object ) value;
        }

        public static ProductPriceTypes? GetPriceType( this QueryString qs )
        {
            return new ProductPriceTypes?( qs.TryGetValue<ProductPriceTypes>( "priceType" ) );
        }

        public static void SetPriceType( this QueryString qs, ProductPriceTypes priceType )
        {
            qs [nameof( priceType )] = ( object ) priceType;
        }

        public static bool? GetTest( this QueryString qs )
        {
            return qs.TryGetValue<bool?>( "test" );
        }

        public static void SetTest( this QueryString qs, bool value )
        {
            qs ["test"] = ( object ) ( value ? 1 : 0 );
        }

        private static string ToString( Decimal d )
        {
            return StringHelper.RemoveMultipleWhitespace( d.ToString() ).Replace( ',', '.' );
        }

        public static Decimal? ToPrice( this string str )
        {
            if ( str == null || StringHelper.IsEmpty( str.Trim() ) )
                return new Decimal?();
            return new Decimal?( Decimal.Parse( str.Replace( ',', '.' ), ( IFormatProvider ) CultureInfo.InvariantCulture ) );
        }
    }
}
