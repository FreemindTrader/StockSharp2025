// Decompiled with JetBrains decompiler
// Type: #=z9jHRW$4hcTcRirEhLafLfKwkzeHFx2BtVDw8LCsrGTu1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;

#nullable disable
internal sealed class \u0023\u003Dz9jHRW\u00244hcTcRirEhLafLfKwkzeHFx2BtVDw8LCsrGTu1 : 
  \u0023\u003DziARJyOecclYiJO5UbZqQJ8tTDgXi4mHI9\u0024dB3n962FPd
{
  public override string \u0023\u003Dz\u0024WinkXTLMGVP(IComparable _param1, bool _param2)
  {
    DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
    return !this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting().\u0023\u003DzCCMM80zDpO6N<char>() ? dateTime.ToString(this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting()) : this.\u0023\u003DzkqN2vZ4\u003D(_param1);
  }

  public override string \u0023\u003DzkqN2vZ4\u003D(IComparable _param1)
  {
    if (!(this.\u0023\u003DzHZDgUSdfqmkx() is dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd y8VrcqY29Qf62Ejd))
      throw new InvalidOperationException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539336757));
    DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
    return (this.\u0023\u003DzHZDgUSdfqmkx().VisibleRange as \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D).Diff.Ticks > TimeSpan.FromDays(1.0).Ticks ? dateTime.ToString(y8VrcqY29Qf62Ejd.TextFormatting) : dateTime.ToString(y8VrcqY29Qf62Ejd.SubDayTextFormatting);
  }
}
