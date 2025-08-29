using System;
using System.Collections.Generic;
using Ecng.Common;
using SciChart.Data.Numerics.GenericMath;

namespace StockSharp.Xaml.Charting;
#nullable disable
public static class GenericMathFactory
{
    private static readonly FloatMath _floatMath = new FloatMath();
    private static readonly DoubleMath _doubleMath = new DoubleMath();
    private static readonly IntMath _intMath = new IntMath();
    private static readonly LongMath _longMath = new LongMath();
    private static readonly DateTimeMath _dateTimeMath = new DateTimeMath();
    private static readonly TimespanMath _timespanMath = new TimespanMath();
    private static readonly DecimalMath _decimalMath = new DecimalMath();
    private static readonly UShortMath _ushortMath = new UShortMath();
    private static readonly ShortMath _shortMath = new ShortMath();
    private static readonly UIntMath _uintMath = new UIntMath();
    private static readonly ULongMath _ulongMath = new ULongMath();
    private static readonly SByteMath _sbyteMath = new SByteMath();
    private static readonly ByteMath _byteMath = new ByteMath();

    private static readonly IDictionary<Type, object> _map = (IDictionary<Type, object>) new Dictionary<Type, object>()
  {
    {
      typeof (double),
       GenericMathFactory._doubleMath
    },
    {
      typeof (DateTime),
       GenericMathFactory._dateTimeMath
    },
    {
      typeof (float),
       GenericMathFactory._floatMath
    },
    {
      typeof (int),
       GenericMathFactory._intMath
    },
    {
      typeof (long),
       GenericMathFactory._longMath
    },
    {
      typeof (Decimal),
       GenericMathFactory._decimalMath
    },
    {
      typeof (TimeSpan),
       GenericMathFactory._timespanMath
    },
    {
      typeof (short),
       GenericMathFactory._shortMath
    },
    {
      typeof (ushort),
       GenericMathFactory._ushortMath
    },
    {
      typeof (sbyte),
       GenericMathFactory._sbyteMath
    },
    {
      typeof (ulong),
       GenericMathFactory._ulongMath
    },
    {
      typeof (uint),
       GenericMathFactory._uintMath
    },
    {
      typeof (byte),
       GenericMathFactory._byteMath
    }
  };

    public static IMath<T> GetMath<T>()
    {
        return GenericMathFactory.TryGetMath<T>() ?? throw new NotSupportedException( "GenericMath does not support Type " + typeof( T )?.ToString() );
    }

    public static IMath<T> TryGetMath<T>()
    {
        Type key = typeof (T);
        object obj;
        return GenericMathFactory._map.TryGetValue( key, out obj ) ? ( IMath<T> ) obj : ( IMath<T> ) null;
    }
}
