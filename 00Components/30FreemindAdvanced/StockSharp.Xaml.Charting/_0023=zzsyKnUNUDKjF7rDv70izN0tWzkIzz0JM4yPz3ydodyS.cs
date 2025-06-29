// Decompiled with JetBrains decompiler
// Type: #=zzsyKnUNUDKjF7rDv70izN0tWzkIzz0JM4yPz3ydodySc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003DzzsyKnUNUDKjF7rDv70izN0tWzkIzz0JM4yPz3ydodySc : 
  \u0023\u003DziARJyOecclYiJO5UbZqQJ8tTDgXi4mHI9\u0024dB3n962FPd
{
  public override string \u0023\u003Dz\u0024WinkXTLMGVP(IComparable _param1, bool _param2)
  {
    if (!(this.\u0023\u003DzHZDgUSdfqmkx() is dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd e72N8R2PvrlrqaEjd))
      throw new InvalidOperationException(XXX.SSS(-539337800));
    return this.\u0023\u003DzxwlUcz8\u003D((TimeSpan) _param1, e72N8R2PvrlrqaEjd.CursorTextFormatting);
  }

  public override string \u0023\u003DzkqN2vZ4\u003D(IComparable _param1)
  {
    if (!(this.\u0023\u003DzHZDgUSdfqmkx() is dje_zUWZK4BU846MEUQCR5WKT9QZA85UZEQW6M2YZEKBE72N8R2PVRLRQA_ejd e72N8R2PvrlrqaEjd))
      throw new InvalidOperationException(XXX.SSS(-539337800));
    return this.\u0023\u003DzxwlUcz8\u003D((TimeSpan) _param1, e72N8R2PvrlrqaEjd.TextFormatting);
  }

  private string \u0023\u003DzxwlUcz8\u003D(TimeSpan _param1, string _param2)
  {
    bool flag = _param2.Contains(XXX.SSS(-539328751));
    int num = _param1 < TimeSpan.Zero & flag ? 1 : 0;
    string format = flag ? _param2.TrimStart('-') : _param2;
    string str = _param1.\u0023\u003Dzto51K8pl8UAh().ToString(format);
    return num == 0 ? str : XXX.SSS(-539328751) + str;
  }
}
