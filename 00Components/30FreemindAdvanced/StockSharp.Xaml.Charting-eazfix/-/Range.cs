// Decompiled with JetBrains decompiler
// Type: -.Range`1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace \u002D;

internal abstract class Range<T> : 
  BindableObject ,
  INotifyPropertyChanged,
  ICloneable,
  IRange<T>,
  IRange
  where T : IComparable
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private T dgr;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private T dgs;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static IMath<T> \u0023\u003DzB8wkVW0YXGlP = MathHelper.GetMath<T>();

  protected Range()
  {
  }

  protected Range(
    T _param1,
    T _param2)
  {
    this.Min = _param1;
    this.Max = _param2;
  }

  public virtual bool IsDefined
  {
    get => this.Max.IsFiniteNumber() && this.Min.IsFiniteNumber();
  }

  IComparable IRange.\u0023\u003DzYvV7blprrv\u0024kuBcS9cPJhPOMjAi3eSq7F9\u0024VAC0\u003D()
  {
    return (IComparable) this.Min;
  }

  void IRange.\u0023\u003DzkAKUJrbqM7JEiA1NxV8i_dHhDAYbBDsyHUJ14hA\u003D(
    IComparable _param1)
  {
    this.Min = (T) _param1;
  }

  IComparable IRange.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG2Kv7N7C7H58hA7fGkg\u003D()
  {
    return (IComparable) this.Max;
  }

  void IRange.\u0023\u003DzPauio66DvxKtWOFEEHOV9Z8NwS3Q53vzn4zJw4g\u003D(
    IComparable _param1)
  {
    this.Max = (T) _param1;
  }

  IComparable IRange.\u0023\u003DzQN2Zes8h9tElvYmX48o49Pepp2LnsyhKmSa3Agc\u003D()
  {
    return (IComparable) this.Diff;
  }

  public abstract bool IsZero { get; }

  public T Min
  {
    get => this.dgr;
    set
    {
      T zWanvsEc = this.dgr;
      this.dgr = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (Min), (object) zWanvsEc, (object) value);
    }
  }

  public T Max
  {
    get => this.dgs;
    set
    {
      T zjV9csI = this.dgs;
      this.dgs = value;
      this.\u0023\u003Dz15moWio\u003D(nameof (Max), (object) zjV9csI, (object) value);
    }
  }

  public abstract T Diff { get; }

  public abstract object Clone();

  public abstract IRange<T> GrowBy(
    double _param1,
    double _param2);

  public abstract IRange<T> \u0023\u003DzJIqIiUw\u003D(
    IRange<T> _param1);

  public abstract DoubleRange AsDoubleRange();

  public abstract IRange<T> SetMinMax(
    double _param1,
    double _param2);

  public abstract IRange<T> SetMinMax(
    double _param1,
    double _param2,
    IRange<T> _param3);

  protected void SetMinMaxInternal(
    T _param1,
    T _param2)
  {
    if (this.Max.CompareTo((object) _param1) < 0)
    {
      this.Max = _param2;
      this.Min = _param1;
    }
    else
    {
      this.Min = _param1;
      this.Max = _param2;
    }
  }

  public IRange \u0023\u003DzJIqIiUw\u003D(
    IRange _param1)
  {
    return (IRange) this.\u0023\u003DzJIqIiUw\u003D((IRange<T>) _param1);
  }

  public IRange \u0023\u003DzJIqIiUw\u003D(
    IRange _param1,
    \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D _param2)
  {
    IRange abyLt9clZggmJsWhw = (IRange) null;
    switch (_param2)
    {
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.MinMax:
        abyLt9clZggmJsWhw = _param1;
        break;
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.Max:
        T zH9Hnkng1 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003DzAGURk2c\u003D<T>();
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, (IComparable) zH9Hnkng1, _param1.Max);
        break;
      case \u0023\u003Dz7oKBks6ccXdMBOl\u0024qXdcQBfb77y0xl0\u00246w\u003D\u003D.Min:
        T zH9Hnkng2 = \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNXCE4EvjzL\u0024mX84Druo\u003D.\u0023\u003Dz9S54PDM\u003D<T>();
        abyLt9clZggmJsWhw = \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003Dzo7udA0u6sNJJ(_param1, _param1.Min, (IComparable) zH9Hnkng2);
        break;
    }
    return (IRange) this.\u0023\u003DzJIqIiUw\u003D((IRange<T>) abyLt9clZggmJsWhw);
  }

  public IRange \u0023\u003DzeiifnZI\u003D(
    IRange _param1)
  {
    return (IRange) this.\u0023\u003DzeiifnZI\u003D((IRange<T>) _param1);
  }

  public bool \u0023\u003DzU0feMzXFLecQ(IComparable _param1)
  {
    return this.Min.CompareTo((object) _param1) <= 0 && this.Max.CompareTo((object) _param1) >= 0;
  }

  public IRange<T> \u0023\u003DzeiifnZI\u003D(
    IRange<T> _param1)
  {
    return (IRange<T>) \u0023\u003DzS_cHGzr_lHDzMznjWZ1hrDlB0n65RlWCGw\u003D\u003D.\u0023\u003DzLc65\u0024pc\u003D((IComparable) Range<T>.\u0023\u003DzB8wkVW0YXGlP.Min(this.Min, _param1.Min), (IComparable) Range<T>.\u0023\u003DzB8wkVW0YXGlP.Max(this.Max, _param1.Max));
  }

  IRange IRange.\u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3L844WUagxCAVomufc\u003D(
    double _param1,
    double _param2)
  {
    return (IRange) this.SetMinMax(_param1, _param2);
  }

  IRange IRange.\u0023\u003Dz3RRntx4pzkd854dIVpLK6aPvdl8ZapW2OeSTMYm_K6Gu(
    double _param1,
    double _param2,
    IRange _param3)
  {
    return (IRange) this.SetMinMax(_param1, _param2, (IRange<T>) _param3);
  }

  IRange IRange.\u0023\u003DzpTBWTwmpvpgHkLhFsQhfVp2o1afiKe2D_7xBFPY\u003D(
    double _param1,
    double _param2)
  {
    return (IRange) this.GrowBy(_param1, _param2);
  }

  public override string ToString() => $"{this.GetType()} {{Min={this.Min}, Max={this.Max}}}";

  public override int GetHashCode()
  {
    T zH9Hnkng = this.Min;
    int num = zH9Hnkng.GetHashCode() * 397;
    zH9Hnkng = this.Max;
    int hashCode = zH9Hnkng.GetHashCode();
    return num ^ hashCode;
  }

  public override bool Equals(object _param1)
  {
    return _param1 is IRange<T> && this.\u0023\u003DzhxbsSqM\u003D((IRange<T>) _param1);
  }

  public bool \u0023\u003DzhxbsSqM\u003D(
    IRange<T> _param1)
  {
    if (_param1 == null)
      return false;
    if (this == _param1)
      return true;
    T zH9Hnkng = _param1.Min;
    if (!zH9Hnkng.Equals((object) this.Min))
      return false;
    zH9Hnkng = _param1.Max;
    return zH9Hnkng.Equals((object) this.Max);
  }

  internal void \u0023\u003DzIECuo1rstuxex\u0024WBruVMWlw\u003D()
  {
    \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D.\u0023\u003DzlTskcr4\u003D((IComparable) this.Min, "Min").\u0023\u003DziXfpgk1YpfgIxrtqTA\u003D\u003D((IComparable) this.Max, "Max");
  }
}
