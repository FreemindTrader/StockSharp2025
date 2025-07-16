// Decompiled with JetBrains decompiler
// Type: #=zN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD$n9L813isRbgur3b1bKA73O8
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

#nullable disable
public sealed class \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8 : 
  dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd,
  IDrawable,
  \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D,
  IAxisParams,
  ISuspendable,
  IInvalidatableElement,
  IAxis,
  IHitTestable
{
  
  public static readonly DependencyProperty \u0023\u003Dzjt8sdPO9\u0024qkNnCrlQyjSA38\u003D = DependencyProperty.Register(nameof (LogarithmicBase), typeof (double), typeof (\u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8), new PropertyMetadata((object) 10.0, new PropertyChangedCallback(\u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8.\u0023\u003DzUSope7CyybJN14Toh8PD_14\u003D)));

  public \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8()
  {
    this.LabelProvider = (\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_U1qeTmG05tjnxhrXf80OTVH) new \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOg8AZz2RC1cZ1hYdlZ22mOTs6M1Rc5MHAxGSwybk_pTjg\u003D\u003D();
    this.SetCurrentValue(dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003Dz1bLZaITSYGdx, (object) new \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3Bm8RZiwmK4L3EV3D9Q_3Sui7NwtBg1zT9cdY4yX());
  }

  private static void \u0023\u003DzUSope7CyybJN14Toh8PD_14\u003D(
    DependencyObject _param0,
    DependencyPropertyChangedEventArgs _param1)
  {
    if (!(_param0 is \u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8 l813isRbgur3b1bKa73O8))
      return;
    if (l813isRbgur3b1bKa73O8.LogarithmicBase <= 0.0)
      throw new InvalidOperationException($"The value {l813isRbgur3b1bKa73O8.LogarithmicBase} is not a valid base for the LogarithmicNumericAxis.");
    dje_zZVEBX5NJ2AQTDXQ94AUTAJRYAXNKUH4NHECKVD8AXF9ZGQ7NBH9KS_ejd.\u0023\u003DzLUQi5D4\u003D(_param0, _param1);
  }

  [TypeConverter(typeof (\u0023\u003DzQ4iRj1YTApc8D349VbLPORGhNf\u0024GoPfbAl4lXkr2qbdyCeSLr0MtuSBxZdlBDY\u0024hamCa\u0024IQMK2bl))]
  public double LogarithmicBase
  {
    get
    {
      return (double) this.GetValue(\u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8.\u0023\u003Dzjt8sdPO9\u0024qkNnCrlQyjSA38\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzN0ICfvrLGc6u90AzzFcyQrNHxk0FFK6Qy3UD\u0024n9L813isRbgur3b1bKA73O8.\u0023\u003Dzjt8sdPO9\u0024qkNnCrlQyjSA38\u003D, (object) value);
    }
  }

  public override bool IsLogarithmicAxis => true;

  public override \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw3_fwmcVcA0rHXkV5W8VrNVY \u0023\u003Dz0RktzzbyC\u002468()
  {
    return base.\u0023\u003Dz0RktzzbyC\u002468() with
    {
      \u0023\u003Dzh2T5GvjINwPwgFALsCLUJis\u003D = true,
      \u0023\u003DzY9K_6JLtXDZUY_yhDkngupc\u003D = this.LogarithmicBase
    };
  }

  public override bool \u0023\u003Dz2OKbyRBzRCBL(
    IRange _param1)
  {
    return base.\u0023\u003Dz2OKbyRBzRCBL(_param1) && Math.Sign(_param1.Min.ToDouble()) == Math.Sign(_param1.Max.ToDouble());
  }

  protected override \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D \u0023\u003Dzgy73vTR0r5jyI3j3hAgwZho\u003D()
  {
    \u0023\u003DzolvWmzKCnovSLB\u0024fEd65U320fYSggUo83Kx0knNe1ENtNGBmir7icAK_u1t0 entNgBmir7icAkU1t0 = (\u0023\u003DzolvWmzKCnovSLB\u0024fEd65U320fYSggUo83Kx0knNe1ENtNGBmir7icAK_u1t0) \u0023\u003DzolvWmzKCnovSLB\u0024fEd65U320fYSggUo83Kx0knNe1ENtNGBmir7icAK_u1t0.\u0023\u003DzFvAsfEI\u003D();
    entNgBmir7icAkU1t0.\u0023\u003DzY2pNM8i3KOHB8USXquggYrI\u003D(this.LogarithmicBase);
    return (\u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWu7vteBmpkBQwFYGp0VhHiJ5hoI4CA\u003D\u003D) entNgBmir7icAkU1t0;
  }

  protected override TickCoordinates \u0023\u003DzyPl0NtN\u0024cLlA()
  {
    if (this.TickProvider is \u0023\u003DzFDK4fEILkMRswIjIg1\u0024y3Bm8RZiwmK4L3EV3D9Q_3Sui7NwtBg1zT9cdY4yX tickProvider)
      tickProvider.\u0023\u003DzY2pNM8i3KOHB8USXquggYrI\u003D(this.LogarithmicBase);
    return base.\u0023\u003DzyPl0NtN\u0024cLlA();
  }

  public override IRange \u0023\u003Dz8dMR0vhnuqhVVjJNjQ\u003D\u003D()
  {
    return (IRange) new DoubleRange(Math.Pow(this.LogarithmicBase, -1.0), Math.Pow(this.LogarithmicBase, 2.0));
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
