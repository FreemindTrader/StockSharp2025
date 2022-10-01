// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Converters.BoolToDoubleConverter
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Boolean" /> to <see cref="T:System.Double" /> converter.
    ///     </summary>
    public class BoolToDoubleConverter : IValueConverter
    {
        
        private double _trueValue = 0.5;
        
        private double _falseValue = 1.0;

        /// <summary>
        /// <see langword="true" /> value.
        ///     </summary>
        public double TrueValue
        {
            get
            {
                return this._trueValue;
            }
            set
            {
                this._trueValue = value;
            }
        }

        /// <summary>
        /// <see langword="false" /> value.
        ///     </summary>
        public double FalseValue
        {
            get
            {
                return this._falseValue;
            }
            set
            {
                this._falseValue = value;
            }
        }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool paraResult = parameter == null || Converter.To<bool>( parameter ) ;
            return ( ( bool )value == paraResult ? this.TrueValue : this.FalseValue );
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4)
        {
            throw new NotSupportedException();
        }
    }
}
