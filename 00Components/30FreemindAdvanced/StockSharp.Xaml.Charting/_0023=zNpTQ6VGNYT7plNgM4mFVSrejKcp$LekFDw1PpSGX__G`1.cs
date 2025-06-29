// Decompiled with JetBrains decompiler
// Type: #=zNpTQ6VGNYT7plNgM4mFVSrejKcp$LekFDw1PpSGX__GL
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

#nullable disable
internal sealed class \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D> : 
  \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003DzH9HNkng\u003D>,
  IList<\u0023\u003DzH9HNkng\u003D>,
  ICollection<\u0023\u003DzH9HNkng\u003D>,
  IEnumerable<\u0023\u003DzH9HNkng\u003D>,
  IEnumerable,
  IList,
  ICollection
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private static readonly \u0023\u003DzH9HNkng\u003D[] \u0023\u003DzUVmXdYNPmc0z = Array.Empty<\u0023\u003DzH9HNkng\u003D>();
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private \u0023\u003DzH9HNkng\u003D[] \u0023\u003Dzg0gWX4E\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003DzJpbCbio\u003D;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private object \u0023\u003DzxztcSMfDuTst;
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private int \u0023\u003Dzq9NR138\u003D;

  public \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL()
    : this(128 /*0x80*/)
  {
  }

  public \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL(int _param1)
  {
    this.\u0023\u003Dzg0gWX4E\u003D = _param1 >= 0 ? new \u0023\u003DzH9HNkng\u003D[_param1] : throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436535));
  }

  public \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL(
    IEnumerable<\u0023\u003DzH9HNkng\u003D> _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436542));
    if (_param1 is ICollection<\u0023\u003DzH9HNkng\u003D> zH9Hnkngs)
    {
      int count = zH9Hnkngs.Count;
      this.\u0023\u003Dzg0gWX4E\u003D = new \u0023\u003DzH9HNkng\u003D[count];
      zH9Hnkngs.CopyTo(this.\u0023\u003Dzg0gWX4E\u003D, 0);
      this.\u0023\u003DzJpbCbio\u003D = count;
    }
    else
    {
      this.\u0023\u003DzJpbCbio\u003D = 0;
      this.\u0023\u003Dzg0gWX4E\u003D = new \u0023\u003DzH9HNkng\u003D[128 /*0x80*/];
      foreach (\u0023\u003DzH9HNkng\u003D zH9Hnkng in _param1)
        this.Add(zH9Hnkng);
    }
  }

  public static \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz19UvNq0\u003D(
    \u0023\u003DzH9HNkng\u003D[] _param0)
  {
    return new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>()
    {
      \u0023\u003Dzg0gWX4E\u003D = _param0,
      \u0023\u003DzJpbCbio\u003D = _param0.Length
    };
  }

  [SpecialName]
  public \u0023\u003DzH9HNkng\u003D[] \u0023\u003DzRr4AYdnHaTxa()
  {
    return this.\u0023\u003Dzg0gWX4E\u003D;
  }

  internal int \u0023\u003DzMRd_64p8E249() => this.\u0023\u003Dzg0gWX4E\u003D.Length;

  internal void \u0023\u003Dz2GOzd49sS99C(int _param1)
  {
    if (_param1 < this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436521));
    if (_param1 == this.\u0023\u003Dzg0gWX4E\u003D.Length)
      return;
    if (_param1 > 0)
    {
      \u0023\u003DzH9HNkng\u003D[] destinationArray = new \u0023\u003DzH9HNkng\u003D[_param1];
      if (this.\u0023\u003DzJpbCbio\u003D > 0)
        Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, 0, (Array) destinationArray, 0, this.\u0023\u003DzJpbCbio\u003D);
      this.\u0023\u003Dzg0gWX4E\u003D = destinationArray;
    }
    else
      this.\u0023\u003Dzg0gWX4E\u003D = \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzUVmXdYNPmc0z;
  }

  bool IList.\u0023\u003DzVtB9Td1HClEhKFH\u0024dUP8Pw0\u003D() => false;

  bool IList.\u0023\u003DzTy9YbO8PCIjJtzE8Uz_BYx8\u003D() => false;

  bool ICollection.\u0023\u003DzU\u0024MaLspaVIlPZAquKstGPb4\u003D() => false;

  object ICollection.\u0023\u003DzxSE4OX0h3nsK6ev9wi6AL7I\u003D()
  {
    if (this.\u0023\u003DzxztcSMfDuTst == null)
      Interlocked.CompareExchange<object>(ref this.\u0023\u003DzxztcSMfDuTst, new object(), (object) null);
    return this.\u0023\u003DzxztcSMfDuTst;
  }

  object IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1) => (object) this[_param1];

  void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object _param2)
  {
    try
    {
      this[_param1] = (\u0023\u003DzH9HNkng\u003D) _param2;
    }
    catch (InvalidCastException ex)
    {
      throw new ArgumentException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436312), (object) _param2.GetType().Name));
    }
  }

  int IList.\u0023\u003Dz9l8NFg1zgw0y\u0024EjyXw\u003D\u003D(object _param1)
  {
    if (_param1 == null)
      throw new ArgumentNullException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436323));
    try
    {
      this.Add((\u0023\u003DzH9HNkng\u003D) _param1);
    }
    catch (InvalidCastException ex)
    {
      throw new ArgumentException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436312), (object) _param1.GetType().Name));
    }
    return this.Count - 1;
  }

  [SecuritySafeCritical]
  bool IList.\u0023\u003Dz07_U1xKJVCxa7bIf\u0024A\u003D\u003D(object _param1)
  {
    return \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzHPu9XX3E93EL(_param1) && this.Contains((\u0023\u003DzH9HNkng\u003D) _param1);
  }

  void ICollection.\u0023\u003DzFAvqYrLd8QaQMLuvPOiGwFQ\u003D(Array _param1, int _param2)
  {
    if (_param1 != null && _param1.Rank != 1)
      throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436330), \u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436361));
    try
    {
      Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, 0, _param1, _param2, this.\u0023\u003DzJpbCbio\u003D);
    }
    catch (ArrayTypeMismatchException ex)
    {
      throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436410));
    }
  }

  int IList.\u0023\u003DzRqsurumTDWAgVqHVtg\u003D\u003D(object _param1)
  {
    return \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzHPu9XX3E93EL(_param1) ? this.IndexOf((\u0023\u003DzH9HNkng\u003D) _param1) : -1;
  }

  void IList.\u0023\u003DzuxPIbwR6tOrj8Wpe9w\u003D\u003D(int _param1, object _param2)
  {
    try
    {
      this.Insert(_param1, (\u0023\u003DzH9HNkng\u003D) _param2);
    }
    catch (InvalidCastException ex)
    {
      throw new ArgumentException(string.Format(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436312), (object) _param2.GetType().Name));
    }
  }

  [SecuritySafeCritical]
  void IList.\u0023\u003Dzx\u0024PvHCNWSNwq55LIBQ\u003D\u003D(object _param1)
  {
    if (!\u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzHPu9XX3E93EL(_param1))
      return;
    this.Remove((\u0023\u003DzH9HNkng\u003D) _param1);
  }

  public int Count => this.\u0023\u003DzJpbCbio\u003D;

  internal void \u0023\u003DzpFWgSog\u003D(int _param1)
  {
    this.\u0023\u003DzJpbCbio\u003D = _param1;
  }

  bool ICollection<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzUq2KM6jl3UCUrxQEdcWSy1EsGjzi\u0024m061w\u003D\u003D()
  {
    return false;
  }

  public \u0023\u003DzH9HNkng\u003D this[int _param1]
  {
    get
    {
      return _param1 >= this.\u0023\u003DzJpbCbio\u003D ? default (\u0023\u003DzH9HNkng\u003D) : this.\u0023\u003Dzg0gWX4E\u003D[_param1];
    }
    set
    {
      if (_param1 >= this.\u0023\u003DzJpbCbio\u003D)
        throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437719));
      this.\u0023\u003Dzg0gWX4E\u003D[_param1] = value;
      ++this.\u0023\u003Dzq9NR138\u003D;
    }
  }

  public void Add(\u0023\u003DzH9HNkng\u003D _param1)
  {
    if (this.\u0023\u003DzJpbCbio\u003D == this.\u0023\u003Dzg0gWX4E\u003D.Length)
      this.\u0023\u003DzflETr9POPm\u0024J(this.\u0023\u003DzJpbCbio\u003D + 1);
    this.\u0023\u003Dzg0gWX4E\u003D[this.\u0023\u003DzJpbCbio\u003D++] = _param1;
    ++this.\u0023\u003Dzq9NR138\u003D;
  }

  public void Clear()
  {
    if (this.\u0023\u003DzJpbCbio\u003D > 0)
    {
      Array.Clear((Array) this.\u0023\u003Dzg0gWX4E\u003D, 0, this.\u0023\u003DzJpbCbio\u003D);
      this.\u0023\u003DzJpbCbio\u003D = 0;
    }
    ++this.\u0023\u003Dzq9NR138\u003D;
  }

  public bool Contains(\u0023\u003DzH9HNkng\u003D _param1)
  {
    if ((object) _param1 == null)
    {
      for (int index = 0; index < this.\u0023\u003DzJpbCbio\u003D; ++index)
      {
        if ((object) this.\u0023\u003Dzg0gWX4E\u003D[index] == null)
          return true;
      }
      return false;
    }
    EqualityComparer<\u0023\u003DzH9HNkng\u003D> equalityComparer = EqualityComparer<\u0023\u003DzH9HNkng\u003D>.Default;
    for (int index = 0; index < this.\u0023\u003DzJpbCbio\u003D; ++index)
    {
      if (equalityComparer.Equals(this.\u0023\u003Dzg0gWX4E\u003D[index], _param1))
        return true;
    }
    return false;
  }

  public void CopyTo(\u0023\u003DzH9HNkng\u003D[] _param1, int _param2)
  {
    Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, 0, (Array) _param1, _param2, this.\u0023\u003DzJpbCbio\u003D);
  }

  IEnumerator<\u0023\u003DzH9HNkng\u003D> IEnumerable<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
  {
    return (IEnumerator<\u0023\u003DzH9HNkng\u003D>) new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzdFhhG7w\u003D(this);
  }

  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    return (IEnumerator) new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzdFhhG7w\u003D(this);
  }

  public int IndexOf(\u0023\u003DzH9HNkng\u003D _param1)
  {
    return Array.IndexOf<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, _param1, 0, this.\u0023\u003DzJpbCbio\u003D);
  }

  public void Insert(int _param1, \u0023\u003DzH9HNkng\u003D _param2)
  {
    if (_param1 > this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437726));
    if (this.\u0023\u003DzJpbCbio\u003D == this.\u0023\u003Dzg0gWX4E\u003D.Length)
      this.\u0023\u003DzflETr9POPm\u0024J(this.\u0023\u003DzJpbCbio\u003D + 1);
    if (_param1 < this.\u0023\u003DzJpbCbio\u003D)
      Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1 + 1, this.\u0023\u003DzJpbCbio\u003D - _param1);
    this.\u0023\u003Dzg0gWX4E\u003D[_param1] = _param2;
    ++this.\u0023\u003DzJpbCbio\u003D;
    ++this.\u0023\u003Dzq9NR138\u003D;
  }

  public bool Remove(\u0023\u003DzH9HNkng\u003D _param1)
  {
    int num = this.IndexOf(_param1);
    if (num < 0)
      return false;
    this.RemoveAt(num);
    return true;
  }

  public void RemoveAt(int _param1)
  {
    if (_param1 >= this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437705));
    --this.\u0023\u003DzJpbCbio\u003D;
    if (_param1 < this.\u0023\u003DzJpbCbio\u003D)
      Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1 + 1, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, this.\u0023\u003DzJpbCbio\u003D - _param1);
    this.\u0023\u003Dzg0gWX4E\u003D[this.\u0023\u003DzJpbCbio\u003D] = default (\u0023\u003DzH9HNkng\u003D);
    ++this.\u0023\u003Dzq9NR138\u003D;
  }

  private static bool \u0023\u003DzHPu9XX3E93EL(object _param0)
  {
    if (_param0 is \u0023\u003DzH9HNkng\u003D)
      return true;
    return _param0 == null && (object) default (\u0023\u003DzH9HNkng\u003D) == null;
  }

  public \u0023\u003DzH9HNkng\u003D \u0023\u003DzxKsmolQ\u003D()
  {
    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, 0, this.Count);
  }

  public \u0023\u003DzH9HNkng\u003D \u0023\u003DzC52X0FE\u003D()
  {
    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, 0, this.Count);
  }

  public void \u0023\u003Dz6_E5\u0024pE\u003D(IEnumerable<\u0023\u003DzH9HNkng\u003D> _param1)
  {
    this.\u0023\u003DzdG5UbJ7rAsgF(this.\u0023\u003DzJpbCbio\u003D, _param1);
  }

  public \u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dzq2zVESXBEvJM()
  {
    return (\u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<\u0023\u003DzH9HNkng\u003D>) new \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ\u003D\u003D<\u0023\u003DzH9HNkng\u003D>(this);
  }

  public void \u0023\u003Dz3MbNd8U\u003D(\u0023\u003DzH9HNkng\u003D[] _param1)
  {
    this.CopyTo(_param1, 0);
  }

  public void \u0023\u003Dz3MbNd8U\u003D(
    int _param1,
    \u0023\u003DzH9HNkng\u003D[] _param2,
    int _param3,
    int _param4)
  {
    if (this.\u0023\u003DzJpbCbio\u003D - _param1 < _param4)
      throw new ArgumentException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437752));
    Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, (Array) _param2, _param3, _param4);
  }

  public bool \u0023\u003Dz8_bgWJ3JomKk(int _param1)
  {
    if (this.Count >= _param1)
      return false;
    this.\u0023\u003DzflETr9POPm\u0024J(_param1);
    this.\u0023\u003DzpFWgSog\u003D(_param1);
    return true;
  }

  private void \u0023\u003DzflETr9POPm\u0024J(int _param1)
  {
    if (this.\u0023\u003Dzg0gWX4E\u003D.Length >= _param1)
      return;
    int num = this.\u0023\u003Dzg0gWX4E\u003D.Length == 0 ? 4 : this.\u0023\u003Dzg0gWX4E\u003D.Length * 2;
    if (num < _param1)
      num = _param1;
    this.\u0023\u003Dz2GOzd49sS99C(num);
  }

  public \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzdFhhG7w\u003D \u0023\u003DzRPOJ5g0\u003D()
  {
    return new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzdFhhG7w\u003D(this);
  }

  public int \u0023\u003DzttdLFAU\u003D(\u0023\u003DzH9HNkng\u003D _param1, int _param2)
  {
    if (_param2 > this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437783));
    return Array.IndexOf<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, _param1, _param2, this.\u0023\u003DzJpbCbio\u003D - _param2);
  }

  public int \u0023\u003DzttdLFAU\u003D(
    \u0023\u003DzH9HNkng\u003D _param1,
    int _param2,
    int _param3)
  {
    if (_param2 > this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437790));
    if (_param3 < 0 || _param2 > this.\u0023\u003DzJpbCbio\u003D - _param3)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437769));
    return Array.IndexOf<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, _param1, _param2, _param3);
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<\u0023\u003DzH9HNkng\u003D> _param2)
  {
    if (_param2 == null)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437816));
    if (_param1 > this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437795));
    switch (_param2)
    {
      case Array sourceArray2:
        int length1 = sourceArray2.Length;
        this.\u0023\u003DzflETr9POPm\u0024J(this.\u0023\u003DzJpbCbio\u003D + length1);
        Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1 + length1, this.\u0023\u003DzJpbCbio\u003D - _param1);
        Array.Copy(sourceArray2, 0, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, length1);
        this.\u0023\u003DzJpbCbio\u003D += length1;
        break;
      case IList<\u0023\u003DzH9HNkng\u003D> zH9HnkngList:
        int count = zH9HnkngList.Count;
        \u0023\u003DzH9HNkng\u003D[] sourceArray1 = zH9HnkngList.\u0023\u003Dz1bvQV4SZTWpA<\u0023\u003DzH9HNkng\u003D>();
        this.\u0023\u003DzflETr9POPm\u0024J(this.\u0023\u003DzJpbCbio\u003D + count);
        Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1 + count, this.\u0023\u003DzJpbCbio\u003D - _param1);
        \u0023\u003DzH9HNkng\u003D[] zg0gWx4E = this.\u0023\u003Dzg0gWX4E\u003D;
        int destinationIndex = _param1;
        int length2 = count;
        Array.Copy((Array) sourceArray1, 0, (Array) zg0gWx4E, destinationIndex, length2);
        this.\u0023\u003DzJpbCbio\u003D += count;
        ++this.\u0023\u003Dzq9NR138\u003D;
        break;
      default:
        using (IEnumerator<\u0023\u003DzH9HNkng\u003D> enumerator = _param2.GetEnumerator())
        {
          int length3 = this.\u0023\u003DzJpbCbio\u003D - _param1;
          \u0023\u003DzH9HNkng\u003D[] zH9HnkngArray = new \u0023\u003DzH9HNkng\u003D[length3];
          Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, (Array) zH9HnkngArray, 0, length3);
          while (enumerator.MoveNext())
          {
            this.\u0023\u003DzflETr9POPm\u0024J(this.\u0023\u003DzJpbCbio\u003D + 1);
            this.\u0023\u003Dzg0gWX4E\u003D[_param1] = enumerator.Current;
            ++_param1;
            ++this.\u0023\u003DzJpbCbio\u003D;
          }
          Array.Copy((Array) zH9HnkngArray, 0, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, length3);
        }
        ++this.\u0023\u003Dzq9NR138\u003D;
        ++this.\u0023\u003Dzq9NR138\u003D;
        break;
    }
  }

  public int \u0023\u003DzLi0LBQXEUVc9(\u0023\u003DzH9HNkng\u003D _param1)
  {
    return this.\u0023\u003DzJpbCbio\u003D == 0 ? -1 : this.\u0023\u003DzLi0LBQXEUVc9(_param1, this.\u0023\u003DzJpbCbio\u003D - 1, this.\u0023\u003DzJpbCbio\u003D);
  }

  public int \u0023\u003DzLi0LBQXEUVc9(\u0023\u003DzH9HNkng\u003D _param1, int _param2)
  {
    return _param2 < this.\u0023\u003DzJpbCbio\u003D ? this.\u0023\u003DzLi0LBQXEUVc9(_param1, _param2, _param2 + 1) : throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437802));
  }

  public int \u0023\u003DzLi0LBQXEUVc9(
    \u0023\u003DzH9HNkng\u003D _param1,
    int _param2,
    int _param3)
  {
    if (this.Count != 0 && _param2 < 0)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437589));
    if (this.Count != 0 && _param3 < 0)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433921));
    if (this.\u0023\u003DzJpbCbio\u003D == 0)
      return -1;
    if (_param2 >= this.\u0023\u003DzJpbCbio\u003D)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437572));
    if (_param3 > _param2 + 1)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539433921));
    return Array.LastIndexOf<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003Dzg0gWX4E\u003D, _param1, _param2, _param3);
  }

  public void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    if (_param1 < 0)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437583));
    if (_param2 < 0)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437622));
    if (this.\u0023\u003DzJpbCbio\u003D - _param1 < _param2)
      throw new ArgumentOutOfRangeException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539437601));
    if (_param2 <= 0)
      return;
    this.\u0023\u003DzJpbCbio\u003D -= _param2;
    if (_param1 < this.\u0023\u003DzJpbCbio\u003D)
      Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1 + _param2, (Array) this.\u0023\u003Dzg0gWX4E\u003D, _param1, this.\u0023\u003DzJpbCbio\u003D - _param1);
    Array.Clear((Array) this.\u0023\u003Dzg0gWX4E\u003D, this.\u0023\u003DzJpbCbio\u003D, _param2);
    ++this.\u0023\u003Dzq9NR138\u003D;
  }

  public \u0023\u003DzH9HNkng\u003D[] \u0023\u003DzSWlLd4k\u003D()
  {
    \u0023\u003DzH9HNkng\u003D[] destinationArray = new \u0023\u003DzH9HNkng\u003D[this.\u0023\u003DzJpbCbio\u003D];
    Array.Copy((Array) this.\u0023\u003Dzg0gWX4E\u003D, 0, (Array) destinationArray, 0, this.\u0023\u003DzJpbCbio\u003D);
    return destinationArray;
  }

  public void \u0023\u003DzFqwmAtQ6h18qSpWcIw\u003D\u003D()
  {
    if (this.\u0023\u003DzJpbCbio\u003D >= (int) ((double) this.\u0023\u003Dzg0gWX4E\u003D.Length * 0.9))
      return;
    this.\u0023\u003Dz2GOzd49sS99C(this.\u0023\u003DzJpbCbio\u003D);
  }

  internal static IList<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz\u00247WdPho\u003D(
    List<\u0023\u003DzH9HNkng\u003D> _param0)
  {
    return (IList<\u0023\u003DzH9HNkng\u003D>) new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzcHSqfbQ\u003D(_param0);
  }

  public void \u0023\u003Dze68j\u0024gs\u003D(int _param1)
  {
    this.\u0023\u003DzJpbCbio\u003D = _param1;
  }

  internal sealed class \u0023\u003DzcHSqfbQ\u003D : 
    IList<\u0023\u003DzH9HNkng\u003D>,
    ICollection<\u0023\u003DzH9HNkng\u003D>,
    IEnumerable<\u0023\u003DzH9HNkng\u003D>,
    IEnumerable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly List<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz5IntIgc\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly object \u0023\u003Dz6j0cxXE\u003D;

    internal \u0023\u003DzcHSqfbQ\u003D(List<\u0023\u003DzH9HNkng\u003D> _param1)
    {
      this.\u0023\u003Dz5IntIgc\u003D = _param1;
      this.\u0023\u003Dz6j0cxXE\u003D = ((ICollection) _param1).SyncRoot;
    }

    public int Count
    {
      get
      {
        lock (this.\u0023\u003Dz6j0cxXE\u003D)
          return this.\u0023\u003Dz5IntIgc\u003D.Count;
      }
    }

    public bool IsReadOnly
    {
      get => ((ICollection<\u0023\u003DzH9HNkng\u003D>) this.\u0023\u003Dz5IntIgc\u003D).IsReadOnly;
    }

    public \u0023\u003DzH9HNkng\u003D this[int _param1]
    {
      get
      {
        lock (this.\u0023\u003Dz6j0cxXE\u003D)
          return this.\u0023\u003Dz5IntIgc\u003D[_param1];
      }
      set
      {
        lock (this.\u0023\u003Dz6j0cxXE\u003D)
          this.\u0023\u003Dz5IntIgc\u003D[_param1] = value;
      }
    }

    public void Add(\u0023\u003DzH9HNkng\u003D _param1)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        this.\u0023\u003Dz5IntIgc\u003D.Add(_param1);
    }

    public void Clear()
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        this.\u0023\u003Dz5IntIgc\u003D.Clear();
    }

    public bool Contains(\u0023\u003DzH9HNkng\u003D _param1)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        return this.\u0023\u003Dz5IntIgc\u003D.Contains(_param1);
    }

    public void CopyTo(\u0023\u003DzH9HNkng\u003D[] _param1, int _param2)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        this.\u0023\u003Dz5IntIgc\u003D.CopyTo(_param1, _param2);
    }

    public bool Remove(\u0023\u003DzH9HNkng\u003D _param1)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        return this.\u0023\u003Dz5IntIgc\u003D.Remove(_param1);
    }

    IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        return (IEnumerator) this.\u0023\u003Dz5IntIgc\u003D.GetEnumerator();
    }

    IEnumerator<\u0023\u003DzH9HNkng\u003D> IEnumerable<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        return ((IEnumerable<\u0023\u003DzH9HNkng\u003D>) this.\u0023\u003Dz5IntIgc\u003D).GetEnumerator();
    }

    public int IndexOf(\u0023\u003DzH9HNkng\u003D _param1)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        return this.\u0023\u003Dz5IntIgc\u003D.IndexOf(_param1);
    }

    public void Insert(int _param1, \u0023\u003DzH9HNkng\u003D _param2)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        this.\u0023\u003Dz5IntIgc\u003D.Insert(_param1, _param2);
    }

    public void RemoveAt(int _param1)
    {
      lock (this.\u0023\u003Dz6j0cxXE\u003D)
        this.\u0023\u003Dz5IntIgc\u003D.RemoveAt(_param1);
    }
  }

  public struct \u0023\u003DzdFhhG7w\u003D : 
    IEnumerator<\u0023\u003DzH9HNkng\u003D>,
    IEnumerator,
    IDisposable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz5IntIgc\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int \u0023\u003Dzq9NR138\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private \u0023\u003DzH9HNkng\u003D \u0023\u003DzCUQ2vA0\u003D;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int \u0023\u003DzKx97DYo\u003D;

    internal \u0023\u003DzdFhhG7w\u003D(
      \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D> _param1)
    {
      this.\u0023\u003Dz5IntIgc\u003D = _param1;
      this.\u0023\u003DzKx97DYo\u003D = 0;
      this.\u0023\u003Dzq9NR138\u003D = _param1.\u0023\u003Dzq9NR138\u003D;
      this.\u0023\u003DzCUQ2vA0\u003D = default (\u0023\u003DzH9HNkng\u003D);
    }

    public \u0023\u003DzH9HNkng\u003D Current => this.\u0023\u003DzCUQ2vA0\u003D;

    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      if (this.\u0023\u003DzKx97DYo\u003D == 0 || this.\u0023\u003DzKx97DYo\u003D == this.\u0023\u003Dz5IntIgc\u003D.\u0023\u003DzJpbCbio\u003D + 1)
        throw new InvalidOperationException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436431));
      return (object) this.Current;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D> z5IntIgc = this.\u0023\u003Dz5IntIgc\u003D;
      if (this.\u0023\u003Dzq9NR138\u003D != z5IntIgc.\u0023\u003Dzq9NR138\u003D || this.\u0023\u003DzKx97DYo\u003D >= z5IntIgc.\u0023\u003DzJpbCbio\u003D)
        return this.\u0023\u003Dz6UGGlohE7E5N();
      this.\u0023\u003DzCUQ2vA0\u003D = z5IntIgc.\u0023\u003Dzg0gWX4E\u003D[this.\u0023\u003DzKx97DYo\u003D];
      ++this.\u0023\u003DzKx97DYo\u003D;
      return true;
    }

    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      if (this.\u0023\u003Dzq9NR138\u003D != this.\u0023\u003Dz5IntIgc\u003D.\u0023\u003Dzq9NR138\u003D)
        throw new InvalidOperationException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436499));
      this.\u0023\u003DzKx97DYo\u003D = 0;
      this.\u0023\u003DzCUQ2vA0\u003D = default (\u0023\u003DzH9HNkng\u003D);
    }

    private bool \u0023\u003Dz6UGGlohE7E5N()
    {
      if (this.\u0023\u003Dzq9NR138\u003D != this.\u0023\u003Dz5IntIgc\u003D.\u0023\u003Dzq9NR138\u003D)
        throw new InvalidOperationException(\u0023\u003DzlTriv\u0024izV_y2_zQvsgEHkxJr2Ncz.\u0023\u003DzhQ0l2sE\u003D(-539436499));
      this.\u0023\u003DzKx97DYo\u003D = this.\u0023\u003Dz5IntIgc\u003D.\u0023\u003DzJpbCbio\u003D + 1;
      this.\u0023\u003DzCUQ2vA0\u003D = default (\u0023\u003DzH9HNkng\u003D);
      return false;
    }
  }
}
