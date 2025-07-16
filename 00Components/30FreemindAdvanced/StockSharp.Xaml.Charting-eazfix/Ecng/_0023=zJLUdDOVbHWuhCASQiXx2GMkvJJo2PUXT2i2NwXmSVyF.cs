// Decompiled with JetBrains decompiler
// Type: #=zJLUdDOVbHWuhCASQiXx2GMkvJJo2PUXT2i2NwXmSVyFVz40zm$fWhZNPRwHj98V9_lHhN68=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public sealed class VertexSourceApplyTransform : 
  IVertexSource
{
  private IVertexSource \u0023\u003Dz4HKgYE47NFoa;
  private ITransform \u0023\u003Dzz7t0PRSAmtgk;

  public VertexSourceApplyTransform(
    IVertexSource _param1,
    ITransform _param2)
  {
    this.\u0023\u003Dz4HKgYE47NFoa = _param1;
    this.\u0023\u003Dzz7t0PRSAmtgk = _param2;
  }

  public void \u0023\u003DzotQWOIc\u003D(
    IVertexSource _param1)
  {
    this.\u0023\u003Dz4HKgYE47NFoa = _param1;
  }

  public void \u0023\u003DzVawdK5C5Lyf_(int _param1)
  {
    this.\u0023\u003Dz4HKgYE47NFoa.\u0023\u003DzVawdK5C5Lyf_(_param1);
  }

  public Path.\u0023\u003Dz9kUnn38\u003D \u0023\u003DzxfekdAs1X3YM(
    out double _param1,
    out double _param2)
  {
    int num = (int) this.\u0023\u003Dz4HKgYE47NFoa.\u0023\u003DzxfekdAs1X3YM(out _param1, out _param2);
    if (!Path.\u0023\u003DzepfxPD_ghBSfgm\u0024Sfw\u003D\u003D((Path.\u0023\u003Dz9kUnn38\u003D) num))
      return (Path.\u0023\u003Dz9kUnn38\u003D) num;
    this.\u0023\u003Dzz7t0PRSAmtgk.\u0023\u003DzhA5n1D0\u003D(ref _param1, ref _param2);
    return (Path.\u0023\u003Dz9kUnn38\u003D) num;
  }

  public void \u0023\u003Dzld0WE_PMxv\u0024Y(
    ITransform _param1)
  {
    this.\u0023\u003Dzz7t0PRSAmtgk = _param1;
  }
}
