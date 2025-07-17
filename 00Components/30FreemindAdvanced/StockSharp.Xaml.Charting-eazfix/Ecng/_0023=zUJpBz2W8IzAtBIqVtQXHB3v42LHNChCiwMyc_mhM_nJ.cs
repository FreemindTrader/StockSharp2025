// Decompiled with JetBrains decompiler
// Type: #=zUJpBz2W8IzAtBIqVtQXHB3v42LHNChCiwMyc_mhM_nJgX2NQAB9o1T4SlQbf8OpLyJGvcS0=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB3v42LHNChCiwMyc_mhM_nJgX2NQAB9o1T4SlQbf8OpLyJGvcS0\u003D : 
  IScanlineCache
{
  private int \u0023\u003Dz_TCtD0vLATv8;
  private int \u0023\u003DzFfSb8y0\u003D;
  private ArrayPOD<ScanlineSpan> \u0023\u003DzDfv8Db0RWqZH;
  private int \u0023\u003DzJZMDU99kHHNc;
  private int \u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D;

  public \u0023\u003DzUJpBz2W8IzAtBIqVtQXHB3v42LHNChCiwMyc_mhM_nJgX2NQAB9o1T4SlQbf8OpLyJGvcS0\u003D()
  {
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003DzDfv8Db0RWqZH = new ArrayPOD<ScanlineSpan>(1000);
    this.\u0023\u003DzJZMDU99kHHNc = 0;
  }

  public ScanlineSpan \u0023\u003DzYkrxbPUh8YlkHw5nkgeSuH8\u003D()
  {
    ++this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D;
    return this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D - 1];
  }

  public void \u0023\u003Dzp_DWHgc\u003D(int _param1, int _param2)
  {
    int num = _param2 - _param1 + 3;
    if (num > this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzdTxNrgQ\u003D())
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003Dz7FKHKl8\u003D(num);
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003DzJZMDU99kHHNc = 0;
  }

  public void \u0023\u003Dzzb9PhvYi_sP8(int _param1, int _param2)
  {
    if (_param1 == this.\u0023\u003Dz_TCtD0vLATv8 + 1)
    {
      ++this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D;
    }
    else
    {
      ++this.\u0023\u003DzJZMDU99kHHNc;
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzwP120vA\u003D = _param1;
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = 1;
    }
    this.\u0023\u003Dz_TCtD0vLATv8 = _param1;
  }

  public void \u0023\u003DzqJOc77USXqlC(int _param1, int _param2, int _param3)
  {
    if (_param1 == this.\u0023\u003Dz_TCtD0vLATv8 + 1)
    {
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D += _param2;
    }
    else
    {
      ++this.\u0023\u003DzJZMDU99kHHNc;
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003DzwP120vA\u003D = _param1;
      this.\u0023\u003DzDfv8Db0RWqZH.\u0023\u003DzvsnCYl4\u003D()[this.\u0023\u003DzJZMDU99kHHNc].\u0023\u003Dzeids9mY\u003D = _param2;
    }
    this.\u0023\u003Dz_TCtD0vLATv8 = _param1 + _param2 - 1;
  }

  public void \u0023\u003DzDoZMoNygQSg8PGaYqw\u003D\u003D(int _param1)
  {
    this.\u0023\u003DzFfSb8y0\u003D = _param1;
  }

  public void \u0023\u003Dz_qg8im_\u0024N_mX()
  {
    this.\u0023\u003Dz_TCtD0vLATv8 = 2147483632;
    this.\u0023\u003DzJZMDU99kHHNc = 0;
  }

  public int \u0023\u003Dzi8jDI4I\u003D() => this.\u0023\u003DzFfSb8y0\u003D;

  public int \u0023\u003DzAK83aWWCVpNB() => this.\u0023\u003DzJZMDU99kHHNc;

  public ScanlineSpan \u0023\u003DzoLjFgpI\u003D()
  {
    this.\u0023\u003DzlYYZvacF9UC9Kl5xTSuYyyE\u003D = 1;
    return this.\u0023\u003DzYkrxbPUh8YlkHw5nkgeSuH8\u003D();
  }

  public byte[] \u0023\u003Dz9PP6ydFYlNHt() => (byte[]) null;
}
