// Decompiled with JetBrains decompiler
// Type: #=zq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Runtime.CompilerServices;

#nullable disable
internal class \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003Dz2n1QiX8\u003D> : 
  \u0023\u003Dza9eQbgAsftIGbI_4wdfcZFY6vYc_HYGEdLlgutNQnK4AkZG\u0024zeIU4wmt7QA82wUrXA\u003D\u003D<\u0023\u003Dz2n1QiX8\u003D>
{
  protected int \u0023\u003DzGupBQuw\u003D;
  private \u0023\u003Dz2n1QiX8\u003D[] \u0023\u003Dz\u0024XVulME\u003D = Array.Empty<\u0023\u003Dz2n1QiX8\u003D>();
  private static \u0023\u003Dz2n1QiX8\u003D \u0023\u003DzUqWKHXd4M9e_ksrX3Q\u003D\u003D;

  public \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D()
  {
  }

  public \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D(
    int _param1)
    : this(_param1, 0)
  {
  }

  public \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D(
    int _param1,
    int _param2)
  {
    this.\u0023\u003DzWIEZ7Zw\u003D(_param1, _param2);
  }

  public \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D(
    \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003Dz2n1QiX8\u003D> _param1)
  {
    this.\u0023\u003DzGupBQuw\u003D = _param1.\u0023\u003DzGupBQuw\u003D;
    this.\u0023\u003Dz\u0024XVulME\u003D = (\u0023\u003Dz2n1QiX8\u003D[]) _param1.\u0023\u003Dz\u0024XVulME\u003D.Clone();
  }

  public int \u0023\u003Dz\u00243MChJsNHkjY() => this.\u0023\u003Dz\u0024XVulME\u003D.Length;

  public virtual void \u0023\u003Dz\u0024abmkXc\u003D(int _param1)
  {
    if (_param1 >= this.\u0023\u003DzxhbmvAVxpXvh())
      throw new Exception("");
    for (int index = _param1; index < this.\u0023\u003DzxhbmvAVxpXvh() - 1; ++index)
      this.\u0023\u003Dz\u0024XVulME\u003D[index] = this.\u0023\u003Dz\u0024XVulME\u003D[index + 1];
    --this.\u0023\u003DzGupBQuw\u003D;
  }

  public virtual void \u0023\u003Dz8ClqfHs\u003D()
  {
    if (this.\u0023\u003DzGupBQuw\u003D == 0)
      return;
    --this.\u0023\u003DzGupBQuw\u003D;
  }

  public void \u0023\u003DzCadMMgc\u003D(
    \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003Dz2n1QiX8\u003D> _param1)
  {
    this.\u0023\u003DzWIEZ7Zw\u003D(_param1.\u0023\u003DzGupBQuw\u003D);
    if (_param1.\u0023\u003DzGupBQuw\u003D == 0)
      return;
    _param1.\u0023\u003Dz\u0024XVulME\u003D.CopyTo((Array) this.\u0023\u003Dz\u0024XVulME\u003D, 0);
  }

  public void \u0023\u003Dz0WRhgbw\u003D(int _param1)
  {
    this.\u0023\u003Dz0WRhgbw\u003D(_param1, 0);
  }

  public void \u0023\u003Dz0WRhgbw\u003D(int _param1, int _param2)
  {
    this.\u0023\u003DzGupBQuw\u003D = 0;
    if (_param1 <= this.\u0023\u003Dz\u00243MChJsNHkjY())
      return;
    this.\u0023\u003Dz\u0024XVulME\u003D = (\u0023\u003Dz2n1QiX8\u003D[]) null;
    int length = _param1 + _param2;
    if (length == 0)
      return;
    this.\u0023\u003Dz\u0024XVulME\u003D = new \u0023\u003Dz2n1QiX8\u003D[length];
  }

  public int \u0023\u003Dz0WRhgbw\u003D() => this.\u0023\u003Dz\u00243MChJsNHkjY();

  public void \u0023\u003DzWIEZ7Zw\u003D(int _param1)
  {
    this.\u0023\u003DzWIEZ7Zw\u003D(_param1, 0);
  }

  public void \u0023\u003DzWIEZ7Zw\u003D(int _param1, int _param2)
  {
    this.\u0023\u003Dz0WRhgbw\u003D(_param1, _param2);
    this.\u0023\u003DzGupBQuw\u003D = _param1;
  }

  public void \u0023\u003Dz7FKHKl8\u003D(int _param1)
  {
    if (_param1 <= this.\u0023\u003DzGupBQuw\u003D || _param1 <= this.\u0023\u003Dz\u00243MChJsNHkjY())
      return;
    \u0023\u003Dz2n1QiX8\u003D[] z2n1QiX8Array = new \u0023\u003Dz2n1QiX8\u003D[_param1];
    if (this.\u0023\u003Dz\u0024XVulME\u003D != null)
    {
      for (int index = 0; index < this.\u0023\u003Dz\u0024XVulME\u003D.Length; ++index)
        z2n1QiX8Array[index] = this.\u0023\u003Dz\u0024XVulME\u003D[index];
    }
    this.\u0023\u003Dz\u0024XVulME\u003D = z2n1QiX8Array;
  }

  public void \u0023\u003DzSdOfhoo\u003D()
  {
    int length = this.\u0023\u003Dz\u0024XVulME\u003D.Length;
    for (int index = 0; index < length; ++index)
      this.\u0023\u003Dz\u0024XVulME\u003D[index] = \u0023\u003Dzq8s_Zceh9qBcjYPACJ3nRNsBClCqagy0OvjeEBllPZvRaPkvRhhJrlp42uAvcBcBfw\u003D\u003D<\u0023\u003Dz2n1QiX8\u003D>.\u0023\u003DzUqWKHXd4M9e_ksrX3Q\u003D\u003D;
  }

  public virtual void \u0023\u003DzObQSsmE\u003D(\u0023\u003Dz2n1QiX8\u003D _param1)
  {
    if (this.\u0023\u003Dz\u0024XVulME\u003D == null || this.\u0023\u003Dz\u0024XVulME\u003D.Length < this.\u0023\u003DzGupBQuw\u003D + 1)
      this.\u0023\u003Dz7FKHKl8\u003D(this.\u0023\u003DzGupBQuw\u003D + this.\u0023\u003DzGupBQuw\u003D / 2 + 16 /*0x10*/);
    this.\u0023\u003Dz\u0024XVulME\u003D[this.\u0023\u003DzGupBQuw\u003D++] = _param1;
  }

  public void \u0023\u003DzUfZs3UqXVMe2(\u0023\u003Dz2n1QiX8\u003D _param1)
  {
    this.\u0023\u003Dz\u0024XVulME\u003D[this.\u0023\u003DzGupBQuw\u003D++] = _param1;
  }

  public void \u0023\u003DzsuAlMvRpKtcF(int _param1, \u0023\u003Dz2n1QiX8\u003D _param2)
  {
    if (_param1 >= this.\u0023\u003DzGupBQuw\u003D)
    {
      this.\u0023\u003Dz\u0024XVulME\u003D[this.\u0023\u003DzGupBQuw\u003D] = _param2;
    }
    else
    {
      for (int index = 0; index < this.\u0023\u003DzGupBQuw\u003D - _param1; ++index)
        this.\u0023\u003Dz\u0024XVulME\u003D[index + _param1 + 1] = this.\u0023\u003Dz\u0024XVulME\u003D[index + _param1];
      this.\u0023\u003Dz\u0024XVulME\u003D[_param1] = _param2;
    }
    ++this.\u0023\u003DzGupBQuw\u003D;
  }

  public void \u0023\u003DzCN7ksgcCYW8D(int _param1) => this.\u0023\u003DzGupBQuw\u003D += _param1;

  public int \u0023\u003DzG2qqjnQ\u003D() => this.\u0023\u003DzGupBQuw\u003D;

  [IndexerName("#=zMRIb09I=")]
  public \u0023\u003Dz2n1QiX8\u003D this[int _param1]
  {
    get => this.\u0023\u003Dz\u0024XVulME\u003D[_param1];
  }

  [SpecialName]
  public \u0023\u003Dz2n1QiX8\u003D[] \u0023\u003DzvsnCYl4\u003D()
  {
    return this.\u0023\u003Dz\u0024XVulME\u003D;
  }

  public \u0023\u003Dz2n1QiX8\u003D \u0023\u003Dz8XhvksE\u003D(int _param1)
  {
    return this.\u0023\u003Dz\u0024XVulME\u003D[_param1];
  }

  public \u0023\u003Dz2n1QiX8\u003D \u0023\u003DzUJWtgeFETgKR(int _param1)
  {
    return this.\u0023\u003Dz\u0024XVulME\u003D[_param1];
  }

  public \u0023\u003Dz2n1QiX8\u003D[] \u0023\u003DzrnGNvGY\u003D()
  {
    return this.\u0023\u003Dz\u0024XVulME\u003D;
  }

  public void \u0023\u003Dzh3TDr7jbiclU() => this.\u0023\u003DzGupBQuw\u003D = 0;

  public void \u0023\u003DzS3VDevc\u003D() => this.\u0023\u003DzGupBQuw\u003D = 0;

  public void \u0023\u003DzSvHatfjPzS\u0024J(int _param1)
  {
    if (_param1 >= this.\u0023\u003DzGupBQuw\u003D)
      return;
    this.\u0023\u003DzGupBQuw\u003D = _param1;
  }

  public int \u0023\u003DzxhbmvAVxpXvh() => this.\u0023\u003DzGupBQuw\u003D;
}
