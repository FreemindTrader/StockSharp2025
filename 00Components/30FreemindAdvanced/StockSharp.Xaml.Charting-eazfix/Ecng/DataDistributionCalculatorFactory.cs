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
      return !_param0 ? new ListSingleDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzmAi_JN5raoSBYo9w2IEI_46HvYdrS0_N1sghuF8KKoiKKlsnxM71kOs\u003D();
    if (typeof (TX) == typeof (double))
      return !_param0 ? new ListDoubleDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQCyGjkAJyy4irf_OdWY0KJDShD8WAuCBHCc\u003D();
    if (typeof (TX) == typeof (Decimal))
      return !_param0 ? new ListDecimalDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQKZ1LPrTImbhyQpZVIBHyFO93MOJa7yd6Us\u003D();
    if (typeof (TX) == typeof (short))
      return !_param0 ? new ListInt16DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzlbJvQa3SY_TV_FXt6bD53MehTjK_yk8w4RkFx2pgB1nCOK9Da0gcg\u0024vS3imZ();
    if (typeof (TX) == typeof (int))
      return !_param0 ? new ListInt32DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz3RRntx4pzkd854dIVpLK6UX0gcjkYt7_xp1WV1SkF7QpNXuNEDpMN\u0024YFVE0a();
    if (typeof (TX) == typeof (long))
      return !_param0 ? new ListInt64DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzjFV3E4nzZ192p80vvoilf01\u00240m_EV2bj12j\u0024QHfwnOmKzQfQyAcZOP3wWOZX();
    if (typeof (TX) == typeof (ushort))
      return !_param0 ? new ListUInt16DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzmAi_JN5raoSBYo9w2IEI_xK5zI\u0024VZ2FtYGm_M5ABFKKcnJPAqPUFruryn3W6();
    if (typeof (TX) == typeof (uint))
      return !_param0 ? new ListUInt32DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz5B3gvTTfbmLYjDPhZPGfZNRhEakCIllfnVwCSJY7Vh3JItzu4yI9shzGZZsa();
    if (typeof (TX) == typeof (ulong))
      return !_param0 ? new ListUInt64DataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzgZ2vtblQgV0wzuJ0wshoWvNnOzvgum5sE0GpcWqsQQX_Al4pCDXe1X3P8\u0024Hw();
    if (typeof (TX) == typeof (byte))
      return !_param0 ? new ListByteDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz5hVyTN88kBn45NAfOxK7MOzNKQH1uxVq4qvGxsln3hTzUcBgLSDbT3Q\u003D();
    if (typeof (TX) == typeof (sbyte))
      return !_param0 ? new ListSByteDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzKurtFt9p_vSeW\u0024_A_uZkzrpbHRcB0FcJmW38xyGraP4Jkq6kbaLSZKM\u003D();
    if (typeof (TX) == typeof (DateTime))
      return !_param0 ? new ListDateTimeDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003Dz6\u0024kyD91Y546oWffDBUVKqEnz1Hw680\u0024YYho\u0024DwBwY351FMWEn9EnILc\u003D();
    if (!(typeof (TX) == typeof (TimeSpan)))
      throw new NotImplementedException($"Cannot create a DataDistributionCalculator for the type TX={typeof (TX)}");
    return !_param0 ? new ListTimeSpanDataDistributionCalculator() as IDataDistributionCalculator<TX> : (IDataDistributionCalculator<TX>) new \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTG3Gc1WKM1RtF9zNZpf1PDqDexfOjWJ4pBM\u003D();
  }
}
