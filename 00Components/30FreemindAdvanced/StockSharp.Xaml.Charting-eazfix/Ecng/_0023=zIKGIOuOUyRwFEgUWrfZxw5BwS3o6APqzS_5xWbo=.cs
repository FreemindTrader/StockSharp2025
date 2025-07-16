// Decompiled with JetBrains decompiler
// Type: #=zIKGIOuOUyRwFEgUWrfZxw5BwS3o6APqzS_5xWbo=
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

#nullable disable
public static class \u0023\u003DzIKGIOuOUyRwFEgUWrfZxw5BwS3o6APqzS_5xWbo\u003D
{
  public static string \u0023\u003DzPrH2XD0\u003D(
    this string _param0,
    string _param1,
    string _param2)
  {
    int startIndex = string.IsNullOrEmpty(_param1) ? 0 : _param0.IndexOf(_param1) + _param1.Length;
    int length = string.IsNullOrEmpty(_param2) ? _param0.Length : _param0.IndexOf(_param2) - startIndex;
    return _param0.Substring(startIndex, length);
  }

  public static bool \u0023\u003DzHHfYuvvaA57ehwCJow\u003D\u003D(this string _param0)
  {
    return string.IsNullOrEmpty(_param0) || string.Equals(_param0, new string(' ', _param0.Length));
  }
}
