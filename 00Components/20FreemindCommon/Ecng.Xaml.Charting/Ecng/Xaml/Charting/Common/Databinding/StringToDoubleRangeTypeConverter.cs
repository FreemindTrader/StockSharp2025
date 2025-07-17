// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Databinding.StringToDoubleRangeTypeConverter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace StockSharp.Xaml.Charting.Common.Databinding
{
    public class StringToDoubleRangeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
        {
            return sourceType == typeof( string );
        }

        public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
        {
            if ( value == null )
                return ( object ) null;
            if ( !this.CanConvertFrom( context, value.GetType() ) )
                throw new FormatException( string.Format( "Unable to convert the object type {0} into a DoubleRange. Please use a string with format '1.234, 5.678' or '1.234'", ( object ) value.GetType() ) );
            string s = (string) value;
            try
            {
                int length = s.IndexOf(',');
                if ( length != -1 )
                    return ( object ) new DoubleRange( double.Parse( s.Substring( 0, length ), ( IFormatProvider ) CultureInfo.InvariantCulture ), double.Parse( s.Substring( length + 1, s.Length - length - 1 ), ( IFormatProvider ) CultureInfo.InvariantCulture ) );
                double num = double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
                return ( object ) new DoubleRange( num, num );
            }
            catch ( Exception ex )
            {
                throw new FormatException( "Unable to convert the string {0} into a DoubleRange. Please use the format '1.234,5.678' or '1.234'" );
            }
        }
    }
}
