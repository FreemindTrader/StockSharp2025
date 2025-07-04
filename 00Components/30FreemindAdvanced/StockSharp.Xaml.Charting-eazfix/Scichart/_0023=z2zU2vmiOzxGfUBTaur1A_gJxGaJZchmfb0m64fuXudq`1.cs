// Decompiled with JetBrains decompiler
// Type: #=z2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ\u003D\u003D<T> : 
  \u0023\u003Dzro0Io1hfSw7LlH634iIk6DImkX90fd6hXMUYrBvYe4GoWtElsg\u003D\u003D<T>,
  \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<T>,
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IList,
  IEnumerable,
  ICollection
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly AbstractList<T> _parentElement;

  public \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ\u003D\u003D(
    AbstractList<T> _param1)
  {
    this._parentElement = _param1;
  }

  public \u0023\u003Dz2zU2vmiOzxGfUBTaur1A_gJxGaJZchmfb0m64fuXudqUOLrxxQ\u003D\u003D(
    T[] _param1)
  {
    this._parentElement = AbstractList<T>.\u0023\u003Dz19UvNq0\u003D(_param1);
  }

  private void \u0023\u003DzrjoFHxcyHAZx()
  {
    throw new InvalidOperationException("this list is read-only");
  }

  [SpecialName]
  public T[] \u0023\u003DzRr4AYdnHaTxa()
  {
    return this._parentElement.\u0023\u003DzRr4AYdnHaTxa();
  }

  internal int \u0023\u003DzMRd_64p8E249()
  {
    return this._parentElement.\u0023\u003DzMRd_64p8E249();
  }

  internal void \u0023\u003Dz2GOzd49sS99C(int _param1) => this.\u0023\u003DzrjoFHxcyHAZx();

  bool IList.\u0023\u003DzVtB9Td1HClEhKFH\u0024dUP8Pw0\u003D() => false;

  bool IList.\u0023\u003DzTy9YbO8PCIjJtzE8Uz_BYx8\u003D() => true;

  bool ICollection.\u0023\u003DzU\u0024MaLspaVIlPZAquKstGPb4\u003D() => false;

  object ICollection.\u0023\u003DzxSE4OX0h3nsK6ev9wi6AL7I\u003D()
  {
    return ((ICollection) this._parentElement).SyncRoot;
  }

  object IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1)
  {
    return (object) this._parentElement[_param1];
  }

  void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object _param2)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  int IList.\u0023\u003Dz9l8NFg1zgw0y\u0024EjyXw\u003D\u003D(object _param1)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
    return 0;
  }

  bool IList.\u0023\u003Dz07_U1xKJVCxa7bIf\u0024A\u003D\u003D(object _param1)
  {
    return ((IList) this._parentElement).Contains(_param1);
  }

  void ICollection.\u0023\u003DzFAvqYrLd8QaQMLuvPOiGwFQ\u003D(Array _param1, int _param2)
  {
    ((ICollection) this._parentElement).CopyTo(_param1, _param2);
  }

  int IList.\u0023\u003DzRqsurumTDWAgVqHVtg\u003D\u003D(object _param1)
  {
    return ((IList) this._parentElement).IndexOf(_param1);
  }

  void IList.\u0023\u003DzuxPIbwR6tOrj8Wpe9w\u003D\u003D(int _param1, object _param2)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  void IList.\u0023\u003Dzx\u0024PvHCNWSNwq55LIBQ\u003D\u003D(object _param1)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public int Count => this._parentElement.Count;

  internal void \u0023\u003DzpFWgSog\u003D(int _param1) => this.\u0023\u003DzrjoFHxcyHAZx();

  bool ICollection<T>.\u0023\u003DzUq2KM6jl3UCUrxQEdcWSy1EsGjzi\u0024m061w\u003D\u003D()
  {
    return true;
  }

  public T this[int _param1]
  {
    get => this._parentElement[_param1];
    set => this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public void Add(T _param1) => this.\u0023\u003DzrjoFHxcyHAZx();

  public void Clear() => this.\u0023\u003DzrjoFHxcyHAZx();

  public bool Contains(T _param1)
  {
    return this._parentElement.Contains(_param1);
  }

  public void CopyTo(T[] _param1, int _param2)
  {
    this._parentElement.CopyTo(_param1, _param2);
  }

  IEnumerator<T> IEnumerable<T>.\u0023\u003DzeZA2Ff9OcH6BD7PGVCI6viR78aQH()
  {
    return (IEnumerator<T>) this._parentElement.\u0023\u003DzRPOJ5g0\u003D();
  }

  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    return (IEnumerator) this._parentElement.\u0023\u003DzRPOJ5g0\u003D();
  }

  public int IndexOf(T _param1)
  {
    return this._parentElement.IndexOf(_param1);
  }

  public void Insert(int _param1, T _param2)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public bool Remove(T _param1)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
    return false;
  }

  public void RemoveAt(int _param1) => this.\u0023\u003DzrjoFHxcyHAZx();

  public T \u0023\u003DzxKsmolQ\u003D()
  {
    return this._parentElement.\u0023\u003DzxKsmolQ\u003D();
  }

  public T \u0023\u003DzC52X0FE\u003D()
  {
    return this._parentElement.\u0023\u003DzC52X0FE\u003D();
  }

  public void \u0023\u003Dz6_E5\u0024pE\u003D(IEnumerable<T> _param1)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public void CopyTo(T[] _param1)
  {
    this._parentElement.CopyTo(_param1);
  }

  public void CopyTo(
    int _param1,
    T[] _param2,
    int _param3,
    int _param4)
  {
    this._parentElement.CopyTo(_param1, _param2, _param3, _param4);
  }

  public bool \u0023\u003Dz8_bgWJ3JomKk(int _param1)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
    return false;
  }

  public int \u0023\u003DzttdLFAU\u003D(T _param1, int _param2)
  {
    return this._parentElement.\u0023\u003DzttdLFAU\u003D(_param1, _param2);
  }

  public int \u0023\u003DzttdLFAU\u003D(
    T _param1,
    int _param2,
    int _param3)
  {
    return this._parentElement.\u0023\u003DzttdLFAU\u003D(_param1, _param2, _param3);
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<T> _param2)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public int \u0023\u003DzLi0LBQXEUVc9(T _param1)
  {
    return this._parentElement.\u0023\u003DzLi0LBQXEUVc9(_param1);
  }

  public int \u0023\u003DzLi0LBQXEUVc9(T _param1, int _param2)
  {
    return this._parentElement.\u0023\u003DzLi0LBQXEUVc9(_param1, _param2);
  }

  public int \u0023\u003DzLi0LBQXEUVc9(
    T _param1,
    int _param2,
    int _param3)
  {
    return this._parentElement.\u0023\u003DzLi0LBQXEUVc9(_param1, _param2, _param3);
  }

  public void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    this.\u0023\u003DzrjoFHxcyHAZx();
  }

  public T[] \u0023\u003DzSWlLd4k\u003D()
  {
    return this._parentElement.\u0023\u003DzSWlLd4k\u003D();
  }

  public void \u0023\u003DzFqwmAtQ6h18qSpWcIw\u003D\u003D() => this.\u0023\u003DzrjoFHxcyHAZx();

  public void \u0023\u003Dze68j\u0024gs\u003D(int _param1) => this.\u0023\u003DzrjoFHxcyHAZx();
}
