// Decompiled with JetBrains decompiler
// Type: #=zboj3ckhISv7k6koCkTeIf05jwZ$CUMIgq7WMHxaPD9pU
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

#nullable disable
internal abstract class \u0023\u003Dzboj3ckhISv7k6koCkTeIf05jwZ\u0024CUMIgq7WMHxaPD9pU
{
  private string \u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;

  protected \u0023\u003Dzboj3ckhISv7k6koCkTeIf05jwZ\u0024CUMIgq7WMHxaPD9pU(string _param1)
  {
    this.Value = _param1;
  }

  protected bool \u0023\u003DzhxbsSqM\u003D(
    \u0023\u003Dzboj3ckhISv7k6koCkTeIf05jwZ\u0024CUMIgq7WMHxaPD9pU _param1)
  {
    return string.Equals(this.Value, _param1?.Value);
  }

  public override int GetHashCode() => this.Value == null ? 0 : this.Value.GetHashCode();

  public string Value
  {
    get => this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D;
    private set => this.\u0023\u003DzeThLMrnu32pVQmpVLQ\u003D\u003D = value;
  }

  public override bool Equals(object _param1)
  {
    return this.\u0023\u003DzhxbsSqM\u003D(_param1 as \u0023\u003Dzboj3ckhISv7k6koCkTeIf05jwZ\u0024CUMIgq7WMHxaPD9pU);
  }

  public override string ToString()
  {
    return string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539441923), (object) this.GetType().Name, (object) (this.Value ?? \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539441972)));
  }
}
