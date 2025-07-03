// Decompiled with JetBrains decompiler
// Type: -.StringToDoubleRangeTypeConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class StringToDoubleRangeTypeConverter :
  TypeConverter
{
    public override bool CanConvertFrom( ITypeDescriptorContext _param1, Type _param2 )
    {
        return _param2 == typeof( string );
    }

    public override object ConvertFrom(
      ITypeDescriptorContext _param1,
      CultureInfo _param2,
      object _param3 )
    {
        if ( _param3 == null )
            return ( object ) null;
        if ( !this.CanConvertFrom( _param1, _param3.GetType() ) )
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(112 /*0x70*/, 1);
            interpolatedStringHandler.AppendLiteral( "" );
            interpolatedStringHandler.AppendFormatted<Type>( _param3.GetType() );
            interpolatedStringHandler.AppendLiteral( "" );
            throw new FormatException( interpolatedStringHandler.ToStringAndClear() );
        }
        string s = (string) _param3;
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
            throw new FormatException( "" );
        }
    }
}
