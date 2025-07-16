// Decompiled with JetBrains decompiler
// Type: -.EnumToStrConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class EnumToStrConverter : IValueConverter
{

    public static readonly EnumToStrConverter _converter = new EnumToStrConverter();

    public object Convert( object _param1, Type _param2 = null, object _param3 = null, CultureInfo _param4 = null )
    {
        if ( _param1 != null )
        {
            FieldInfo field = _param1.GetType().GetField(_param1.ToString());
            bool flag = (string) _param3 == "1";
            if ( field != ( FieldInfo ) null )
            {
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
                return customAttributes.Length == 0 || !( !string.IsNullOrEmpty( customAttributes[ 0 ].Description ) | flag ) ? ( object ) _param1.ToString() : ( object ) customAttributes[ 0 ].Description;
            }
        }
        return ( object ) string.Empty;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new Exception( "Cant convert back" );
    }
}
