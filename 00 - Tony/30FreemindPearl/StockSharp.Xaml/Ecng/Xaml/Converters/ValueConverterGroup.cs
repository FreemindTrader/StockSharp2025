// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.ValueConverterGroup
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// </summary>
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        object IValueConverter.Convert( object o1, Type myType, object o2, CultureInfo cultInfo )
        {            
            return this.Aggregate<IValueConverter, object>( o1, ( o, c ) => c.Convert( o, myType, o2, cultInfo ) );
        }

        object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }        
    }
}
