using System;
using System.ComponentModel;
using System.Globalization;
using SciChart.Data.Model;

namespace SciChart.Charting.Common.Databinding;

public sealed class StringToDoubleRangeTypeConverter :
  TypeConverter
{
    public override bool CanConvertFrom( ITypeDescriptorContext _param1, Type _param2 )
    {
        return _param2 == typeof( string );
    }

    public override object ConvertFrom( ITypeDescriptorContext _param1, CultureInfo _param2, object _param3 )
    {
        if ( _param3 == null )
            return ( object ) null;
        string s = this.CanConvertFrom(_param1, _param3.GetType()) ? (string) _param3 : throw new FormatException($"Unable to convert the object type {_param3.GetType()} into a DoubleRange. Please use a string with format '1.234, 5.678' or '1.234'");
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
