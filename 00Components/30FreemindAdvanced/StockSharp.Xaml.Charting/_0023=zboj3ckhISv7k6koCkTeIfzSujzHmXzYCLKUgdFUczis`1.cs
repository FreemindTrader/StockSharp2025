// Decompiled with JetBrains decompiler
// Type: #=zboj3ckhISv7k6koCkTeIfzSujzHmXzYCLKUgdFUczis$
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
internal abstract class \u0023\u003Dzboj3ckhISv7k6koCkTeIfzSujzHmXzYCLKUgdFUczis\u0024<\u0023\u003DzH9HNkng\u003D> : 
  \u0023\u003Dz5tgJOvSsgsmn_0Qv_7eNQxgil7IMIZzTVOZnJ0QEWy6u<\u0023\u003DzH9HNkng\u003D>,
  IList<\u0023\u003DzH9HNkng\u003D>,
  ICollection<\u0023\u003DzH9HNkng\u003D>,
  IEnumerable<\u0023\u003DzH9HNkng\u003D>,
  IEnumerable,
  IList,
  ICollection,
  \u0023\u003DzJhc8WdlQgSkcniY\u0024669ans2mQMwz_VJH0HVFEk8\u003D
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003DzH9HNkng\u003D> \u0023\u003DzkELV\u0024GPsC83d = (\u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<\u0023\u003DzH9HNkng\u003D>) new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSrejKcp\u0024LekFDw1PpSGX__GL<\u0023\u003DzH9HNkng\u003D>(128 /*0x80*/);

  public virtual \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<\u0023\u003DzH9HNkng\u003D> \u0023\u003Dz1bvQV4SZTWpA(
    int _param1,
    int _param2)
  {
    int num1 = _param1 > this.Count ? this.Count : _param1;
    int num2 = Math.Min(this.Count - num1, _param2);
    return new \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<\u0023\u003DzH9HNkng\u003D>(this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzRr4AYdnHaTxa(), num1, num2);
  }

  public \u0023\u003DzH9HNkng\u003D[] \u0023\u003DzCQWqAOc\u003D()
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzRr4AYdnHaTxa();
  }

  public void Add(\u0023\u003DzH9HNkng\u003D _param1)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.Add(_param1);
  }

  public int Add(object _param1) => ((IList) this.\u0023\u003DzkELV\u0024GPsC83d).Add(_param1);

  public bool Contains(object _param1)
  {
    return ((IList) this.\u0023\u003DzkELV\u0024GPsC83d).Contains(_param1);
  }

  public void Clear() => this.\u0023\u003DzkELV\u0024GPsC83d.Clear();

  public int IndexOf(object _param1)
  {
    return this.Count == 0 ? -1 : this.IndexOf((\u0023\u003DzH9HNkng\u003D) Convert.ChangeType(_param1, typeof (\u0023\u003DzH9HNkng\u003D), (IFormatProvider) null));
  }

  public void Insert(int _param1, object _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.Insert(_param1, (\u0023\u003DzH9HNkng\u003D) _param2);
  }

  public void Remove(object _param1)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.Remove((\u0023\u003DzH9HNkng\u003D) _param1);
  }

  public bool Contains(\u0023\u003DzH9HNkng\u003D _param1)
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.Contains(_param1);
  }

  public void CopyTo(\u0023\u003DzH9HNkng\u003D[] _param1, int _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.CopyTo(_param1, _param2);
  }

  bool ICollection<\u0023\u003DzH9HNkng\u003D>.\u0023\u003DzSNMbA4NBIHO6wT8LM2lfHp2KTKyC(
    \u0023\u003DzH9HNkng\u003D _param1)
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.Remove(_param1);
  }

  public \u0023\u003DzH9HNkng\u003D \u0023\u003DzC52X0FE\u003D()
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzC52X0FE\u003D();
  }

  public \u0023\u003DzH9HNkng\u003D \u0023\u003DzxKsmolQ\u003D()
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzxKsmolQ\u003D();
  }

  public void \u0023\u003Dz6_E5\u0024pE\u003D(IEnumerable<\u0023\u003DzH9HNkng\u003D> _param1)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003Dz6_E5\u0024pE\u003D(_param1);
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<\u0023\u003DzH9HNkng\u003D> _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzdG5UbJ7rAsgF(_param1, _param2);
  }

  public void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.\u0023\u003DzdDirImQ\u003D(_param1, _param2);
  }

  public void \u0023\u003Dz\u0024abmkXc\u003D(\u0023\u003DzH9HNkng\u003D _param1)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.Remove(_param1);
  }

  public int IndexOf(\u0023\u003DzH9HNkng\u003D _param1)
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.IndexOf(_param1);
  }

  public void Insert(int _param1, \u0023\u003DzH9HNkng\u003D _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d.Insert(_param1, _param2);
  }

  public void RemoveAt(int _param1) => this.\u0023\u003DzkELV\u0024GPsC83d.RemoveAt(_param1);

  object IList.\u0023\u003Dzsw6uZQAY38X4SXUVUM6sxbU\u003D(int _param1)
  {
    return (object) this.\u0023\u003DzkELV\u0024GPsC83d[_param1];
  }

  void IList.\u0023\u003DzPS8zWbReapv0MVOGfNSMdFU\u003D(int _param1, object _param2)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d[_param1] = (\u0023\u003DzH9HNkng\u003D) _param2;
  }

  public \u0023\u003DzH9HNkng\u003D this[int _param1]
  {
    get => this.\u0023\u003DzkELV\u0024GPsC83d[_param1];
    set => this.\u0023\u003DzkELV\u0024GPsC83d[_param1] = value;
  }

  public void CopyTo(Array _param1, int _param2)
  {
    ((ICollection) this.\u0023\u003DzkELV\u0024GPsC83d).CopyTo(_param1, _param2);
  }

  public int Count => this.\u0023\u003DzkELV\u0024GPsC83d.Count;

  public object SyncRoot => (object) this.\u0023\u003DzkELV\u0024GPsC83d;

  public bool IsSynchronized => false;

  public bool IsReadOnly => false;

  public bool IsFixedSize => false;

  [SpecialName]
  public bool \u0023\u003Dz5vfl0A4nWD8Q() => this.\u0023\u003DzkELV\u0024GPsC83d.Count != 0;

  public IEnumerator<\u0023\u003DzH9HNkng\u003D> GetEnumerator()
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d.GetEnumerator();
  }

  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    return (IEnumerator) this.GetEnumerator();
  }
}
