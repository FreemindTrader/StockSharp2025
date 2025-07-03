// Decompiled with JetBrains decompiler
// Type: -.BaseMountainRenderableSeries
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace \u002D;

internal abstract class BaseMountainRenderableSeries : 
  BaseRenderableSeries
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D = DependencyProperty.Register(nameof (IsDigitalLine), typeof (bool), typeof (BaseMountainRenderableSeries), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003DzXc9apgJiH9mm = DependencyProperty.Register(nameof (AreaBrush), typeof (Brush), typeof (BaseMountainRenderableSeries), new PropertyMetadata((object) null, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private Color \u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D;

  protected BaseMountainRenderableSeries()
  {
    this.SetCurrentValue(BaseRenderableSeries.\u0023\u003DzZgWT7YttYHbwyP3zHCVW0zI\u003D, (object) \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.Max);
  }

  public Brush AreaBrush
  {
    get
    {
      return (Brush) this.GetValue(BaseMountainRenderableSeries.\u0023\u003DzXc9apgJiH9mm);
    }
    set
    {
      this.SetValue(BaseMountainRenderableSeries.\u0023\u003DzXc9apgJiH9mm, (object) value);
    }
  }

  public Color \u0023\u003DzrCwKa1niXYKW() => this.\u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D;

  public void \u0023\u003DzIIOrZiK2cjgx(Color _param1)
  {
    this.\u0023\u003Dz7whQ6lqoKrkWxT8_LQ\u003D\u003D = _param1;
  }

  public bool IsDigitalLine
  {
    get
    {
      return (bool) this.GetValue(BaseMountainRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D);
    }
    set
    {
      this.SetValue(BaseMountainRenderableSeries.\u0023\u003Dz777sMZMTOybHlBhdug\u003D\u003D, (object) value);
    }
  }
}
