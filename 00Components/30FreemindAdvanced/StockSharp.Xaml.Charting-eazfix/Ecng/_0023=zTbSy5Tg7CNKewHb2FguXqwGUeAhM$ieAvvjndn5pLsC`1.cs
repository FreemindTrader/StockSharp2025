// Decompiled with JetBrains decompiler
// Type: #=zTbSy5Tg7CNKewHb2FguXqwGUeAhM$ieAvvjndn5pLsCI
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<T> : 
  \u0023\u003DzN_ef\u0024eTD0bISWSKkIKedSvLP\u0024zDbYxtEhpMKleCtJGtGqo7ZPw\u003D\u003D<T>,
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IEnumerable
{
  
  private T[] \u0023\u003DzkELV\u0024GPsC83d;
  
  private int \u0023\u003Dz0EJ0DjSA4vke = -1;
  
  private readonly int \u0023\u003DzJpbCbio\u003D;
  
  private int \u0023\u003DzdJFp_7vu05Ks;

  public \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI(int _param1)
  {
    this.\u0023\u003DzJpbCbio\u003D = _param1;
    this.\u0023\u003DzkELV\u0024GPsC83d = new T[_param1];
  }

  public int \u0023\u003Dz2lXru4I\u003D() => this.\u0023\u003DzJpbCbio\u003D;

  public int Count => this.\u0023\u003DzdJFp_7vu05Ks;

  public void \u0023\u003DzpFWgSog\u003D(int _param1) => this.\u0023\u003DzdJFp_7vu05Ks = _param1;

  public T Add(T _param1)
  {
    this.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003DzpxjKVGQ\u003D()] = _param1;
    this.\u0023\u003DzdJFp_7vu05Ks = Math.Min(this.\u0023\u003DzdJFp_7vu05Ks + 1, this.\u0023\u003DzJpbCbio\u003D);
    return _param1;
  }

  public T \u0023\u003DzxKsmolQ\u003D()
  {
    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dzj_Gd1fY\u003D<T>(this.\u0023\u003DzkELV\u0024GPsC83d, 0, this.\u0023\u003DzdJFp_7vu05Ks);
  }

  public T \u0023\u003DzC52X0FE\u003D()
  {
    return \u0023\u003DzE2B_RS0KvtqHnw_gRshK2XNMFe_3Evlr8vIKUruTJaum.\u0023\u003Dz2wWd_ME\u003D<T>(this.\u0023\u003DzkELV\u0024GPsC83d, 0, this.\u0023\u003DzdJFp_7vu05Ks);
  }

  public void \u0023\u003Dz6_E5\u0024pE\u003D(IEnumerable<T> _param1)
  {
    int num1 = this.\u0023\u003DzpxjKVGQ\u003D();
    T[] zkElvGpsC83d = this.\u0023\u003DzkELV\u0024GPsC83d;
    int num2 = this.\u0023\u003Dz2lXru4I\u003D() - num1;
    switch (_param1)
    {
      case Array array2:
        this.\u0023\u003DzPePCW2yxAGKh(num1, array2, array2.Length, num2, zkElvGpsC83d);
        break;
      case IList<T> zH9HnkngList:
        T[] zH9HnkngArray = zH9HnkngList.\u0023\u003Dz1bvQV4SZTWpA<T>();
        this.\u0023\u003DzPePCW2yxAGKh(num1, (Array) zH9HnkngArray, zH9HnkngList.Count, num2, zkElvGpsC83d);
        break;
      default:
        T[] array1 = _param1.ToArray<T>();
        this.\u0023\u003DzPePCW2yxAGKh(num1, (Array) array1, array1.Length, num2, zkElvGpsC83d);
        break;
    }
  }

  public void \u0023\u003DzdG5UbJ7rAsgF(
    int _param1,
    IEnumerable<T> _param2)
  {
    throw new NotSupportedException("Insert is not a supported operation on a Fifo Buffer");
  }

  public void \u0023\u003DzdDirImQ\u003D(int _param1, int _param2)
  {
    throw new NotSupportedException("Remove is not a supported operation on a Fifo Buffer");
  }

  private void \u0023\u003DzPePCW2yxAGKh(
    int _param1,
    Array _param2,
    int _param3,
    int _param4,
    T[] _param5)
  {
    if (_param3 > this.\u0023\u003Dz2lXru4I\u003D())
    {
      Array.Copy(_param2, _param3 - this.\u0023\u003Dz2lXru4I\u003D(), (Array) this.\u0023\u003DzkELV\u0024GPsC83d, 0, this.\u0023\u003Dz2lXru4I\u003D());
      this.\u0023\u003Dz0EJ0DjSA4vke = -1;
      this.\u0023\u003DzdJFp_7vu05Ks = this.\u0023\u003Dz2lXru4I\u003D();
    }
    else if (_param3 < _param4)
    {
      Array.Copy(_param2, 0, (Array) _param5, _param1, _param3);
      this.\u0023\u003DzdJFp_7vu05Ks = Math.Min(this.\u0023\u003DzdJFp_7vu05Ks + _param3, this.\u0023\u003DzJpbCbio\u003D);
      this.\u0023\u003Dz0EJ0DjSA4vke = _param1 + _param3 - 1;
    }
    else
    {
      int length1 = _param4;
      int length2 = _param3 - _param4;
      Array.Copy(_param2, 0, (Array) this.\u0023\u003DzkELV\u0024GPsC83d, _param1, length1);
      Array.Copy(_param2, _param4, (Array) this.\u0023\u003DzkELV\u0024GPsC83d, 0, length2);
      this.\u0023\u003DzdJFp_7vu05Ks = Math.Min(this.\u0023\u003DzdJFp_7vu05Ks + _param3, this.\u0023\u003DzJpbCbio\u003D);
      this.\u0023\u003Dz0EJ0DjSA4vke = length2 - 1;
    }
  }

  public int IndexOf(T _param1)
  {
    int num = Array.IndexOf<T>(this.\u0023\u003DzkELV\u0024GPsC83d, _param1, 0, this.\u0023\u003DzJpbCbio\u003D);
    return num == -1 ? -1 : this.\u0023\u003DzfjN0veBoVRBI(num);
  }

  public void Insert(int _param1, T _param2)
  {
    throw new NotSupportedException("Insert is not a supported operation on a Fifo Buffer");
  }

  public void RemoveAt(int _param1)
  {
    throw new NotSupportedException("Remove is not a supported operation on a Fifo Buffer");
  }

  T IList<T>.\u0023\u003Dz0AHuzVTfq\u0024d4zWGWl4gMSpIlkWpf(
    int _param1)
  {
    return this.\u0023\u003DzyBK4kJ0B\u0024FFU(_param1);
  }

  void IList<T>.\u0023\u003Dz_vb79EtlArV3ZdNVeByl4tQWV3O8(
    int _param1,
    T _param2)
  {
    this.\u0023\u003DzwDCWXBFSmFxV(_param1, _param2);
  }

  
  [IndexerName("#=zMRIb09I=")]
  public T this[int _param1]
  {
    get => this.\u0023\u003DzyBK4kJ0B\u0024FFU(_param1);
  }

  public T \u0023\u003Dz\u0024CeUvME\u003D(int _param1)
  {
    return this.\u0023\u003DzyBK4kJ0B\u0024FFU(_param1);
  }

  private int \u0023\u003DzpxjKVGQ\u003D()
  {
    if (this.\u0023\u003Dz0EJ0DjSA4vke < 0 && this.\u0023\u003DzdJFp_7vu05Ks != this.\u0023\u003Dz2lXru4I\u003D())
      return this.\u0023\u003DzdJFp_7vu05Ks;
    this.\u0023\u003Dz0EJ0DjSA4vke = (this.\u0023\u003Dz0EJ0DjSA4vke + 1) % this.\u0023\u003Dz2lXru4I\u003D();
    if (this.\u0023\u003Dz0EJ0DjSA4vke > this.\u0023\u003DzdJFp_7vu05Ks)
      this.\u0023\u003Dz0EJ0DjSA4vke = this.\u0023\u003DzdJFp_7vu05Ks;
    return this.\u0023\u003Dz0EJ0DjSA4vke;
  }

  public int \u0023\u003DzfjN0veBoVRBI(int _param1)
  {
    return this.\u0023\u003Dz0EJ0DjSA4vke < 0 ? _param1 : (_param1 - this.\u0023\u003Dz0EJ0DjSA4vke + this.\u0023\u003DzdJFp_7vu05Ks - 1) % this.\u0023\u003DzdJFp_7vu05Ks;
  }

  public int \u0023\u003Dzh4Not7Y\u003D(int _param1)
  {
    return this.\u0023\u003Dz0EJ0DjSA4vke < 0 ? _param1 : (this.\u0023\u003Dz0EJ0DjSA4vke + 1 + _param1) % this.\u0023\u003DzdJFp_7vu05Ks;
  }

  private void \u0023\u003DzMxu8pns\u003D(int _param1)
  {
    if (_param1 < 0 || _param1 >= this.\u0023\u003DzdJFp_7vu05Ks)
      throw new IndexOutOfRangeException();
  }

  void ICollection<T>.\u0023\u003DzGBPAZl7kD4Y_019f_JdDMabqLLf_(
    T _param1)
  {
    this.Add(_param1);
  }

  public void Clear()
  {
    this.\u0023\u003DzkELV\u0024GPsC83d = new T[this.\u0023\u003DzJpbCbio\u003D];
    this.\u0023\u003Dz0EJ0DjSA4vke = 0;
    this.\u0023\u003DzdJFp_7vu05Ks = 0;
  }

  public bool Contains(T _param1)
  {
    return Array.IndexOf<T>(this.\u0023\u003DzkELV\u0024GPsC83d, _param1, 0, this.\u0023\u003DzJpbCbio\u003D) != -1;
  }

  public void Clone(T[] _param1, int _param2)
  {
    int sourceIndex = this.\u0023\u003Dz0EJ0DjSA4vke + 1;
    int length1 = _param1.Length - sourceIndex;
    Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, sourceIndex, (Array) _param1, _param2, length1);
    int length2 = _param1.Length - length1;
    if (length2 <= 0)
      return;
    Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, 0, (Array) _param1, length1 + _param2, length2);
  }

  public void Clone(
    int _param1,
    T[] _param2,
    int _param3,
    int _param4)
  {
    int sourceIndex = this.\u0023\u003Dz0EJ0DjSA4vke + 1 + _param1;
    if (sourceIndex < _param2.Length)
    {
      int length1 = Math.Min(_param2.Length - sourceIndex, _param4);
      Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, sourceIndex, (Array) _param2, _param3, length1);
      int length2 = _param4 - length1;
      if (length2 <= 0)
        return;
      Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, 0, (Array) _param2, _param3 + length1, length2);
    }
    else
      Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, sourceIndex - _param2.Length, (Array) _param2, _param3, _param4);
  }

  public bool IsReadOnly => false;

  public bool Remove(T _param1)
  {
    int destinationIndex = Array.IndexOf<T>(this.\u0023\u003DzkELV\u0024GPsC83d, _param1, 0, this.\u0023\u003DzJpbCbio\u003D);
    if (destinationIndex < 0)
      return false;
    --this.\u0023\u003DzdJFp_7vu05Ks;
    if (destinationIndex < this.\u0023\u003Dz2lXru4I\u003D() - 1)
      Array.Copy((Array) this.\u0023\u003DzkELV\u0024GPsC83d, destinationIndex + 1, (Array) this.\u0023\u003DzkELV\u0024GPsC83d, destinationIndex, this.\u0023\u003Dz2lXru4I\u003D() - destinationIndex - 1);
    this.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003DzdJFp_7vu05Ks] = default (T);
    if (destinationIndex <= this.\u0023\u003Dz0EJ0DjSA4vke)
      --this.\u0023\u003Dz0EJ0DjSA4vke;
    return true;
  }

  public T \u0023\u003DzyBK4kJ0B\u0024FFU(int _param1)
  {
    this.\u0023\u003DzMxu8pns\u003D(_param1);
    return this.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003Dzh4Not7Y\u003D(_param1)];
  }

  public void \u0023\u003DzwDCWXBFSmFxV(int _param1, T _param2)
  {
    this.\u0023\u003DzMxu8pns\u003D(_param1);
    this.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003Dzh4Not7Y\u003D(_param1)] = _param2;
  }

  public IEnumerator<T> GetEnumerator()
  {
    return (IEnumerator<T>) new \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<T>.\u0023\u003Dzo9CreWbON5RL5AeMow\u003D\u003D(0)
    {
      _variableSome3535 = this
    };
  }

  IEnumerator IEnumerable.\u0023\u003DzSV_TZe7ftMh2SQ86i417Nok\u003D()
  {
    return (IEnumerator) this.GetEnumerator();
  }

  public int \u0023\u003Dz8rmtah183uns() => this.\u0023\u003Dz0EJ0DjSA4vke;

  public void \u0023\u003Dz0MUs9D0LKoXa(int _param1) => this.\u0023\u003Dz0EJ0DjSA4vke = _param1;

  [SpecialName]
  public T[] \u0023\u003DzRr4AYdnHaTxa()
  {
    return this.\u0023\u003DzkELV\u0024GPsC83d;
  }

  public void \u0023\u003Dze68j\u0024gs\u003D(int _param1)
  {
    throw new InvalidOperationException("SetCount is not valid on Circular-Buffer list types");
  }

  private sealed class \u0023\u003Dzo9CreWbON5RL5AeMow\u003D\u003D : 
    IEnumerator<T>,
    IDisposable,
    IEnumerator
  {
    
    private int \u0023\u003Dz4fzyEZ1SsHYa;
    
    private T \u0023\u003Dzaev1bhaFFIDX;
    
    public \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<T> _variableSome3535;
    
    private int \u0023\u003DzWloIRY4kF_0P;

    [DebuggerHidden]
    public \u0023\u003Dzo9CreWbON5RL5AeMow\u003D\u003D(int _param1)
    {
      this.\u0023\u003Dz4fzyEZ1SsHYa = _param1;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
      int z4fzyEz1SsHya = this.\u0023\u003Dz4fzyEZ1SsHYa;
      \u0023\u003DzTbSy5Tg7CNKewHb2FguXqwGUeAhM\u0024ieAvvjndn5pLsCI<T> zRrvwDu67s9Rm = this._variableSome3535;
      switch (z4fzyEz1SsHya)
      {
        case 0:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          if (zRrvwDu67s9Rm.\u0023\u003DzdJFp_7vu05Ks == 0)
            return false;
          this.\u0023\u003DzWloIRY4kF_0P = zRrvwDu67s9Rm.\u0023\u003Dz0EJ0DjSA4vke + 1;
          break;
        case 1:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          ++this.\u0023\u003DzWloIRY4kF_0P;
          break;
        case 2:
          this.\u0023\u003Dz4fzyEZ1SsHYa = -1;
          ++this.\u0023\u003DzWloIRY4kF_0P;
          goto label_11;
        default:
          return false;
      }
      if (this.\u0023\u003DzWloIRY4kF_0P < zRrvwDu67s9Rm.\u0023\u003DzdJFp_7vu05Ks)
      {
        this.\u0023\u003Dzaev1bhaFFIDX = zRrvwDu67s9Rm.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003DzWloIRY4kF_0P];
        this.\u0023\u003Dz4fzyEZ1SsHYa = 1;
        return true;
      }
      this.\u0023\u003DzWloIRY4kF_0P = 0;
label_11:
      if (this.\u0023\u003DzWloIRY4kF_0P > zRrvwDu67s9Rm.\u0023\u003Dz0EJ0DjSA4vke)
        return false;
      this.\u0023\u003Dzaev1bhaFFIDX = zRrvwDu67s9Rm.\u0023\u003DzkELV\u0024GPsC83d[this.\u0023\u003DzWloIRY4kF_0P];
      this.\u0023\u003Dz4fzyEZ1SsHYa = 2;
      return true;
    }

    [DebuggerHidden]
    T IEnumerator<T>.\u0023\u003DzFPFGeOP\u0024gB895G_WoTnUqerbuUD3()
    {
      return this.\u0023\u003Dzaev1bhaFFIDX;
    }

    [DebuggerHidden]
    void IEnumerator.\u0023\u003Dz__yDkd4DQlAhNe9vxQ\u003D\u003D()
    {
      throw new NotSupportedException();
    }

    [DebuggerHidden]
    object IEnumerator.\u0023\u003DzmTTmbxIPszmU9qSLsSEbSqM\u003D()
    {
      return (object) this.\u0023\u003Dzaev1bhaFFIDX;
    }
  }
}
