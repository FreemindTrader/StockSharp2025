using System;
using System.Collections.Generic;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;
#nullable disable
internal static class MathHelper
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
       MathHelper._doubleMath
    },
    {
      typeof (DateTime),
       MathHelper._dateTimeMath
    },
    {
      typeof (float),
       MathHelper._floatMath
    },
    {
      typeof (int),
       MathHelper._intMath
    },
    {
      typeof (long),
       MathHelper._longMath
    },
    {
      typeof (Decimal),
       MathHelper._decimalMath
    },
    {
      typeof (TimeSpan),
       MathHelper._timespanMath
    },
    {
      typeof (short),
       MathHelper._shortMath
    },
    {
      typeof (ushort),
       MathHelper._ushortMath
    },
    {
      typeof (sbyte),
       MathHelper._sbyteMath
    },
    {
      typeof (ulong),
       MathHelper._ulongMath
    },
    {
      typeof (uint),
       MathHelper._uintMath
    },
    {
      typeof (byte),
       MathHelper._byteMath
    }
  };

    internal static IMath<T> GetMath<T>()
    {
        return MathHelper.TryGetMath<T>() ?? throw new NotSupportedException( "GenericMath does not support Type " + typeof( T )?.ToString() );
    }

    internal static IMath<T> TryGetMath<T>()
    {
        Type key = typeof (T);
        object obj;
        return MathHelper._map.TryGetValue( key, out obj ) ? ( IMath<T> ) obj : ( IMath<T> ) null;
    }
}
