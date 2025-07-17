// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Databinding.EnumToStringConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Ecng.Xaml.Charting
{
    public class EnumToStringConverter : IValueConverter
    {
        public static readonly EnumToStringConverter Instance = new EnumToStringConverter();

        public object Convert( object value, Type targetType = null, object parameter = null, CultureInfo culture = null )
        {
            if ( value != null )
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                bool flag = (string) parameter == "1";
                if ( field != ( FieldInfo ) null )
                {
                    DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
                    if ( customAttributes.Length == 0 || !( !string.IsNullOrEmpty( customAttributes[ 0 ].Description ) | flag ) )
                        return ( object ) value.ToString();
                    return ( object ) customAttributes[ 0 ].Description;
                }
            }
            return ( object ) string.Empty;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new Exception( "Cant convert back" );
        }
    }
}
