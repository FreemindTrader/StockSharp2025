// Decompiled with JetBrains decompiler
// Type: #=zbcX$ot$Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
internal abstract class \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T> : 
  \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG47w9OaqF_aMuilIfFliOpay<T>,
  ITickProvider
  where T : IComparable
{
  private IAxis \u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D;

  public IAxis \u0023\u003DzHZDgUSdfqmkx()
  {
    return this.\u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D;
  }

  protected void \u0023\u003DzkF76BMTQOROh(
    IAxis _param1)
  {
    this.\u0023\u003Dz_HMHLf7iUOmptZScZA\u003D\u003D = _param1;
  }

  public virtual void \u0023\u003DzWzUaFxw\u003D(
    IAxis _param1)
  {
    this.\u0023\u003DzkF76BMTQOROh(_param1);
  }

  double[] ITickProvider.\u0023\u003Dzi_t7eeX4F5JXHEvvNMYntVuE\u0024DOzJypZ_yUUtsy7UfgGUn\u0024KmQ\u003D\u003D(
    IAxisParams _param1)
  {
    return this.\u0023\u003DzzRdFGRW8MXMa(this.\u0023\u003Dzctqa9kMCtfQQ(_param1));
  }

  double[] ITickProvider.\u0023\u003Dz9Un\u00242WfBWxcgtkVaLDekO\u0024WlNKRH8oky5JNikYGv5urX2CiRow\u003D\u003D(
    IAxisParams _param1)
  {
    return this.\u0023\u003DzzRdFGRW8MXMa(this.\u0023\u003Dz65PoZl8ZJBOc(_param1));
  }

  protected virtual double[] \u0023\u003DzzRdFGRW8MXMa(T[] _param1)
  {
    return ((IEnumerable<T>) _param1).Select<T, double>(\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T>.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D ?? (\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T>.SomeClass34343383.\u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D = new Func<T, double>(\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T>.SomeClass34343383.SomeMethond0343.\u0023\u003Dzjr185UPePXNseau9xK_z_0g\u003D))).ToArray<double>();
  }

  public abstract T[] \u0023\u003Dzctqa9kMCtfQQ(
    IAxisParams _param1);

  public abstract T[] \u0023\u003Dz65PoZl8ZJBOc(
    IAxisParams _param1);

  [Serializable]
  private sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T>.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC32XliaaE3r6bFuapa3pO8HOc<T>.SomeClass34343383();
    public static Func<T, double> \u0023\u003Dzpc02twqHC0yNe7RFKg\u003D\u003D;

    internal double \u0023\u003Dzjr185UPePXNseau9xK_z_0g\u003D(T _param1)
    {
      return _param1.ToDouble();
    }
  }
}
