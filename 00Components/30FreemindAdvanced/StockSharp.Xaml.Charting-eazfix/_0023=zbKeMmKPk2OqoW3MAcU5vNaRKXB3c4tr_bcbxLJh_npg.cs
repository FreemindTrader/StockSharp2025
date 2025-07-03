// Decompiled with JetBrains decompiler
// Type: #=zbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npg0ToIQspxEOSxZDc8CPWTKZg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;

#nullable disable
internal static class \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNaRKXB3c4tr_bcbxLJh_npg0ToIQspxEOSxZDc8CPWTKZg\u003D\u003D
{
  public static int \u0023\u003DzqaUGipRjoOHk(int _param0) => _param0 >> 4;

  public static int \u0023\u003Dz7ZLw4Sj113Tr(int _param0) => _param0 << 4;

  public static int \u0023\u003DzCvMG77WdhpD4__lbZw\u003D\u003D(int _param0) => _param0 << 8;

  public static void \u0023\u003DzyOXg1EDPrMhyRBFe_A\u003D\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param0,
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param1,
    out int _param2,
    out int _param3)
  {
    double num1 = (double) _param1.\u0023\u003Dzeids9mY\u003D / (double) _param0.\u0023\u003Dzeids9mY\u003D;
    double num2 = (double) _param1.\u0023\u003DzxpGWEWQ\u003D - (double) (_param1.\u0023\u003DzLNbWazE\u003D - _param0.\u0023\u003DzLNbWazE\u003D) * num1;
    double num3 = (double) _param1.\u0023\u003Dz3MxuAfw\u003D - (double) (_param1.\u0023\u003DzrS5XlCc\u003D - _param0.\u0023\u003DzrS5XlCc\u003D) * num1;
    if ((double) (_param1.\u0023\u003DzxpGWEWQ\u003D - _param1.\u0023\u003DzLNbWazE\u003D) * (double) (_param1.\u0023\u003DzrS5XlCc\u003D - _param0.\u0023\u003DzrS5XlCc\u003D) < (double) (_param1.\u0023\u003Dz3MxuAfw\u003D - _param1.\u0023\u003DzrS5XlCc\u003D) * (double) (_param1.\u0023\u003DzLNbWazE\u003D - _param0.\u0023\u003DzLNbWazE\u003D) + 100.0)
    {
      num2 -= (num2 - (double) _param1.\u0023\u003DzLNbWazE\u003D) * 2.0;
      num3 -= (num3 - (double) _param1.\u0023\u003DzrS5XlCc\u003D) * 2.0;
    }
    double num4 = num2 - (double) _param1.\u0023\u003DzLNbWazE\u003D;
    double num5 = num3 - (double) _param1.\u0023\u003DzrS5XlCc\u003D;
    if ((int) Math.Sqrt(num4 * num4 + num5 * num5) < 256 /*0x0100*/)
    {
      _param2 = _param1.\u0023\u003DzLNbWazE\u003D + _param1.\u0023\u003DzLNbWazE\u003D + (_param1.\u0023\u003DzrS5XlCc\u003D - _param0.\u0023\u003DzrS5XlCc\u003D) + (_param1.\u0023\u003Dz3MxuAfw\u003D - _param1.\u0023\u003DzrS5XlCc\u003D) >> 1;
      _param3 = _param1.\u0023\u003DzrS5XlCc\u003D + _param1.\u0023\u003DzrS5XlCc\u003D - (_param1.\u0023\u003DzLNbWazE\u003D - _param0.\u0023\u003DzLNbWazE\u003D) - (_param1.\u0023\u003DzxpGWEWQ\u003D - _param1.\u0023\u003DzLNbWazE\u003D) >> 1;
    }
    else
    {
      _param2 = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H(num2);
      _param3 = \u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H(num3);
    }
  }

  public static void \u0023\u003Dz0pIwvXqY\u00243PPx1xwq0aquk3MB0TUPaQClJnAKBg\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param0,
    ref int _param1,
    ref int _param2)
  {
    if (\u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H(((double) (_param1 - _param0.\u0023\u003DzxpGWEWQ\u003D) * (double) (_param0.\u0023\u003Dz3MxuAfw\u003D - _param0.\u0023\u003DzrS5XlCc\u003D) - (double) (_param2 - _param0.\u0023\u003Dz3MxuAfw\u003D) * (double) (_param0.\u0023\u003DzxpGWEWQ\u003D - _param0.\u0023\u003DzLNbWazE\u003D)) / (double) _param0.\u0023\u003Dzeids9mY\u003D) >= 128 /*0x80*/)
      return;
    _param1 = _param0.\u0023\u003DzLNbWazE\u003D + (_param0.\u0023\u003Dz3MxuAfw\u003D - _param0.\u0023\u003DzrS5XlCc\u003D);
    _param2 = _param0.\u0023\u003DzrS5XlCc\u003D - (_param0.\u0023\u003DzxpGWEWQ\u003D - _param0.\u0023\u003DzLNbWazE\u003D);
  }

  public static void \u0023\u003DzbeNhGWFw1dIS2BM8PGelnvKfy0rPjVZLO7IBgC0\u003D(
    \u0023\u003DzSQJobdqtH0NktyvbaGGemf4lpuP7IWrDyPzfHcmWMZgPGDmCBoWgpkV6Fnoq _param0,
    ref int _param1,
    ref int _param2)
  {
    if (\u0023\u003DzV9O5tWduWosGLvu_87Zf5OXt7zllMlwUCoVEqrXWXWOo_9I8LKlxnD0wx5l0vOI7XMUaGCc\u003D.\u0023\u003DzQ9DKAFLSaa9H(((double) (_param1 - _param0.\u0023\u003DzxpGWEWQ\u003D) * (double) (_param0.\u0023\u003Dz3MxuAfw\u003D - _param0.\u0023\u003DzrS5XlCc\u003D) - (double) (_param2 - _param0.\u0023\u003Dz3MxuAfw\u003D) * (double) (_param0.\u0023\u003DzxpGWEWQ\u003D - _param0.\u0023\u003DzLNbWazE\u003D)) / (double) _param0.\u0023\u003Dzeids9mY\u003D) >= 128 /*0x80*/)
      return;
    _param1 = _param0.\u0023\u003DzxpGWEWQ\u003D + (_param0.\u0023\u003Dz3MxuAfw\u003D - _param0.\u0023\u003DzrS5XlCc\u003D);
    _param2 = _param0.\u0023\u003Dz3MxuAfw\u003D - (_param0.\u0023\u003DzxpGWEWQ\u003D - _param0.\u0023\u003DzLNbWazE\u003D);
  }
}
