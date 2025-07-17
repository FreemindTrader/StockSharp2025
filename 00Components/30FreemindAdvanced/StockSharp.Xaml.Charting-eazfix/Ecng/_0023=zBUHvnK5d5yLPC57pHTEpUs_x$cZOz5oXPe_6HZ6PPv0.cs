// Decompiled with JetBrains decompiler
// Type: #=zBUHvnK5d5yLPC57pHTEpUs_x$cZOz5oXPe_6HZ6PPv0g0VAgCXbN$kvBDdEW
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzBUHvnK5d5yLPC57pHTEpUs_x\u0024cZOz5oXPe_6HZ6PPv0g0VAgCXbN\u0024kvBDdEW : 
  \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerRnFnKJZv3yJKLQRpJ
{
  private IImageByte \u0023\u003DzegTLEtYkn1Xk;
  private \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npicDwJ2IziWohYgJvXhRNocRw\u003D\u003D \u0023\u003Dzv8zN_0vlssXJ;
  private int m_start;
  private double \u0023\u003DzpIkTrpHGJNon;
  private RectangleInt \u0023\u003DzI89dEGml\u0024B4z;

  public \u0023\u003DzBUHvnK5d5yLPC57pHTEpUs_x\u0024cZOz5oXPe_6HZ6PPv0g0VAgCXbN\u0024kvBDdEW(
    IImageByte _param1,
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npicDwJ2IziWohYgJvXhRNocRw\u003D\u003D _param2)
  {
    this.\u0023\u003DzegTLEtYkn1Xk = _param1;
    this.\u0023\u003Dzv8zN_0vlssXJ = _param2;
    this.m_start = 0;
    this.\u0023\u003DzpIkTrpHGJNon = 1.0;
    this.\u0023\u003DzI89dEGml\u0024B4z = new RectangleInt(0, 0, 0, 0);
  }

  public void \u0023\u003DzotQWOIc\u003D(
    IImageByte _param1)
  {
    this.\u0023\u003DzegTLEtYkn1Xk = _param1;
  }

  public void \u0023\u003DzUlMKsq8\u003D(
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npicDwJ2IziWohYgJvXhRNocRw\u003D\u003D _param1)
  {
    this.\u0023\u003Dzv8zN_0vlssXJ = _param1;
  }

  public \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npicDwJ2IziWohYgJvXhRNocRw\u003D\u003D \u0023\u003DzUlMKsq8\u003D()
  {
    return this.\u0023\u003Dzv8zN_0vlssXJ;
  }

  public void \u0023\u003DzThyAY8tY1g4ajpiZ6g\u003D\u003D()
  {
  }

  public void \u0023\u003DzbRHAWK1yd7\u00242(
    double _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    this.\u0023\u003DzI89dEGml\u0024B4z.\u0023\u003DzP4R7yU0\u003D = \u0023\u003DzbZGwufOdFTewaG24h4AgEliT50xkV18wBgbq3ZSSVzJmxlUquvfLbDUE95zermueEDOZtJ4\u003D.\u0023\u003Dz2skJA3U\u003D(_param1);
    this.\u0023\u003DzI89dEGml\u0024B4z.\u0023\u003DzRNV_Dpk\u003D = \u0023\u003DzbZGwufOdFTewaG24h4AgEliT50xkV18wBgbq3ZSSVzJmxlUquvfLbDUE95zermueEDOZtJ4\u003D.\u0023\u003Dz2skJA3U\u003D(_param2);
    this.\u0023\u003DzI89dEGml\u0024B4z.\u0023\u003Dzp55dtus\u003D = \u0023\u003DzbZGwufOdFTewaG24h4AgEliT50xkV18wBgbq3ZSSVzJmxlUquvfLbDUE95zermueEDOZtJ4\u003D.\u0023\u003Dz2skJA3U\u003D(_param3);
    this.\u0023\u003DzI89dEGml\u0024B4z.\u0023\u003DzSzOWcj8\u003D = \u0023\u003DzbZGwufOdFTewaG24h4AgEliT50xkV18wBgbq3ZSSVzJmxlUquvfLbDUE95zermueEDOZtJ4\u003D.\u0023\u003Dz2skJA3U\u003D(_param4);
  }

  public void \u0023\u003DzXN_RwFRwc4qH(double _param1) => this.\u0023\u003DzpIkTrpHGJNon = _param1;

  public double \u0023\u003DzXN_RwFRwc4qH() => this.\u0023\u003DzpIkTrpHGJNon;

  public void \u0023\u003Dz1qhSEfFndRSy(double _param1)
  {
    this.m_start = agg_basics.\u0023\u003DzQ9DKAFLSaa9H(_param1 * 256.0);
  }

  public double \u0023\u003Dz1qhSEfFndRSy() => (double) this.m_start / 256.0;

  public int \u0023\u003DzqKKXccv\u0024wvZxrEpbtQ\u003D\u003D()
  {
    return this.\u0023\u003Dzv8zN_0vlssXJ.\u0023\u003Dz4aDJxoSLg9y0();
  }

  public int \u0023\u003DzRCZcxIYMEvaL()
  {
    return this.\u0023\u003Dzv8zN_0vlssXJ.\u0023\u003DzRCZcxIYMEvaL();
  }

  public double \u0023\u003DzCIN619c\u003D()
  {
    return (double) this.\u0023\u003DzqKKXccv\u0024wvZxrEpbtQ\u003D\u003D() / 256.0;
  }

  public void \u0023\u003Dz\u00246e75ZE\u003D(
    RGBA_Bytes[] _param1,
    int _param2,
    int _param3,
    int _param4)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dz_Cr7XQfsG3x4FPQiULaFBxE\u003D(
    int _param1,
    int _param2,
    uint _param3,
    RGBA_Bytes[] _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzCHZdvpXxHmsxsEnO0xInJE8\u003D(
    int _param1,
    int _param2,
    uint _param3,
    RGBA_Bytes[] _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public static bool \u0023\u003DzCQgIZG_ak02f9V0Lpx4M3Ro\u003D() => true;

  public override void \u0023\u003DzWw9HFRDBzgin(
    \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerRnFnKJZv3yJKLQRpJ.\u0023\u003DzBI80sk2_LzQE _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
  }

  public override void \u0023\u003DzftuZkWHjuqjuLgb_ZQ09YYE\u003D(
    \u0023\u003DzEp503ezAshtH55ArQ\u0024ydEuHQdGn\u0024BlVr_f8qOhYtPerRnFnKJZv3yJKLQRpJ.\u0023\u003DzBI80sk2_LzQE _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6,
    int _param7,
    int _param8)
  {
  }

  public override void \u0023\u003DzDEkPjjk\u003D(
    int _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5,
    int _param6)
  {
  }

  public override void \u0023\u003Dz_kyUZCg\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1)
  {
  }

  public override void \u0023\u003DzqjLLI7c\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    int _param2,
    int _param3)
  {
  }

  public override void \u0023\u003Dz9xp\u0024wRI\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    int _param2,
    int _param3)
  {
  }

  public void \u0023\u003DzZ5O1X2mMaDDQAHP9nA\u003D\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public override void \u0023\u003Dzo2ftlYE\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    int _param2,
    int _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }
}
