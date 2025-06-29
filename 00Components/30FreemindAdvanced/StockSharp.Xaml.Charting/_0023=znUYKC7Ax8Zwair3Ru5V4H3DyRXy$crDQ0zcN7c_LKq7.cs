// Decompiled with JetBrains decompiler
// Type: #=znUYKC7Ax8Zwair3Ru5V4H3DyRXy$crDQ0zcN7c_LKq7HenVQrw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System.Diagnostics;

#nullable disable
internal sealed class \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3DyRXy\u0024crDQ0zcN7c_LKq7HenVQrw\u003D\u003D : 
  \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D
{
  
  private double \u0023\u003DzpVRHOdY\u003D;

  public \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3DyRXy\u0024crDQ0zcN7c_LKq7HenVQrw\u003D\u003D(
    IRenderableSeries _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2)
    : base(_param1, _param2)
  {
    this.Percentage = _param2.\u0023\u003Dz5EzXoGYMBJeG1JPrc\u0024BWrUc\u003D();
  }

  public double Percentage
  {
    get => this.\u0023\u003DzpVRHOdY\u003D;
    set
    {
      this.\u0023\u003DzwGPLgl8\u003D<double>(ref this.\u0023\u003DzpVRHOdY\u003D, value, XXX.SSS(-539437471));
    }
  }

  public override void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    base.\u0023\u003DzCadMMgc\u003D(_param1);
    this.Percentage = ((\u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3DyRXy\u0024crDQ0zcN7c_LKq7HenVQrw\u003D\u003D) _param1).Percentage;
  }
}
