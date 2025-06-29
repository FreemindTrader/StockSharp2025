// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x$GHuPMdKXH0icPdKkp5z7HSJCOy
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable enable
internal sealed class \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy : 
  ChartBaseViewModel
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly 
  #nullable disable
  Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color> \u0023\u003Dz6rrn61zvEDwM;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DzFIYMKhNJj4Yf;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DzChcrgRo\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string \u0023\u003DzEBLPoa4\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color \u0023\u003Dzfzo3Zt0\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D \u0023\u003DzXRhpdYI8boz_;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB \u0023\u003DzTOCXFN06qVgbO6kYig\u003D\u003D;

  public \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(
    INotifyPropertyChanged _param1,
    Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color> _param2,
    Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> _param3,
    params string[] _param4)
    : this((string) null, _param1, _param2, _param3, _param4)
  {
  }

  public \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy(
    string _param1,
    INotifyPropertyChanged _param2,
    Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color> _param3,
    Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> _param4,
    params string[] _param5)
  {
    \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D kiaDl76b0Nyu42rxJq = new \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy.\u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D();
    kiaDl76b0Nyu42rxJq.\u0023\u003DzdRM6y1Xa8O0P = _param5;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    kiaDl76b0Nyu42rxJq.\u0023\u003DzRRvwDu67s9Rm = this;
    this.Name = _param1;
    this.\u0023\u003Dz6rrn61zvEDwM = _param3;
    this.\u0023\u003DzFIYMKhNJj4Yf = _param4;
    string[] zdRm6y1Xa8O0P = kiaDl76b0Nyu42rxJq.\u0023\u003DzdRM6y1Xa8O0P;
    if ((zdRm6y1Xa8O0P != null ? (zdRm6y1Xa8O0P.Length != 0 ? 1 : 0) : 0) == 0)
      return;
    _param2.PropertyChanged += new PropertyChangedEventHandler(kiaDl76b0Nyu42rxJq.\u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D);
  }

  public \u0023\u003DzfuNSIBalvsZFtWGR3evczlu8c0hHILDz7oIFnPPdzY2A4VgOP\u0024CeDIqsTdzB Parent
  {
    get => this.\u0023\u003DzTOCXFN06qVgbO6kYig\u003D\u003D;
    set => this.\u0023\u003DzTOCXFN06qVgbO6kYig\u003D\u003D = value;
  }

  public string Name
  {
    get => this.\u0023\u003DzChcrgRo\u003D;
    set
    {
      this.SetField<string>(ref this.\u0023\u003DzChcrgRo\u003D, value, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433348));
    }
  }

  public Color Color
  {
    get => this.\u0023\u003Dzfzo3Zt0\u003D;
    set
    {
      this.SetField<Color>(ref this.\u0023\u003Dzfzo3Zt0\u003D, value, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433444));
    }
  }

  public string Value
  {
    get => this.\u0023\u003DzEBLPoa4\u003D;
    set
    {
      this.SetField<string>(ref this.\u0023\u003DzEBLPoa4\u003D, value, \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539324386));
    }
  }

  public static Color \u0023\u003DzLHZrYbpAZyAx(Color _param0, Color _param1)
  {
    return (int) _param0.A <= (int) _param1.A ? _param1 : _param0;
  }

  public void \u0023\u003DzGPrsWyT8SibF(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    this.\u0023\u003DzXRhpdYI8boz_ = _param1;
    this.Color = this.\u0023\u003Dz6rrn61zvEDwM(this.\u0023\u003DzXRhpdYI8boz_);
    this.Value = this.\u0023\u003DzFIYMKhNJj4Yf(this.\u0023\u003DzXRhpdYI8boz_);
  }

  private sealed class \u0023\u003DzzKVd_KIaDl76b0Nyu42rxJQ\u003D
  {
    public string[] \u0023\u003DzdRM6y1Xa8O0P;
    public \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZvdCgmE37pS0x\u0024GHuPMdKXH0icPdKkp5z7HSJCOy \u0023\u003DzRRvwDu67s9Rm;

    internal void \u0023\u003DzPc3AK\u0024sNtgO1kq4Bew\u003D\u003D(
      #nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2)
    {
      if (!((IEnumerable<string>) this.\u0023\u003DzdRM6y1Xa8O0P).Contains<string>(_param2.PropertyName) || this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzXRhpdYI8boz_ == null)
        return;
      this.\u0023\u003DzRRvwDu67s9Rm.Color = this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz6rrn61zvEDwM(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzXRhpdYI8boz_);
    }
  }
}
