// Decompiled with JetBrains decompiler
// Type: #=z8HlC6EDl$btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Linq;
namespace StockSharp.Xaml.Charting;
#nullable disable
internal sealed class ChartAnnotationViewModel( ChartAnnotation annotation) :  ChartCompentWpfBaseViewModel<ChartAnnotation>(annotation)
{
  protected override void UpdateUi()
  {
  }

  protected override void Clear()
  {
    this.ScichartSurfaceMVVM.AnnotationModifier.GuiUpdateAndClear(this.ChartComponentView);
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    this.\u0023\u003DzY_lPK_VP\u0024B7_(new Action(new ChartAnnotationViewModel.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D()
    {
      _variableSome3535 = this,
      \u0023\u003Dzor6OKKQ\u003D = _param1
    }.\u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D), true);
    return true;
  }

  private sealed class \u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D
  {
    public ChartAnnotationViewModel _variableSome3535;
    public IEnumerableEx<ChartDrawData.IDrawValue> \u0023\u003Dzor6OKKQ\u003D;

    internal void \u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D()
    {
      this._variableSome3535.ScichartSurfaceMVVM.AnnotationModifier.Draw(this._variableSome3535.ChartComponentView, ((IEnumerable) this.\u0023\u003Dzor6OKKQ\u003D).Cast<ChartDrawData.AnnotationData>().Single<ChartDrawData.AnnotationData>());
    }
  }
}
