// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.XmlReaderExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace Ecng.Xaml.Charting.Common.Extensions
{
    internal static class XmlReaderExtensions
    {
        private static readonly FontFamily DefaultFontFamily = new TextBlock().FontFamily;
        private static readonly IDictionary<string, FontWeight> _fontWeights = (IDictionary<string, FontWeight>) new Dictionary<string, FontWeight>() { { FontWeights.Bold.ToString(), FontWeights.Bold }, { FontWeights.Black.ToString(), FontWeights.Black }, { FontWeights.ExtraBlack.ToString(), FontWeights.ExtraBlack }, { FontWeights.ExtraBold.ToString(), FontWeights.ExtraBold }, { FontWeights.ExtraLight.ToString(), FontWeights.ExtraLight }, { FontWeights.Light.ToString(), FontWeights.Light }, { FontWeights.Medium.ToString(), FontWeights.Medium }, { FontWeights.Normal.ToString(), FontWeights.Normal }, { FontWeights.SemiBold.ToString(), FontWeights.SemiBold }, { FontWeights.Thin.ToString(), FontWeights.Thin } };
        private static readonly IDictionary<string, FontStyle> _fontStyles = (IDictionary<string, FontStyle>) new Dictionary<string, FontStyle>() { { FontStyles.Italic.ToString(), FontStyles.Italic }, { FontStyles.Normal.ToString(), FontStyles.Normal } };

        public static void DeserilizeProperty( this XmlReader reader, object element, string propertyName )
        {
            PropertyInfo property = element.GetType().GetProperty(propertyName);
            object obj = reader.GetValue(propertyName, property.PropertyType);
            property.SetValue( element, obj, ( object[ ] ) null );
        }

        public static object GetValue( this XmlReader reader, string attributeName, Type valueType )
        {
            string index = reader[attributeName];
            object obj;
            if ( valueType.IsEnum )
                obj = index == null ? ( object ) null : Enum.Parse( valueType, index, false );
            else if ( valueType == typeof( Brush ) )
            {
                if ( index == null )
                    return ( object ) null;
                obj = ( object ) new SolidColorBrush( XmlReaderExtensions.GetColorFromString( index ) );
            }
            else if ( valueType == typeof( Color ) )
            {
                if ( index == null )
                    return ( object ) Color.FromArgb( ( byte ) 0, ( byte ) 0, ( byte ) 0, ( byte ) 0 );
                obj = ( object ) XmlReaderExtensions.GetColorFromString( index );
            }
            else if ( valueType == typeof( Thickness ) )
            {
                if ( index == null )
                    return ( object ) new Thickness( 0.0, 0.0, 0.0, 0.0 );
                obj = ( object ) XmlReaderExtensions.GetThicknessFromString( index );
            }
            else if ( valueType == typeof( FontFamily ) )
            {
                try
                {
                    obj = string.IsNullOrEmpty( index ) ? ( object ) XmlReaderExtensions.DefaultFontFamily : ( object ) new FontFamily( index );
                }
                catch
                {
                    obj = ( object ) XmlReaderExtensions.DefaultFontFamily;
                }
            }
            else if ( valueType == typeof( FontWeight ) )
                obj = index == null || !XmlReaderExtensions._fontWeights.ContainsKey( index ) ? ( object ) FontWeights.Normal : ( object ) XmlReaderExtensions._fontWeights[ index ];
            else if ( valueType == typeof( FontStyle ) )
                obj = index == null || !XmlReaderExtensions._fontStyles.ContainsKey( index ) ? ( object ) FontStyles.Normal : ( object ) XmlReaderExtensions._fontStyles[ index ];
            else if ( typeof( IRange ).IsAssignableFrom( valueType ) )
            {
                if ( index == null )
                    return ( object ) null;
                string[] strArray = index.Split(',');
                obj = ( object ) RangeFactory.NewRange( Type.GetType( strArray[ 0 ] ), ( IComparable ) Convert.ToDouble( strArray[ 1 ] ), ( IComparable ) Convert.ToDouble( strArray[ 2 ] ) );
            }
            else if ( valueType == typeof( TimeSpan ) )
            {
                if ( index == null )
                    return ( object ) TimeSpan.Zero;
                obj = ( object ) TimeSpan.Parse( index );
            }
            else
            {
                if ( index == null )
                    return ( object ) null;
                Type type = Nullable.GetUnderlyingType(valueType);
                if ( ( object ) type == null )
                    type = valueType;
                Type conversionType = type;
                obj = Convert.ChangeType( ( object ) index, conversionType, ( IFormatProvider ) CultureInfo.CurrentCulture );
            }
            return obj;
        }

        private static Color GetColorFromString( string value )
        {
            string[] strArray = value.Split(',');
            return Color.FromArgb( Convert.ToByte( strArray[ 0 ], 16 ), Convert.ToByte( strArray[ 1 ], 16 ), Convert.ToByte( strArray[ 2 ], 16 ), Convert.ToByte( strArray[ 3 ], 16 ) );
        }

        private static Thickness GetThicknessFromString( string value )
        {
            string[] strArray = value.Split(',');
            return new Thickness( Convert.ToDouble( strArray[ 0 ] ), Convert.ToDouble( strArray[ 1 ] ), Convert.ToDouble( strArray[ 2 ] ), Convert.ToDouble( strArray[ 3 ] ) );
        }
    }
}
