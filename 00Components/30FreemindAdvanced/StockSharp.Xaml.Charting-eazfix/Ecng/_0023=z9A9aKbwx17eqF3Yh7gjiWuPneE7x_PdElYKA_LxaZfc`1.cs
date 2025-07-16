// Decompiled with JetBrains decompiler
// Type: #=z9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
public sealed class \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ<T> : 
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IEnumerable
{
  
  private T[] \u0023\u003Dz0GgNoqs\u003D;
  
  private int \u0023\u003Dz_tkZHEs\u003D;
  
  private int \u0023\u003DzpxhY2Co\u003D;

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ(
    T[] _param1,
    int _param2,
    int _param3)
  {
    this.\u0023\u003Dz0GgNoqs\u003D = _param1;
    this.\u0023\u003Dz_tkZHEs\u003D = _param2;
    this.\u0023\u003DzpxhY2Co\u003D = _param3;
  }

  public \u0023\u003Dz9A9aKbwx17eqF3Yh7gjiWuPneE7x_PdElYKA_LxaZfcJ(
    T[] _param1)
  {
    this.\u0023\u003Dz0GgNoqs\u003D = _param1;
    this.\u0023\u003DzpxhY2Co\u003D = _param1.Length;
  }

  public T[] \u0023\u003DzvsnCYl4\u003D()
  {
    return this.\u0023\u003Dz0GgNoqs\u003D;
  }

  public int \u0023\u003DzOhT86Emh4umk() => this.\u0023\u003Dz_tkZHEs\u003D;

  public int Count => this.\u0023\u003DzpxhY2Co\u003D;

  public int IndexOf(T _param1) => throw new NotImplementedException();

  public void Insert(int _param1, T _param2)
  {
    throw new NotSupportedException();
  }

  public void RemoveAt(int _param1) => throw new NotSupportedException();

  public T this[int _param1]
  {
    get => this.\u0023\u003DzvsnCYl4\u003D()[_param1 + this.\u0023\u003DzOhT86Emh4umk()];
    set => throw new NotImplementedException();
  }

  public void Add(T _param1) => throw new NotSupportedException();

  public void Clear() => throw new NotSupportedException();

  public bool Contains(T _param1) => throw new NotImplementedException();

  public void Clone(T[] _param1, int _param2)
  {
    throw new NotImplementedException();
  }

  public bool IsReadOnly => true;

  public bool Remove(T _param1) => throw new NotSupportedException();

  public IEnumerator<T> GetEnumerator()
  {
    throw new NotImplementedException();
  }

  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    throw new NotImplementedException();
  }
}
