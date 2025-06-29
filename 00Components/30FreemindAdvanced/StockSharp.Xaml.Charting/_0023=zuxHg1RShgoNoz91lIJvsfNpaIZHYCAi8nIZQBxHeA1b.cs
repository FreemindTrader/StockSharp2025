// Decompiled with JetBrains decompiler
// Type: #=zuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1bYuJPy71Yd6geBTiTgRvujp4_sN2kwARwbT441sg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;

#nullable disable
internal sealed class \u0023\u003DzuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1bYuJPy71Yd6geBTiTgRvujp4_sN2kwARwbT441sg\u003D\u003D : 
  \u0023\u003Dz5VLaAZX2bctAcuSoajSAXhoSlmBm0oGMB\u0024RrBBDXLSS28mTU1\u0024kh32C_bmlBdAw\u0024tw\u003D\u003D
{
  private \u0023\u003DzviDbWv7UmJNh\u0024GKRSPGKrhpWi_3XO1_6AkUbgNIlZG1JTfJPF45bQEQe\u0024b9Zb\u0024d8Dw\u003D\u003D \u0023\u003Dzq_GYkpSu5Wgn;
  private uint \u0023\u003DzfFySSudz\u0024SOy;
  private uint \u0023\u003Dzosch78aT\u00245mK;
  public static int \u0023\u003DzC5aH6eUup5RPeQILWA\u003D\u003D = 8;
  public static int \u0023\u003DzYp3S3uuOMppS6WJotA\u003D\u003D = 0;
  public static int \u0023\u003Dz68hARzzl638oLDKc3Q\u003D\u003D = (int) byte.MaxValue;

  public \u0023\u003DzuxHg1RShgoNoz91lIJvsfNpaIZHYCAi8nIZQBxHeA1bYuJPy71Yd6geBTiTgRvujp4_sN2kwARwbT441sg\u003D\u003D(
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
    int index = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    return this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D()[index];
  }

  public byte \u0023\u003DzScHVJa9MMOhSof4pGw\u003D\u003D(int _param1, int _param2, byte _param3)
  {
    int index = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    byte[] numArray = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
    return (byte) ((int) byte.MaxValue + (int) _param3 * (int) numArray[index] >> 8);
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
    int num = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    byte[] numArray = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
    do
    {
      _param3[_param4++] = numArray[num++];
    }
    while (--_param5 != 0);
  }

  public void \u0023\u003DzMZv4GU8l7YQAa\u00242I8Gh1iSg\u003D(
    int _param1,
    int _param2,
    byte[] _param3,
    int _param4,
    int _param5)
  {
    int index = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003DzHlHGfKJZNJsq(_param1, _param2);
    byte[] numArray = this.\u0023\u003Dzq_GYkpSu5Wgn.\u0023\u003Dz9b1_JhA\u003D();
    do
    {
      _param3[_param4] = (byte) ((int) byte.MaxValue + (int) _param3[_param4] * (int) numArray[index] >> 8);
      ++_param4;
      ++index;
    }
    while (--_param5 != 0);
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
