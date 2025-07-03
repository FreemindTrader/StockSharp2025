// Decompiled with JetBrains decompiler
// Type: #=zITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzITX8mZ2jbGEtwuB21HaSb94StZu7BSE7Sw\u003D\u003D
{
  internal static void \u0023\u003DzUuEJfBE\u003D(bool _param0, string _param1)
  {
    if (!_param0)
      throw new ArgumentException(_param1);
  }

  internal static void \u0023\u003DzVDzEWto\u003D(object _param0, string _param1)
  {
    if (_param0 == null)
      throw new ArgumentNullException(_param1, $"The Argument {_param1} cannot be null");
  }

  internal static void \u0023\u003DzsmufvA2pkwpA(
    int _param0,
    string _param1,
    int _param2,
    string _param3)
  {
    if (_param0 != _param2)
      throw new InvalidOperationException($"Arrays {_param1} and {_param3} must have the same length");
  }

  public static void \u0023\u003DzWz_gx9jgbpNA(double _param0)
  {
    if (!_param0.\u0023\u003Dz_Bj0HmLWq3hY())
      throw new InvalidOperationException($"Value {_param0} is not a real number");
  }

  public static void \u0023\u003Dzb9sKgvTSQj5CMO21dA\u003D\u003D(DateTime _param0, string _param1)
  {
    if (DateTime.MinValue == _param0 || DateTime.MaxValue == _param0)
      throw new InvalidOperationException($"DateTime Argument {_param1} is not defined");
  }

  public static \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0\u003D \u0023\u003DzlTskcr4\u003D(
    IComparable _param0,
    string _param1)
  {
    return new \u0023\u003DzNpTQ6VGNYT7plNgM4mFVSmVXa75ENDZ2yd0DZz0\u003D(_param0, _param1);
  }
}
