// Decompiled with JetBrains decompiler
// Type: #=zVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid$7DvZjvNcM0JGxzNylJSrzd9csvZOKBkbvSNaARA=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
public sealed class \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0JGxzNylJSrzd9csvZOKBkbvSNaARA\u003D : 
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSS28mTU1\u0024kh32C_bmlBdAw\u0024tw\u003D\u003D
{
  private \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D \u0023\u003Dzq_GYkpSu5Wgn;
  private uint \u0023\u003DzfFySSudz\u0024SOy;
  private uint \u0023\u003Dzosch78aT\u00245mK;
  public static int \u0023\u003DzC5aH6eUup5RPeQILWA\u003D\u003D = 8;
  public static int \u0023\u003DzYp3S3uuOMppS6WJotA\u003D\u003D = 0;
  public static int \u0023\u003Dz68hARzzl638oLDKc3Q\u003D\u003D = (int) byte.MaxValue;

  public \u0023\u003DzVZAnYWMfoaQCzNrFMqw3u3L4wr8nsid\u00247DvZjvNcM0JGxzNylJSrzd9csvZOKBkbvSNaARA\u003D(
    \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D _param1,
    uint _param2,
    uint _param3)
  {
    this.\u0023\u003DzfFySSudz\u0024SOy = _param2;
    this.\u0023\u003Dzosch78aT\u00245mK = _param3;
    this.\u0023\u003Dzq_GYkpSu5Wgn = _param1;
  }

  public void \u0023\u003DzotQWOIc\u003D(
    \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D _param1)
  {
    this.\u0023\u003Dzq_GYkpSu5Wgn = _param1;
  }

  public byte \u0023\u003Dz\u00246e75ZE\u003D(int _param1, int _param2)
  {
    if ((uint) _param1 >= (uint) this.\u0023\u003Dzq_GYkpSu5Wgn.Width || (uint) _param2 >= (uint) this.\u0023\u003Dzq_GYkpSu5Wgn.Height)
      return 0;
    int index = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    return this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D()[index];
  }

  public byte \u0023\u003DzScHVJa9MMOhSof4pGw\u003D\u003D(int _param1, int _param2, byte _param3)
  {
    if ((uint) _param1 >= (uint) this.\u0023\u003Dzq_GYkpSu5Wgn.Width || (uint) _param2 >= (uint) this.\u0023\u003Dzq_GYkpSu5Wgn.Height)
      return 0;
    int index = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    byte[] numArray = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
    return (byte) ((int) _param3 * (int) numArray[index] + (int) byte.MaxValue >> 8);
  }

  public void \u0023\u003Dzw2m5o9nBTZi4\u0024stKlA\u003D\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003DzK6WagmL6YRAHetmvn\u0024bY3pE4uVhRF9dz8w\u003D\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    int num1 = this.\u0023\u003Dzq_GYkpSu5Wgn.Width - 1;
    int num2 = this.\u0023\u003Dzq_GYkpSu5Wgn.Height - 1;
    int num3 = _param5;
    if (_param2 < 0 || _param2 > num2)
    {
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
    }
    else
    {
      if (_param1 < 0)
      {
        num3 += _param1;
        if (num3 <= 0)
        {
          \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
          return;
        }
        \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, -_param1);
        _param4 -= _param1;
        _param1 = 0;
      }
      if (_param1 + num3 > num1)
      {
        int num4 = _param1 + num3 - num1 - 1;
        num3 -= num4;
        if (num3 <= 0)
        {
          \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
          return;
        }
        \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4 + num3, num4);
      }
      int num5 = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
      byte[] numArray = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
      do
      {
        _param3[_param4++] = numArray[num5++];
      }
      while (--num3 != 0);
    }
  }

  public void \u0023\u003DzMZv4GU8l7YQAa\u00242I8Gh1iSg\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    int num1 = this.\u0023\u003Dzq_GYkpSu5Wgn.Width - 1;
    int num2 = this.\u0023\u003Dzq_GYkpSu5Wgn.Height - 1;
    int num3 = _param5;
    byte[] numArray1 = _param3;
    int index1 = _param4;
    if (_param2 < 0 || _param2 > num2)
    {
      \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
    }
    else
    {
      if (_param1 < 0)
      {
        num3 += _param1;
        if (num3 <= 0)
        {
          \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
          return;
        }
        \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(numArray1, index1, -_param1);
        index1 -= _param1;
        _param1 = 0;
      }
      if (_param1 + num3 > num1)
      {
        int num4 = _param1 + num3 - num1 - 1;
        num3 -= num4;
        if (num3 <= 0)
        {
          \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(_param3, _param4, _param5);
          return;
        }
        \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzDrRZp7mp0k89(numArray1, index1 + num3, num4);
      }
      int index2 = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
      byte[] numArray2 = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
      do
      {
        numArray1[index1] = (byte) ((int) numArray1[index1] * (int) numArray2[index2] + (int) byte.MaxValue >> 8);
        ++index1;
        ++index2;
      }
      while (--num3 != 0);
    }
  }

  public void \u0023\u003Dzuvbutu_6FawR42V2yQ\u003D\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }

  public void \u0023\u003Dznhdl8qr0IfcJ5mclEBig_YA\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    throw new NotImplementedException();
  }
}
