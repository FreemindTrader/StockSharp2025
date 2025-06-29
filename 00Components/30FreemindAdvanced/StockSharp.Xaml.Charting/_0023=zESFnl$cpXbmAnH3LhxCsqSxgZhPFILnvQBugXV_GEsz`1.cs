// Decompiled with JetBrains decompiler
// Type: #=zESFnl$cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
internal sealed class \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D<\u0023\u003DzH9HNkng\u003D> : 
  \u0023\u003Dzboj3ckhISv7k6koCkTeIfzSujzHmXzYCLKUgdFUczis\u0024<\u0023\u003DzH9HNkng\u003D>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly \u0023\u003DzH9HNkng\u003D[] \u0023\u003Dzy04c1jB0PxOy;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly int \u0023\u003DzfwiqU6ROP8\u0024k;

  public \u0023\u003DzESFnl\u0024cpXbmAnH3LhxCsqSxgZhPFILnvQBugXV_GEszyTZ24Jw\u003D\u003D(
    int _param1)
  {
    this.\u0023\u003DzfwiqU6ROP8\u0024k = _param1;
    this.\u0023\u003DzkELV\u0024GPsC83d = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003DzH9HNkng\u003D>) new \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<\u0023\u003DzH9HNkng\u003D>(_param1);
    this.\u0023\u003Dzy04c1jB0PxOy = new \u0023\u003DzH9HNkng\u003D[_param1];
  }

  public override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz1bvQV4SZTWpA(
    int _param1,
    int _param2)
  {
    int num1 = _param1 > this.Count ? this.Count : _param1;
    int num2 = Math.Min(this.Count - num1, _param2);
    if (this.\u0023\u003DzkELV\u0024GPsC83d.Count != this.\u0023\u003DzfwiqU6ROP8\u0024k)
      return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzRr4AYdnHaTxa(), num1, num2);
    ((\u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<\u0023\u003DzH9HNkng\u003D>) this.\u0023\u003DzkELV\u0024GPsC83d).\u0023\u003Dz3MbNd8U\u003D(num1, this.\u0023\u003Dzy04c1jB0PxOy, 0, num2);
    return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzy04c1jB0PxOy, 0, num2);
  }

  public \u0023\u003DzH9HNkng\u003D[] \u0023\u003DzSWlLd4k\u003D()
  {
    if (this.\u0023\u003DzkELV\u0024GPsC83d.Count != this.\u0023\u003DzfwiqU6ROP8\u0024k)
      return this.\u0023\u003DzkELV\u0024GPsC83d.ToArray<\u0023\u003DzH9HNkng\u003D>();
    this.\u0023\u003DzkELV\u0024GPsC83d.CopyTo(this.\u0023\u003Dzy04c1jB0PxOy, 0);
    return this.\u0023\u003Dzy04c1jB0PxOy;
  }

  public IList<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz3TQv53iUYmxB()
  {
    return (IList<\u0023\u003DzH9HNkng\u003D>) this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzRr4AYdnHaTxa();
  }
}
