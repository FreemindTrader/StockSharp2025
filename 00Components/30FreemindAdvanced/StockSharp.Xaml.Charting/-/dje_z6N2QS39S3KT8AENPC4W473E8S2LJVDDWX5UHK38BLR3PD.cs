// Decompiled with JetBrains decompiler
// Type: -.dje_z6N2QS39S3KT8AENPC4W473E8S2LJVDDWX5UHK38BLR3PDJQ_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Xaml.Charting;

internal sealed class dje_z6N2QS39S3KT8AENPC4W473E8S2LJVDDWX5UHK38BLR3PDJQ_ejd : IValueConverter
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Visibility \u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;

  public dje_z6N2QS39S3KT8AENPC4W473E8S2LJVDDWX5UHK38BLR3PDJQ_ejd()
  {
    this.\u0023\u003DzgUA4lBZwhI1n(Visibility.Collapsed);
  }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public Visibility dje_zHG9WX3XQJ2NF2B2_ejd
  {
    get => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;
    set => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D = value;
  }

  public Visibility \u0023\u003DzYTPqj7MFUvQf() => this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D;

  public void \u0023\u003DzgUA4lBZwhI1n(Visibility _param1)
  {
    this.\u0023\u003Dzuuu0n2mobAynoPMTh6dB4JY\u003D = _param1;
  }

  public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    int num = string.Equals(_param3 as string, XXX.SSS(-539430952), StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
    Visibility visibility1 = num != 0 ? this.\u0023\u003DzYTPqj7MFUvQf() : Visibility.Visible;
    Visibility visibility2 = num != 0 ? Visibility.Visible : this.\u0023\u003DzYTPqj7MFUvQf();
    return (object) (Visibility) ((bool) _param1 ? (int) visibility1 : (int) visibility2);
  }

  public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
  {
    return (object) true;
  }
}
