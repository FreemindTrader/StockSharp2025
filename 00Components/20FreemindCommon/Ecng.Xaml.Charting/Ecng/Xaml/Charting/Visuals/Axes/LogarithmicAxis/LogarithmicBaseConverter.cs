// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicAxis.LogarithmicBaseConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace StockSharp.Xaml.Charting.Visuals.Axes.LogarithmicAxis
{
    public class LogarithmicBaseConverter : TypeConverter
    {
        public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
        {
            if ( value == null || !this.CanConvertFrom( context, value.GetType() ) )
                throw new FormatException( string.Format( "Unable to convert the object type {0} into a double. Please use a string with format '2, 5.6' or 'E, e'", ( object ) value?.GetType() ) );
            string s = ((string) value).Trim();
            double num;
            try
            {
                num = s.Length != 1 || !( s.ToUpperInvariant() == "E" ) ? double.Parse( s, ( IFormatProvider ) CultureInfo.InvariantCulture ) : Math.E;
            }
            catch ( Exception ex )
            {
                throw new FormatException( "Unable to convert the string {0} into a double. Please use the format '2, 5.6' or 'E, e'" );
            }
            return ( object ) num;
        }
    }
}
