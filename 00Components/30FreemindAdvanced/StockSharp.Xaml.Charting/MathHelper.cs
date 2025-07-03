// Decompiled with JetBrains decompiler
// Type: #=zgZ2vtblQgV0wzuJ0wshoWkZiI6zajPlHhEQ36XDarPj3
using System;
using Ecng.Common;

namespace SciChart.Data.Numerics.GenericMath;

using System;
using System.Collections.Generic;

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
      (object) MathHelper._doubleMath
    },
    {
      typeof (DateTime),
      (object) MathHelper._dateTimeMath
    },
    {
      typeof (float),
      (object) MathHelper._floatMath
    },
    {
      typeof (int),
      (object) MathHelper._intMath
    },
    {
      typeof (long),
      (object) MathHelper._longMath
    },
    {
      typeof (Decimal),
      (object) MathHelper._decimalMath
    },
    {
      typeof (TimeSpan),
      (object) MathHelper._timespanMath
    },
    {
      typeof (short),
      (object) MathHelper._shortMath
    },
    {
      typeof (ushort),
      (object) MathHelper._ushortMath
    },
    {
      typeof (sbyte),
      (object) MathHelper._sbyteMath
    },
    {
      typeof (ulong),
      (object) MathHelper._ulongMath
    },
    {
      typeof (uint),
      (object) MathHelper._uintMath
    },
    {
      typeof (byte),
      (object) MathHelper._byteMath
    }
  };

  internal static IMath<T> GetMath<T>()
  {
    return MathHelper.TryGetMath<T>() ?? throw new NotSupportedException("" + typeof (T)?.ToString());
  }

  internal static IMath<T> TryGetMath<T>()
  {
    Type key = typeof (T);
    object obj;
    return MathHelper._map.TryGetValue(key, out obj) ? (IMath<T>) obj : (IMath<T>) null;
  }
}
