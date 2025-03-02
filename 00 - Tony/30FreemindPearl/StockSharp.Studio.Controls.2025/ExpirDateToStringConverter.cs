// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ExpirDateToStringConverter
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Studio.Controls
{
    internal sealed class ExpirDateToStringConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo _ )
        {
            if ( value is DateTime )
                return  ( ( DateTime ) value ).ToString( "D" );
            if ( value == null )
                return  null;
            return  value.ToString();
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
