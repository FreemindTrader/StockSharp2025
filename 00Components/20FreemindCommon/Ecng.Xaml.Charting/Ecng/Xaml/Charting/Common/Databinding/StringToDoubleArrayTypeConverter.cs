// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Databinding.StringToDoubleArrayTypeConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Ecng.Xaml.Charting.Common.Databinding
{
    public class StringToDoubleArrayTypeConverter : TypeConverter
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
                throw new FormatException( string.Format( "Unable to convert the object type {0} into a double array. Please use a string with format '1.234, 5.678'", ( object ) value.GetType() ) );
            string str = (string) value;
            try
            {
                return ( object ) ( ( IEnumerable<string> ) str.Split( new char[ 3 ] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries ) ).Select<string, double>( ( Func<string, double> ) ( x => double.Parse( x, ( IFormatProvider ) culture ) ) ).ToArray<double>();
            }
            catch ( Exception ex )
            {
                throw new FormatException( "Unable to convert the string {0} into a double array. Please use the format '1.234,5.678'" );
            }
        }
    }
}
