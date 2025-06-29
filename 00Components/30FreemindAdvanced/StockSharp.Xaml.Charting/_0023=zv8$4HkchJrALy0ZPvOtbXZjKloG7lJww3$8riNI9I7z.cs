// Decompiled with JetBrains decompiler
// Type: #=zv8$4HkchJrALy0ZPvOtbXZjKloG7lJww3$8riNI9I7z_
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using System;
using System.Windows;

#nullable disable
internal abstract class \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_ : 
  dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd,
  \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB,
  \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS,
  \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z,
  \u0023\u003DzExPUKZPbT0fb9dlf_qOoa7Fo_o9lZIelo\u0024_m4wTHwP6Ifze3\u0024A\u003D\u003D,
  \u0023\u003Dz_\u0024BhX3lQii9_VUtVozqEe06Do2pQ7ReqT8Ks0apzs3KdsLXgXg\u003D\u003D,
  \u0023\u003Dz03BSxVLolBnG92GmtCJpdmgFT25iN3r_1AVHKJDMXa95
{
  IComparable \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS.\u0023\u003DzYH1zUE63H2wnu5PkgVdj8KE9yksdLftO9Wu9AIsnejYvcYebcnx\u0024GS0\u003D()
  {
    return (IComparable) this.MinorDelta;
  }

  void \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS.\u0023\u003DzT6V9cIzTPzymiPsaXC1JFPwb6nII0YqOSZeKgD8JpxJau6W8mO0lqlE\u003D(
    IComparable _param1)
  {
    this.MinorDelta = (TimeSpan) _param1;
  }

  IComparable \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS.\u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQ4SOTZMepbYnETE_r4C8UOZeutWzIpvP1d0\u003D()
  {
    return (IComparable) this.MajorDelta;
  }

  void \u0023\u003Dz6SSn5QQkepq6NeBmeacJnAoj7IAxnW4w0PxdsBxUKNwS.\u0023\u003DzKeWTzo3ARlgJ3_OnyMXBxJ53y9GANuEkCWWh9YLHQaRZjZbSvk2NwxY\u003D(
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

  public override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (this.IsXAxis)
      throw new InvalidOperationException("");
    double num1 = TimeSpan.MinValue.\u0023\u003Dzb9UCYbo\u003D();
    double num2 = TimeSpan.MaxValue.\u0023\u003Dzb9UCYbo\u003D();
    int length = _param1.\u0023\u003Dz4nxjMSnapDjJ.Length;
    for (int index = 0; index < length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
      if (uhIm4pSg8PxqhyA71 != null && ftrixUnpTllY1PkTyq != null && !(uhIm4pSg8PxqhyA71.get_YAxisId() != this.Id))
      {
        dje_zTYH4Q5AG6V7AZV2P5HXXAU5W2KLQCJ87ZM8UWE3W_ejd klqcJ87Zm8UwE3WEjd = ftrixUnpTllY1PkTyq.\u0023\u003DzxNQHuqrEvxH2();
        num2 = num2 < klqcJ87Zm8UwE3WEjd.Min ? num2 : klqcJ87Zm8UwE3WEjd.Min;
        num1 = num1 > klqcJ87Zm8UwE3WEjd.Max ? num1 : klqcJ87Zm8UwE3WEjd.Max;
      }
    }
    return this.\u0023\u003DzJMGFyjEoHSQY((IComparable) num2, (IComparable) num1).\u0023\u003DzzXTqVFg\u003D(this.GrowBy.Min, this.GrowBy.Max);
  }

  protected abstract \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzJMGFyjEoHSQY(
    IComparable _param1,
    IComparable _param2);

  public override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003DzFwoMKP9juTnt()
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = base.\u0023\u003DzFwoMKP9juTnt();
    return this.\u0023\u003DzJMGFyjEoHSQY(abyLt9clZggmJsWhw.Min, abyLt9clZggmJsWhw.Max);
  }

  protected override \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D \u0023\u003Dzd6x7lH_dQH0I()
  {
    \u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D abyLt9clZggmJsWhw = base.\u0023\u003Dzd6x7lH_dQH0I();
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
    \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("", new object[2]
    {
      (object) karIzshiFbv2m5Lhw.\u0023\u003Dzgq30Jn5PclK8(),
      (object) karIzshiFbv2m5Lhw.\u0023\u003DzZ85DqsktXJL3()
    });
  }

  public override IComparable \u0023\u003DzACwLhyc\u003D(double _param1)
  {
    return this.\u0023\u003Dz3ZiX3E6vqtLl(base.\u0023\u003DzACwLhyc\u003D(_param1));
  }

  public override \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB \u0023\u003DzQ8SgRgQ\u003D()
  {
    \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_ instance = (\u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_) Activator.CreateInstance(((object) this).GetType());
    if (this.VisibleRange != null)
      instance.VisibleRange = (\u0023\u003DztyAKlj3UbIrpcOb4hAbyLt9clZggmJsWHw\u003D\u003D) this.VisibleRange.Clone();
    if (this.GrowBy != null)
      instance.GrowBy = (\u0023\u003Dza5uC6EI3X0HH3HGpwdgoZpprK58gKzo0gQ\u003D\u003D<double>) this.GrowBy.Clone();
    return (\u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB) instance;
  }

  HorizontalAlignment \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003Dz5VLaAZX2bctAcuSoajSAXtBWUytTwKmwegWB430RRP_iyVUjrw\u003D\u003D()
  {
    return this.HorizontalAlignment;
  }

  void \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003DzTbSy5Tg7CNKewHb2FguXqzKcL0mg\u0024lar5H2OZ3W_18PGuoI1WA\u003D\u003D(
    HorizontalAlignment _param1)
  {
    this.HorizontalAlignment = _param1;
  }

  VerticalAlignment \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003DzSseiGdgwJmJ1pkmz7CEFfx8mbOWyc1wXvn8wBzjwACKu6EY0OQ\u003D\u003D()
  {
    return this.VerticalAlignment;
  }

  void \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntTNDUky6SmSb6\u0024FDWAQO1Y0HmfujBQ\u003D\u003D(
    VerticalAlignment _param1)
  {
    this.VerticalAlignment = _param1;
  }

  Visibility \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003Dzh5FljKv\u0024Q_lDTADyTGyZRYwo3gBY9dA\u0024Mbe\u0024dG0As1jePnhZWw\u003D\u003D()
  {
    return this.Visibility;
  }

  void \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntSyV0Rj0ibC6aIMhpwJ2VCPFsZZaBw\u003D\u003D(
    Visibility _param1)
  {
    this.Visibility = _param1;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.\u0023\u003Dz4lH8q7tXMt_gtLJO2itFk_uTcHPb_FD6TqCanmMNLu1qiOPHXwlPSNY\u003D()
  {
    return this.ActualWidth;
  }

  double \u0023\u003DzzF1ExzlVBfOa5IIxZ\u0024bDKBa6QBHQt0COuh5AtkBhEO3z.\u0023\u003DzzsyKnUNUDKjF7rDv70izN8J6fpW\u0024OkM14cKsD6c_CdYLZ77RJxzrNo0\u003D()
  {
    return this.ActualHeight;
  }
}
