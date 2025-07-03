// Decompiled with JetBrains decompiler
// Type: #=zvQvE6Bu$W$4U309jzRCs4fms0TWWxAeIRqZFKIYsXfsGW8Eo$qNNchFzm6GiqJLhJL$N$KU=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Runtime.CompilerServices;

#nullable disable
internal class \u0023\u003DzvQvE6Bu\u0024W\u00244U309jzRCs4fms0TWWxAeIRqZFKIYsXfsGW8Eo\u0024qNNchFzm6GiqJLhJL\u0024N\u0024KU\u003D : 
  \u0023\u003DzsDU9XQyTsl2DjEg2HhKpBHrPPgTSsTv2U_6gZ4ERd6w55yk5zndcPe_vl2HLhMqz\u0024A\u003D\u003D
{
  protected \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D \u0023\u003DzcRozJLFRZ0BY;
  protected int \u0023\u003DzI6P8IpE\u003D;
  protected int \u0023\u003DzgwQEFlDsVMon;
  protected int \u0023\u003DzFfSb8y0\u003D;
  protected int \u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D;
  protected float[] \u0023\u003DzDm5AkRtEtmYQ;
  protected int \u0023\u003DzLVC\u00242OK7OxP3 = -1;
  private int \u0023\u003Dz9LgI12vZMy\u0024F;

  public \u0023\u003DzvQvE6Bu\u0024W\u00244U309jzRCs4fms0TWWxAeIRqZFKIYsXfsGW8Eo\u0024qNNchFzm6GiqJLhJL\u0024N\u0024KU\u003D(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1)
  {
    this.\u0023\u003DzotQWOIc\u003D(_param1);
  }

  private void \u0023\u003DzotQWOIc\u003D(
    \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D _param1)
  {
    this.\u0023\u003DzcRozJLFRZ0BY = _param1;
    this.\u0023\u003DzDm5AkRtEtmYQ = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
    this.\u0023\u003Dz9LgI12vZMy\u0024F = this.\u0023\u003DzcRozJLFRZ0BY.Width;
    this.\u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzaqBjqW\u00249N6vELXAC78288VQ\u003D();
  }

  [SpecialName]
  public \u0023\u003DzUib3SzczDtLU7txM4YiSeAyIBVtxyMwwBNQ1qn4xMV3gcDwD1G4meJ\u0024PCf2w5LQ6sg\u003D\u003D \u0023\u003Dz8hY71usSYcKH()
  {
    return this.\u0023\u003DzcRozJLFRZ0BY;
  }

  private float[] \u0023\u003Dz\u00246e75ZE\u003D(out int _param1)
  {
    int num1 = this.\u0023\u003DzI6P8IpE\u003D;
    int num2 = this.\u0023\u003DzFfSb8y0\u003D;
    if ((uint) num1 >= (uint) this.\u0023\u003DzcRozJLFRZ0BY.Width)
      num1 = num1 >= 0 ? this.\u0023\u003DzcRozJLFRZ0BY.Width - 1 : 0;
    if ((uint) num2 >= (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height)
      num2 = num2 >= 0 ? this.\u0023\u003DzcRozJLFRZ0BY.Height - 1 : 0;
    _param1 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(num1, num2);
    return this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
  }

  public float[] \u0023\u003DzmiTEKiA\u003D(
    int _param1,
    int _param2,
    int _param3,
    out int _param4)
  {
    this.\u0023\u003DzI6P8IpE\u003D = this.\u0023\u003DzgwQEFlDsVMon = _param1;
    this.\u0023\u003DzFfSb8y0\u003D = _param2;
    if ((uint) _param2 < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height && _param1 >= 0 && _param1 + _param3 <= this.\u0023\u003DzcRozJLFRZ0BY.Width)
    {
      _param4 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
      this.\u0023\u003DzDm5AkRtEtmYQ = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003Dz9b1_JhA\u003D();
      this.\u0023\u003DzLVC\u00242OK7OxP3 = _param4;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    this.\u0023\u003DzLVC\u00242OK7OxP3 = -1;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param4);
  }

  public float[] \u0023\u003DziwTQ98wsEeu3(out int _param1)
  {
    if (this.\u0023\u003DzLVC\u00242OK7OxP3 != -1)
    {
      this.\u0023\u003DzLVC\u00242OK7OxP3 += this.\u0023\u003DzVllaPSUsIxTUfFLaKw\u003D\u003D;
      _param1 = this.\u0023\u003DzLVC\u00242OK7OxP3;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    ++this.\u0023\u003DzI6P8IpE\u003D;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param1);
  }

  public float[] \u0023\u003DzXgTbLkAgh1Pd(out int _param1)
  {
    ++this.\u0023\u003DzFfSb8y0\u003D;
    this.\u0023\u003DzI6P8IpE\u003D = this.\u0023\u003DzgwQEFlDsVMon;
    if (this.\u0023\u003DzLVC\u00242OK7OxP3 != -1 && (uint) this.\u0023\u003DzFfSb8y0\u003D < (uint) this.\u0023\u003DzcRozJLFRZ0BY.Height)
    {
      this.\u0023\u003DzLVC\u00242OK7OxP3 = this.\u0023\u003DzcRozJLFRZ0BY.\u0023\u003DzHlHGfKJZNJsq(this.\u0023\u003DzI6P8IpE\u003D, this.\u0023\u003DzFfSb8y0\u003D);
      _param1 = this.\u0023\u003DzLVC\u00242OK7OxP3;
      return this.\u0023\u003DzDm5AkRtEtmYQ;
    }
    this.\u0023\u003DzLVC\u00242OK7OxP3 = -1;
    return this.\u0023\u003Dz\u00246e75ZE\u003D(out _param1);
  }
}
