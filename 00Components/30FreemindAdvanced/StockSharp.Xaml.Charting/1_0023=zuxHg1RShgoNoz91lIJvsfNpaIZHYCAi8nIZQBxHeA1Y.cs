// Decompiled with JetBrains decompiler
// Type: #=zuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1YPjtvfCM4Cf$SW20w4MlG5kJm$WkUzOQfp
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

#nullable disable
internal abstract class \u0023\u003DzuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1YPjtvfCM4Cf\u0024SW20w4MlG5kJm\u0024WkUzOQfp : 
  \u0023\u003DzumuFC1VOOoBDs2QpPto5VkbLjIthccEiDcgR\u0024vJQ7K_Hrp4E7vWJo36zKSrEqSwNKA\u003D\u003D
{
  private int \u0023\u003DzXbx4eadkEUpa;
  private int \u0023\u003DzCNTzN\u0024tjwepc;
  private int \u0023\u003Dz\u0024O5I0BM18Lf3;

  public \u0023\u003DzuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1YPjtvfCM4Cf\u0024SW20w4MlG5kJm\u0024WkUzOQfp(
    \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0LKrMWRP8HpfFNPgYY55tLjfw\u003D\u003D _param1,
    \u0023\u003DzkAKUJrbqM7JEiA1NxV8i_VXA\u0024pdrNBRk9sVTmSjnfr_47jyZuQRTCMKIqVTsNBVwaPINZNveauDT _param2,
    \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeTxLdSjL9CR9M_QGcrFkI\u0024EwQ\u003D\u003D _param3)
    : base(_param1, _param2, _param3)
  {
    this.\u0023\u003DzXbx4eadkEUpa = 20;
    this.\u0023\u003DzCNTzN\u0024tjwepc = 256 /*0x0100*/;
    this.\u0023\u003Dz\u0024O5I0BM18Lf3 = 256 /*0x0100*/;
  }

  private int \u0023\u003Dzod7XSeyO13Eq() => this.\u0023\u003DzXbx4eadkEUpa;

  private void \u0023\u003Dzod7XSeyO13Eq(int _param1) => this.\u0023\u003DzXbx4eadkEUpa = _param1;

  private double \u0023\u003DzCD_pIKWSINi6()
  {
    return (double) this.\u0023\u003DzCNTzN\u0024tjwepc / 256.0;
  }

  private double \u0023\u003DzZMnScZr_2vL_()
  {
    return (double) this.\u0023\u003Dz\u0024O5I0BM18Lf3 / 256.0;
  }

  private void \u0023\u003DzCD_pIKWSINi6(double _param1)
  {
    this.\u0023\u003DzCNTzN\u0024tjwepc = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(_param1 * 256.0);
  }

  private void \u0023\u003DzZMnScZr_2vL_(double _param1)
  {
    this.\u0023\u003Dz\u0024O5I0BM18Lf3 = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(_param1 * 256.0);
  }

  public void \u0023\u003DzPnqJtjI\u003D(double _param1)
  {
    this.\u0023\u003DzCNTzN\u0024tjwepc = this.\u0023\u003Dz\u0024O5I0BM18Lf3 = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzROReRE0C5MV7(_param1 * 256.0);
  }

  protected void \u0023\u003Dzkol1j7VmSj1\u0024(ref int _param1, ref int _param2)
  {
    if (_param1 < 256 /*0x0100*/)
      _param1 = 256 /*0x0100*/;
    if (_param2 < 256 /*0x0100*/)
      _param2 = 256 /*0x0100*/;
    if (_param1 > 256 /*0x0100*/ * this.\u0023\u003DzXbx4eadkEUpa)
      _param1 = 256 /*0x0100*/ * this.\u0023\u003DzXbx4eadkEUpa;
    if (_param2 > 256 /*0x0100*/ * this.\u0023\u003DzXbx4eadkEUpa)
      _param2 = 256 /*0x0100*/ * this.\u0023\u003DzXbx4eadkEUpa;
    _param1 = _param1 * this.\u0023\u003DzCNTzN\u0024tjwepc >> 8;
    _param2 = _param2 * this.\u0023\u003Dz\u0024O5I0BM18Lf3 >> 8;
    if (_param1 < 256 /*0x0100*/)
      _param1 = 256 /*0x0100*/;
    if (_param2 >= 256 /*0x0100*/)
      return;
    _param2 = 256 /*0x0100*/;
  }
}
