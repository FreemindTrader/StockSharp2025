// Decompiled with JetBrains decompiler
// Type: #=zzsyKnUNUDKjF7rDv70izNy4bx$saGFs108aHH7lGphGuZX7JpGJEopE=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public static class DataDistributionCalculatorFactory
{
  public static IDataDistributionCalculator<TX> \u0023\u003DzpxJeWbQ\u003D<TX>(
    bool _param0)
    where TX : IComparable
  {
    if (typeof (TX) == typeof (float))
      return !_param0 ? new ListSingleDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new SingleDataDistributionCalculator();
    if (typeof (TX) == typeof (double))
      return !_param0 ? new ListDoubleDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new DoubleDataDistributionCalculator();
    if (typeof (TX) == typeof (Decimal))
      return !_param0 ? new ListDecimalDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new DecimalDataDistributionCalculator();
    if (typeof (TX) == typeof (short))
      return !_param0 ? new ListInt16DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new Int16DataDistributionCalculator();
    if (typeof (TX) == typeof (int))
      return !_param0 ? new ListInt32DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new Int32DataDistributionCalculator();
    if (typeof (TX) == typeof (long))
      return !_param0 ? new ListInt64DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new Int64DataDistributionCalculator();
    if (typeof (TX) == typeof (ushort))
      return !_param0 ? new ListUInt16DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new UInt16DataDistributionCalculator();
    if (typeof (TX) == typeof (uint))
      return !_param0 ? new ListUInt32DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new UInt32DataDistributionCalculator();
    if (typeof (TX) == typeof (ulong))
      return !_param0 ? new ListUInt64DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new UInt64DataDistributionCalculator();
    if (typeof (TX) == typeof (byte))
      return !_param0 ? new ListByteDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new ByteDataDistributionCalculator();
    if (typeof (TX) == typeof (sbyte))
      return !_param0 ? new ListSByteDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new SByteDataDistributionCalculator();
    if (typeof (TX) == typeof (DateTime))
      return !_param0 ? new ListDateTimeDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new DateTimeDataDistributionCalculator();
    if (!(typeof (TX) == typeof (TimeSpan)))
      throw new NotImplementedException($"Cannot create a DataDistributionCalculator for the type TX={typeof (TX)}");
    return !_param0 ? new ListTimeSpanDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new TimeSpanDataDistributionCalculator();
  }
}
