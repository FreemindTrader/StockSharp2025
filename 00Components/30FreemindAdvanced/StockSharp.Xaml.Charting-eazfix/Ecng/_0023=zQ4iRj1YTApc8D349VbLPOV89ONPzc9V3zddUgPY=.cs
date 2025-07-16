// Decompiled with JetBrains decompiler
// Type: #=zQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
public sealed class \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D : 
  \u0023\u003DzZScQl1C_L0f_XQiTX6oTcyrI5xM77ZuKeI88UaM\u003D
{
  public static int \u0023\u003Dz34Tp6NEN8Abr = 33554432 /*0x02000000*/;
  public static int \u0023\u003DzxpWfwYUEdeay = 2048 /*0x0800*/;
  private readonly LinkedList<Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D>> \u0023\u003DzKdPJclnoJAsz = new LinkedList<Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D>>();
  private readonly Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, byte[]> \u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D = new Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, byte[]>();
  private int \u0023\u003DzxQJBOYWola1R;
  private readonly Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, WriteableBitmap> \u0023\u003DzLfXhl\u0024LTlEY_Xwe1TXGJuck\u003D = new Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, WriteableBitmap>();
  private readonly Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, int[]> \u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D = new Dictionary<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, int[]>();

  public int \u0023\u003Dz1KJAskZOhP4f() => this.\u0023\u003DzxQJBOYWola1R;

  private void \u0023\u003DzQGhpoazqbRE2im9Msg\u003D\u003D()
  {
    while (this.\u0023\u003DzxQJBOYWola1R > \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz34Tp6NEN8Abr || this.\u0023\u003DzKdPJclnoJAsz.Count > \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003DzxpWfwYUEdeay)
    {
      Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D> tuple = this.\u0023\u003DzKdPJclnoJAsz.First.Value;
      this.\u0023\u003DzKdPJclnoJAsz.RemoveFirst();
      if (tuple.Item2 == (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 0)
      {
        this.\u0023\u003DzxQJBOYWola1R -= this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D[tuple.Item1].Length;
        this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D.Remove(tuple.Item1);
      }
      else if (tuple.Item2 == (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 2)
      {
        WriteableBitmap writeableBitmap = this.\u0023\u003DzLfXhl\u0024LTlEY_Xwe1TXGJuck\u003D[tuple.Item1];
        this.\u0023\u003DzxQJBOYWola1R -= writeableBitmap.PixelHeight * writeableBitmap.PixelWidth * 4;
        this.\u0023\u003DzLfXhl\u0024LTlEY_Xwe1TXGJuck\u003D.Remove(tuple.Item1);
      }
      else
      {
        if (tuple.Item2 != (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 1)
          throw new Exception("unknown TextureType");
        this.\u0023\u003DzxQJBOYWola1R -= this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D[tuple.Item1].Length * 4;
        this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D.Remove(tuple.Item1);
      }
    }
  }

  public void \u0023\u003DzeYeAjD8\u003D(Size _param1, Brush _param2, byte[] _param3)
  {
    \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D key = new \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(_param1, _param2);
    if (this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D.ContainsKey(key))
    {
      this.\u0023\u003DzxQJBOYWola1R -= this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D[key].Length;
      this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D[key] = _param3;
      this.\u0023\u003DzxQJBOYWola1R += _param3.Length;
    }
    else
    {
      this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D.Add(key, _param3);
      this.\u0023\u003DzxQJBOYWola1R += _param3.Length;
      this.\u0023\u003DzKdPJclnoJAsz.AddLast(new Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D>(key, (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 0));
      this.\u0023\u003DzQGhpoazqbRE2im9Msg\u003D\u003D();
    }
  }

  public byte[] \u0023\u003DzlOSbHnJKdduh(Size _param1, Brush _param2)
  {
    byte[] numArray;
    return !this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D.TryGetValue(new \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(_param1, _param2), out numArray) ? (byte[]) null : numArray;
  }

  public WriteableBitmap \u0023\u003DzuAOGltcBIMXJ(FrameworkElement _param1)
  {
    \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D key = new \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(_param1);
    WriteableBitmap writeableBitmap;
    if (!this.\u0023\u003DzLfXhl\u0024LTlEY_Xwe1TXGJuck\u003D.TryGetValue(key, out writeableBitmap))
    {
      writeableBitmap = _param1.\u0023\u003DzWmxEXx9881f\u0024();
      this.\u0023\u003DzLfXhl\u0024LTlEY_Xwe1TXGJuck\u003D.Add(key, writeableBitmap);
      this.\u0023\u003DzxQJBOYWola1R += writeableBitmap.PixelWidth * writeableBitmap.PixelHeight * 4;
      this.\u0023\u003DzKdPJclnoJAsz.AddLast(new Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D>(key, (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 2));
    }
    this.\u0023\u003DzQGhpoazqbRE2im9Msg\u003D\u003D();
    return writeableBitmap;
  }

  public void \u0023\u003DzeYeAjD8\u003D(Size _param1, Brush _param2, int[] _param3)
  {
    \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D key = new \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(_param1, _param2);
    if (this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D.ContainsKey(key))
    {
      this.\u0023\u003DzxQJBOYWola1R -= this.\u0023\u003DzBSenOp9Q5Ubc8b8h\u0024AIPzFE\u003D[key].Length * 4;
      this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D[key] = _param3;
      this.\u0023\u003DzxQJBOYWola1R += _param3.Length * 4;
    }
    else
    {
      this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D.Add(key, _param3);
      this.\u0023\u003DzxQJBOYWola1R += _param3.Length * 4;
      this.\u0023\u003DzKdPJclnoJAsz.AddLast(new Tuple<\u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D, \u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D>(key, (\u0023\u003DzQ4iRj1YTApc8D349VbLPOV89ONPzc9V3zddUgPY\u003D.\u0023\u003Dz6UmS9q8\u003D) 1));
      this.\u0023\u003DzQGhpoazqbRE2im9Msg\u003D\u003D();
    }
  }

  public int[] \u0023\u003DzCQL\u0024Quea_QT8(Size _param1, Brush _param2)
  {
    int[] numArray;
    return !this.\u0023\u003DzMQQUg0mhP_zmp6oKUfTltjs\u003D.TryGetValue(new \u0023\u003DzXMBDTJZ70yU3Te1Rk5MqG4NeX6nMgPydnA\u003D\u003D(_param1, _param2), out numArray) ? (int[]) null : numArray;
  }

  private enum \u0023\u003Dz6UmS9q8\u003D
  {
  }
}
