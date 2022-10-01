// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.CountryIdToFlagImageSourceConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core;

namespace Ecng.Xaml.Converters
{
    internal static class StaticHelper01
    {
        public static ImageSource ToImageSource( this CountryCodes cc )
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler( 69, 1 );
            interpolatedStringHandler.AppendLiteral( "/Ecng.Xaml;component/Resources/Flags/" );
            interpolatedStringHandler.AppendFormatted<CountryCodes>( cc );
            interpolatedStringHandler.AppendLiteral( ".svg" );
            return WpfSvgRenderer.CreateImageSource( new Uri( interpolatedStringHandler.ToStringAndClear() ), ( Uri )null, new Size?(), ( WpfSvgPalette )null, ( string )null, new bool?(), true, true );
        }
    }


    /// <summary>Country id to country flag converter.</summary>
    public class CountryIdToFlagImageSourceConverter : IValueConverter
    {        
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            CountryCodes? cc = CountryIdToNameConverter.ConvertFromObject( value );
            
            if ( cc.HasValue )
            {
                return ( object )cc.Value.ToImageSource();
            }
                
            return ( object )null;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
