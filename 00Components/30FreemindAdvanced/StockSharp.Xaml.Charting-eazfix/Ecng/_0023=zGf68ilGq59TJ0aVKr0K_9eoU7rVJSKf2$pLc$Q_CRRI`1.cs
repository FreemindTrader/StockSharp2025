// Decompiled with JetBrains decompiler
// Type: #=zGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2$pLc$Q_CRRI0B0xDoi_RvpM0a$y$HqY7Xw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
public sealed class \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D<T> : 
  \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK4AkZG\u0024zeIU4wmt7QA82wUrXA\u003D\u003D<T>
{
  private T[] \u0023\u003DzhUUq_d4kT69A;
  private int \u0023\u003DzE8qDEoWQ1KEt;

  public \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D()
    : this(64 /*0x40*/)
  {
  }

  public \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D(
    int _param1)
  {
    this.\u0023\u003DzhUUq_d4kT69A = new T[_param1];
    this.\u0023\u003DzE8qDEoWQ1KEt = _param1;
  }

  public \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D(
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9eoU7rVJSKf2\u0024pLc\u0024Q_CRRI0B0xDoi_RvpM0a\u0024y\u0024HqY7Xw\u003D\u003D<T> _param1)
  {
    this.\u0023\u003DzhUUq_d4kT69A = (T[]) _param1.\u0023\u003DzhUUq_d4kT69A.Clone();
    this.\u0023\u003DzE8qDEoWQ1KEt = _param1.\u0023\u003DzE8qDEoWQ1KEt;
  }

  public void \u0023\u003Dz8ClqfHs\u003D() => throw new NotImplementedException();

  public void \u0023\u003Dz7FKHKl8\u003D(int _param1)
  {
    if (_param1 == this.\u0023\u003DzE8qDEoWQ1KEt)
      return;
    this.\u0023\u003DzhUUq_d4kT69A = new T[_param1];
  }

  public int \u0023\u003DzdTxNrgQ\u003D() => this.\u0023\u003DzE8qDEoWQ1KEt;

  [IndexerName("#=zMRIb09I=")]
  public T this[int _param1]
  {
    get => this.\u0023\u003DzhUUq_d4kT69A[_param1];
    set => this.\u0023\u003DzhUUq_d4kT69A[_param1] = value;
  }

  public T \u0023\u003Dz\u0024CeUvME\u003D(int _param1)
  {
    return this.\u0023\u003DzhUUq_d4kT69A[_param1];
  }

  public void \u0023\u003DzS9gpfR4\u003D(int _param1, T _param2)
  {
    this.\u0023\u003DzhUUq_d4kT69A[_param1] = _param2;
  }

  [SpecialName]
  public T[] \u0023\u003DzvsnCYl4\u003D()
  {
    return this.\u0023\u003DzhUUq_d4kT69A;
  }
}
