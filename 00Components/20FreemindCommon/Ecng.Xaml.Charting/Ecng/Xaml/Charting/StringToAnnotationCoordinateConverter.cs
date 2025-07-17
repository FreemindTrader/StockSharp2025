// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.StringToAnnotationCoordinateConverter
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace Ecng.Xaml.Charting
{
    internal class StringToAnnotationCoordinateConverter : TypeConverter
    {
        public override bool CanConvertFrom( ITypeDescriptorContext context, Type sourceType )
        {
            if ( sourceType == typeof( string ) )
                return true;
            return base.CanConvertFrom( context, sourceType );
        }

        public override object ConvertFrom( ITypeDescriptorContext context, CultureInfo culture, object value )
        {
            string s = (string) value;
            double result1;
            if ( double.TryParse( s, NumberStyles.Any, ( IFormatProvider ) CultureInfo.InvariantCulture, out result1 ) )
                return ( object ) result1;
            DateTime result2;
            if ( DateTime.TryParse( s, ( IFormatProvider ) CultureInfo.InvariantCulture, DateTimeStyles.None, out result2 ) )
                return ( object ) result2;
            return ( object ) null;
        }
    }
}
