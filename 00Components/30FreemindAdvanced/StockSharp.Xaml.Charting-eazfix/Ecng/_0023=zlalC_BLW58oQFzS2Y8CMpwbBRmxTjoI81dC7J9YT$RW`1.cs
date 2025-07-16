// Decompiled with JetBrains decompiler
// Type: #=zlalC_BLW58oQFzS2Y8CMpwbBRmxTjoI81dC7J9YT$RWJeZXysfONBiA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
public abstract class \u0023\u003DzlalC_BLW58oQFzS2Y8CMpwbBRmxTjoI81dC7J9YT\u0024RWJeZXysfONBiA\u003D<TX> : 
  \u0023\u003DzbZGwufOdFTewaG24h4AgEt0e4y2LR89MbvhnTaKf4YV_es8hzZJVk08\u003D<TX>
  where TX : IComparable
{
  private bool \u0023\u003DzK\u00241TDFxphrRsPn8ch6Qgvpw\u003D;
  private bool \u0023\u003Dz1cZLPq2J2qvlWoRmQSiW0nbao8YakgXF\u0024A\u003D\u003D;

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003Dzu6iQ2MOE9IYpXL_QIQ\u003D\u003D()
  {
    return this.\u0023\u003DzK\u00241TDFxphrRsPn8ch6Qgvpw\u003D;
  }

  protected void \u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(bool _param1)
  {
    this.\u0023\u003DzK\u00241TDFxphrRsPn8ch6Qgvpw\u003D = _param1;
  }

  [CompilerGenerated]
  [SpecialName]
  public bool \u0023\u003Dzto0ucxxNtpN_zkiSeV1usQd_D\u0024yl()
  {
    return this.\u0023\u003Dz1cZLPq2J2qvlWoRmQSiW0nbao8YakgXF\u0024A\u003D\u003D;
  }

  protected void \u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(bool _param1)
  {
    this.\u0023\u003Dz1cZLPq2J2qvlWoRmQSiW0nbao8YakgXF\u0024A\u003D\u003D = _param1;
  }

  public void \u0023\u003DzYxK_ApJhHiJi9CS2B8NjXKM\u003D()
  {
    this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(false);
  }

  public abstract void \u0023\u003DzFIf7JZ5S\u0024Wr_(
    ISeriesColumn<TX> _param1,
    TX _param2,
    bool _param3);

  public abstract void \u0023\u003DzeU6gWqHRfREz(
    ISeriesColumn<TX> _param1,
    int _param2,
    IEnumerable<TX> _param3,
    bool _param4);

  public abstract void \u0023\u003DzPY2yStN8KbO\u0024(
    ISeriesColumn<TX> _param1,
    int _param2,
    int _param3,
    IEnumerable<TX> _param4,
    bool _param5);

  public abstract void \u0023\u003Dzs9WSchJIpnF0(
    ISeriesColumn<TX> _param1,
    int _param2,
    TX _param3,
    bool _param4);

  public virtual void Clear()
  {
    this.\u0023\u003DzBD_etwoAJ6Nw1j21ug\u003D\u003D(true);
    this.\u0023\u003Dz6HFZWED70KA3OkmAw\u0024SltknkBzo4(true);
  }
}
