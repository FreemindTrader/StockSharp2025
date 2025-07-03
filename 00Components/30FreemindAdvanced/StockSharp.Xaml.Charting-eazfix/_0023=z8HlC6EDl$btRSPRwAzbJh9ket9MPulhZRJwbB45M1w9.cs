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

#nullable disable
internal sealed class \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY(
  ChartAnnotation _param1) : 
  \u0023\u003DzBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB\u0024<ChartAnnotation>(_param1)
{
  protected override void \u0023\u003DzowR7R4A\u003D()
  {
  }

  protected override void \u0023\u003DzXfak0jM\u003D()
  {
    this.\u0023\u003Dz\u00246aIVrHDxlRJ().AnnotationModifier.\u0023\u003Dz\u0024abmkXc\u003D(this.\u0023\u003DzeaszzAAoBOY9());
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    this.\u0023\u003DzY_lPK_VP\u0024B7_(new Action(new \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY.\u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D()
    {
      \u0023\u003DzRRvwDu67s9Rm = this,
      \u0023\u003Dzor6OKKQ\u003D = _param1
    }.\u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D), true);
    return true;
  }

  private sealed class \u0023\u003Dz6U5SVX_6MHYdSkOPOA\u003D\u003D
  {
    public \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh9ket9MPulhZRJwbB45M1w92HjAe5qWGx_96jzkY \u0023\u003DzRRvwDu67s9Rm;
    public IEnumerableEx<ChartDrawData.IDrawValue> \u0023\u003Dzor6OKKQ\u003D;

    internal void \u0023\u003DzgXmWJEIKFwdJ3WKiTA\u003D\u003D()
    {
      this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003Dz\u00246aIVrHDxlRJ().AnnotationModifier.Draw(this.\u0023\u003DzRRvwDu67s9Rm.\u0023\u003DzeaszzAAoBOY9(), ((IEnumerable) this.\u0023\u003Dzor6OKKQ\u003D).Cast<ChartDrawData.AnnotationData>().Single<ChartDrawData.AnnotationData>());
    }
  }
}
