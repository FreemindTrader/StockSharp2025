// Decompiled with JetBrains decompiler
// Type: #=zv8$4HkchJrALy0ZPvOtbXZjKloG7lJww3$8riNI9I7z_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.Windows;

#nullable disable
internal abstract class \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_ : 
  dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd,
  IDrawable,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  IAxis,
  IHitTestable
{
  IComparable IAxisParams.\u0023\u003DzYH1zUE63H2wnu5PkgVdj8KE9yksdLftO9Wu9AIsnejYvcYebcnx\u0024GS0\u003D()
  {
    return (IComparable) this.MinorDelta;
  }

  void IAxisParams.\u0023\u003DzT6V9cIzTPzymiPsaXC1JFPwb6nII0YqOSZeKgD8JpxJau6W8mO0lqlE\u003D(
    IComparable _param1)
  {
    this.MinorDelta = (TimeSpan) _param1;
  }

  IComparable IAxisParams.\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ4SOTZMepbYnETE_r4C8UOZeutWzIpvP1d0\u003D()
  {
    return (IComparable) this.MajorDelta;
  }

  void IAxisParams.\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxJ53y9GANuEkCWWh9YLHQaRZjZbSvk2NwxY\u003D(
    IComparable _param1)
  {
    this.MajorDelta = (TimeSpan) _param1;
  }

  public TimeSpan MajorDelta
  {
    get
    {
      return (TimeSpan) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzuTkKbr18L_Ur);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzuTkKbr18L_Ur, (object) value);
    }
  }

  public TimeSpan MinorDelta
  {
    get
    {
      return (TimeSpan) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzdg3vng1nptYM);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dzdg3vng1nptYM, (object) value);
    }
  }

  public TimeSpan? MinimalZoomConstrain
  {
    get
    {
      return (TimeSpan?) this.GetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F);
    }
    set
    {
      this.SetValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzjWJEoVoxRw8F, (object) value);
    }
  }

  public override IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (this.IsXAxis)
      throw new InvalidOperationException("CalculateYRange is only valid on Y-Axis types");
    double num1 = TimeSpan.MinValue.ToDouble();
    double num2 = TimeSpan.MaxValue.ToDouble();
    int length = _param1.\u0023\u003Dz4nxjMSnapDjJ.Length;
    for (int index = 0; index < length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
      if (uhIm4pSg8PxqhyA71 != null && ftrixUnpTllY1PkTyq != null && !(uhIm4pSg8PxqhyA71.get_YAxisId() != this.Id))
      {
        DoubleRange klqcJ87Zm8UwE3WEjd = ftrixUnpTllY1PkTyq.\u0023\u003DzxNQHuqrEvxH2();
        num2 = num2 < klqcJ87Zm8UwE3WEjd.Min ? num2 : klqcJ87Zm8UwE3WEjd.Min;
        num1 = num1 > klqcJ87Zm8UwE3WEjd.Max ? num1 : klqcJ87Zm8UwE3WEjd.Max;
      }
    }
    return this.\u0023\u003DzJMGFyjEoHSQY((IComparable) num2, (IComparable) num1).GrowBy(this.GrowBy.Min, this.GrowBy.Max);
  }

  protected abstract IRange \u0023\u003DzJMGFyjEoHSQY(
    IComparable _param1,
    IComparable _param2);

  public override IRange \u0023\u003DzFwoMKP9juTnt()
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003DzFwoMKP9juTnt();
    return this.\u0023\u003DzJMGFyjEoHSQY(abyLt9clZggmJsWhw.Min, abyLt9clZggmJsWhw.Max);
  }

  protected override IRange \u0023\u003Dzd6x7lH_dQH0I()
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003Dzd6x7lH_dQH0I();
    if (abyLt9clZggmJsWhw != null)
      abyLt9clZggmJsWhw = this.\u0023\u003DzJMGFyjEoHSQY(abyLt9clZggmJsWhw.Min, abyLt9clZggmJsWhw.Max);
    return abyLt9clZggmJsWhw;
  }

  protected override void \u0023\u003Dz2RD3F8MtvzO1()
  {
    if (!this.AutoTicks)
      return;
    \u0023\u003DzQN2Zes8h9tElvYmX48o49BUYcGhHuBjHtpu_APLaHrAgLQA2lg\u003D\u003D htpuApLaHrAgLqA2lg = (\u0023\u003DzQN2Zes8h9tElvYmX48o49BUYcGhHuBjHtpu_APLaHrAgLQA2lg\u003D\u003D) this.\u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D();
    uint num1 = this.\u0023\u003Dzl02YIEvJDKYh();
    IComparable min = this.VisibleRange.Min;
    IComparable max = this.VisibleRange.Max;
    int minorsPerMajor = this.MinorsPerMajor;
    int num2 = (int) num1;
    \u0023\u003DzPm\u0024a5jxBEPxWxb6PrKARIzshiFBv2m5Lhw\u003D\u003D karIzshiFbv2m5Lhw = htpuApLaHrAgLqA2lg.\u0023\u003DzyE145DTzxI8R(min, max, minorsPerMajor, (uint) num2);
    this.MajorDelta = karIzshiFbv2m5Lhw.\u0023\u003Dzgq30Jn5PclK8();
    this.MinorDelta = karIzshiFbv2m5Lhw.\u0023\u003DzZ85DqsktXJL3();
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("CalculateDelta: Major={0}, Minor={1}", new object[2]
    {
      (object) karIzshiFbv2m5Lhw.\u0023\u003Dzgq30Jn5PclK8(),
      (object) karIzshiFbv2m5Lhw.\u0023\u003DzZ85DqsktXJL3()
    });
  }

  public override IComparable GetDataValue(double _param1)
  {
    return this.\u0023\u003Dz3ZiX3E6vqtLl(base.GetDataValue(_param1));
  }

  public override IAxis \u0023\u003DzQ8SgRgQ\u003D()
  {
    \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_ instance = (\u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_) Activator.CreateInstance(((object) this).GetType());
    if (this.VisibleRange != null)
      instance.VisibleRange = (IRange) this.VisibleRange.Clone();
    if (this.GrowBy != null)
      instance.GrowBy = (IRange<double>) this.GrowBy.Clone();
    return (IAxis) instance;
  }

  HorizontalAlignment IAxis.\u0023\u003Dz5VLaAZX2bctAcuSoajSAXtBWUytTwKmwegWB430RRP_iyVUjrw\u003D\u003D()
  {
    return this.HorizontalAlignment;
  }

  void IAxis.\u0023\u003DzTbSy5Tg7CNKewHb2FguXqzKcL0mg\u0024lar5H2OZ3W_18PGuoI1WA\u003D\u003D(
    HorizontalAlignment _param1)
  {
    this.HorizontalAlignment = _param1;
  }

  VerticalAlignment IAxis.\u0023\u003DzSseiGdgwJmJ1pkmz7CEFfx8mbOWyc1wXvn8wBzjwACKu6EY0OQ\u003D\u003D()
  {
    return this.VerticalAlignment;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntTNDUky6SmSb6\u0024FDWAQO1Y0HmfujBQ\u003D\u003D(
    VerticalAlignment _param1)
  {
    this.VerticalAlignment = _param1;
  }

  Visibility IAxis.\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRYwo3gBY9dA\u0024Mbe\u0024dG0As1jePnhZWw\u003D\u003D()
  {
    return this.Visibility;
  }

  void IAxis.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntSyV0Rj0ibC6aIMhpwJ2VCPFsZZaBw\u003D\u003D(
    Visibility _param1)
  {
    this.Visibility = _param1;
  }

  double IHitTestable.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double IHitTestable.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }
}
