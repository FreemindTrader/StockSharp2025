// Decompiled with JetBrains decompiler
// Type: -.dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

#nullable disable
namespace \u002D;

internal sealed class dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd : 
  \u0023\u003Dzv8\u00244HkchJrALy0ZPvOtbXZjKloG7lJww3\u00248riNI9I7z_
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly List<Type> \u0023\u003DzVGdWd1PKAs\u00242 = ((IEnumerable<Type>) new Type[1]
  {
    typeof (DateTime)
  }).ToList<Type>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public static readonly DependencyProperty \u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D = DependencyProperty.Register(nameof (SubDayTextFormatting), typeof (string), typeof (dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd), new PropertyMetadata((object) null, new PropertyChangedCallback(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D)));

  public dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd()
  {
    this.DefaultStyleKey = (object) typeof (dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd);
    this.DefaultLabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003Dz9jHRW\u00244hcTcRirEhLafLfKwkzeHFx2BtVDw8LCsrGTu1();
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003DzJhc8WdlQgSkcniY\u0024669aniHe9rfoFyUgrbTADSj0lBiy());
  }

  public string SubDayTextFormatting
  {
    get
    {
      return (string) this.GetValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D);
    }
    set
    {
      this.SetValue(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz5Kre9LKvddWFL51pIQ\u003D\u003D, (object) value);
    }
  }

  public override IRange \u0023\u003DzzMId\u0024f67Wftb(
    \u0023\u003DzdDznHH56iLab0VjufJI3RvrDHJH0\u0024iDtfw\u003D\u003D _param1)
  {
    if (this.IsXAxis)
      throw new InvalidOperationException("CalculateYRange is only valid on Y-Axis types");
    double val1_1 = DateTime.MinValue.ToDouble();
    double val1_2 = DateTime.MaxValue.ToDouble();
    int length = _param1.\u0023\u003Dz4nxjMSnapDjJ.Length;
    for (int index = 0; index < length; ++index)
    {
      IRenderableSeries uhIm4pSg8PxqhyA71 = _param1.\u0023\u003DzRS6ptUHIm4pSg8PXQHYA71s\u003D[index];
      \u0023\u003DzAJ2g5KE5bawCuhjG0TamYmz92FTRIX_UnpTLlY1PkTYQ ftrixUnpTllY1PkTyq = _param1.\u0023\u003Dz4nxjMSnapDjJ[index];
      if (uhIm4pSg8PxqhyA71 != null && ftrixUnpTllY1PkTyq != null && !(uhIm4pSg8PxqhyA71.get_YAxisId() != this.Id))
      {
        DoubleRange klqcJ87Zm8UwE3WEjd = ftrixUnpTllY1PkTyq.\u0023\u003DzxNQHuqrEvxH2();
        val1_2 = val1_2 < klqcJ87Zm8UwE3WEjd.Min ? val1_2 : klqcJ87Zm8UwE3WEjd.Min;
        val1_1 = val1_1 > klqcJ87Zm8UwE3WEjd.Max ? val1_1 : klqcJ87Zm8UwE3WEjd.Max;
      }
    }
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(new DateTime(Math.Min((long) val1_2, DateTime.MaxValue.Ticks)), new DateTime(Math.Max((long) val1_1, DateTime.MinValue.Ticks))).GrowBy(this.GrowBy.Min, this.GrowBy.Max);
  }

  public override IRange \u0023\u003DzFwoMKP9juTnt()
  {
    IRange abyLt9clZggmJsWhw = base.\u0023\u003DzFwoMKP9juTnt();
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(abyLt9clZggmJsWhw.Min.\u0023\u003Dzxuo5aY4wjkaI(), abyLt9clZggmJsWhw.Max.\u0023\u003Dzxuo5aY4wjkaI());
  }

  protected override IRange \u0023\u003DzsB7Y9t30CQ63(
    IRange _param1)
  {
    long ticks = TimeSpan.FromDays(1.0).Ticks;
    return \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) (_param1.Min.ToDouble() - (double) ticks), (IComparable) (_param1.Max.ToDouble() + (double) ticks));
  }

  public override void \u0023\u003DzQ4klw1orSVl\u0024(Type _param1)
  {
    if (!dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003DzVGdWd1PKAs\u00242.Contains(_param1))
      throw new InvalidOperationException($"DateTimeAxis does not support the type {_param1}. Supported types include {string.Join(", ", dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003DzVGdWd1PKAs\u00242.Select<Type, string>(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D ?? (dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D = new Func<Type, string>(dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz7qOdpi4\u003D.\u0023\u003DzhxV_97w\u003D.\u0023\u003Dzxhk169PwYjC0CfRyZh2qvME\u003D))).ToArray<string>())}");
  }

  public override IRange \u0023\u003DzspbjXJnVtbB\u0024()
  {
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D();
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    DateTime date = DateTime.UtcNow.Date;
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(date.AddDays(-1.0), date.AddDays(1.0));
  }

  protected override IRange \u0023\u003DzJMGFyjEoHSQY(
    IComparable _param1,
    IComparable _param2)
  {
    return (IRange) new \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D(_param1.\u0023\u003Dzxuo5aY4wjkaI(), _param2.\u0023\u003Dzxuo5aY4wjkaI());
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) \u0023\u003DzpTBWTwmpvpgHkLhFsQhfVp7kErA5SKirgiuHeuzdyo1JF6GbLQ\u003D\u003D.\u0023\u003DzFvAsfEI\u003D();
  }

  protected override IComparable \u0023\u003Dz3ZiX3E6vqtLl(IComparable _param1)
  {
    return (IComparable) _param1.\u0023\u003Dzxuo5aY4wjkaI();
  }

  public override bool \u0023\u003Dz9yvpaTXy3ucx(
    IRange _param1)
  {
    return _param1 is \u0023\u003DzS5mFHV\u0024eXnkCjzbt0Dx26vpI1xWpwwNQJw\u003D\u003D;
  }

  protected override List<Type> \u0023\u003DzvwDcRtQA0c4T()
  {
    return dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003DzVGdWd1PKAs\u00242;
  }

  [Serializable]
  private new sealed class \u0023\u003Dz7qOdpi4\u003D
  {
    public static readonly dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz7qOdpi4\u003D \u0023\u003DzhxV_97w\u003D = new dje_zW9CS5E2KYALJRMCDFUV9GBWAD6S7353K768YQ7ENY8VRCQY29QF62_ejd.\u0023\u003Dz7qOdpi4\u003D();
    public static Func<Type, string> \u0023\u003DzADw0BKPL7SQ6VX_CXg\u003D\u003D;

    internal string \u0023\u003Dzxhk169PwYjC0CfRyZh2qvME\u003D(Type _param1) => _param1.Name;
  }
}
