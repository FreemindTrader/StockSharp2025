// Decompiled with JetBrains decompiler
// Type: #=z5tgJOvSsgsmn_0Qv_7eNQ6PRW443CXjnpjY_jRLnxeQQey4VixhEfLv3WEbdfJtrVw==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

#nullable disable
public sealed class TypeFace
{
  private string \u0023\u003DzmNYaxFg\u003D;
  private int \u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D;
  private string \u0023\u003Dzz3d39DAyibKX;
  private int \u0023\u003DzzS3aJw1Da6nl;
  private string \u0023\u003DzuaP8ojCFobQT7qAQgA\u003D\u003D;
  private int \u0023\u003DzCu36riKHj0j2;
  private TypeFace.\u0023\u003DztrGFU0EyM2_0 \u0023\u003DzfemHw0FF3xh2OBkJ\u0024A\u003D\u003D;
  private int \u0023\u003DzzGpJ4e08a\u0024\u0024_;
  private int \u0023\u003DzJDD4k\u0024ciIH02;
  private int \u0023\u003DzmAVInH3niaSc;
  private int \u0023\u003Dz_Qlon1QYYmxh;
  private RectangleInt \u0023\u003DzypY0S9AeVnD2;
  private int \u0023\u003DzmP5CGsKOimFZDt\u0024iKkRvlbUtn9dk;
  private int \u0023\u003Dze8tJZyduQTy9FV6iEha6Ipw\u003D;
  private string \u0023\u003DzZXLAFHk41BjN;
  private TypeFace.\u0023\u003DzBajRNyc\u003D \u0023\u003DzRz4XM2qWN534;
  private Dictionary<int, TypeFace.\u0023\u003DzBajRNyc\u003D> \u0023\u003DzBiidJptRYuVL = new Dictionary<int, TypeFace.\u0023\u003DzBajRNyc\u003D>();
  private Dictionary<char, Dictionary<char, int>> \u0023\u003DzSqlx4ORB7psf = new Dictionary<char, Dictionary<char, int>>();
  private static Regex \u0023\u003DzygX_Z2V3pebJ = new Regex("[-+]?[0-9]*\\.?[0-9]+");

  public int \u0023\u003DznUNSmhGt9lvCxl\u0024Rlg\u003D\u003D()
  {
    return this.\u0023\u003DzzGpJ4e08a\u0024\u0024_;
  }

  public int \u0023\u003Dz\u0024o3NSCR\u0024eeym9xPhRw\u003D\u003D()
  {
    return this.\u0023\u003DzJDD4k\u0024ciIH02;
  }

  public int \u0023\u003Dz39BTDp11rdEc() => this.\u0023\u003DzmAVInH3niaSc;

  public int \u0023\u003DzPWQ_4PfsNVL8() => this.\u0023\u003Dz_Qlon1QYYmxh;

  public RectangleInt \u0023\u003DzlXV1PkA5CFnd()
  {
    return this.\u0023\u003DzypY0S9AeVnD2;
  }

  public int \u0023\u003DznbqhU0H0KL1mJeTlJdcDh1w\u003D()
  {
    return this.\u0023\u003DzmP5CGsKOimFZDt\u0024iKkRvlbUtn9dk;
  }

  public int \u0023\u003DzrWvQWVYRjlb_() => this.\u0023\u003Dze8tJZyduQTy9FV6iEha6Ipw\u003D;

  public int \u0023\u003DzJz2GtHxDwgfEBj4ifw\u003D\u003D() => this.\u0023\u003DzCu36riKHj0j2;

  private static string \u0023\u003DzauNNAETvl1IT(string _param0, string _param1, string _param2)
  {
    int num = 0;
    return TypeFace.\u0023\u003DzauNNAETvl1IT(_param0, _param1, _param2, ref num);
  }

  private static string \u0023\u003DzauNNAETvl1IT(
    string _param0,
    string _param1,
    string _param2,
    ref int _param3)
  {
    int num1 = _param0.IndexOf(_param1, _param3);
    if (num1 < 0)
      return (string) null;
    int num2 = _param0.IndexOf(_param2, num1 + _param1.Length);
    int length = num2 - (num1 + _param1.Length);
    _param3 = num2 + _param2.Length;
    return _param0.Substring(num1 + _param1.Length, length);
  }

  private static string \u0023\u003DzKzcceY4\u003D(string _param0, string _param1)
  {
    return TypeFace.\u0023\u003DzauNNAETvl1IT(_param0, _param1 + "=\"", "\"");
  }

  private static bool \u0023\u003DzLFdoj5hQ0pjJ(
    string _param0,
    string _param1,
    out int _param2,
    ref int _param3)
  {
    return int.TryParse(TypeFace.\u0023\u003DzauNNAETvl1IT(_param0, _param1 + "=\"", "\"", ref _param3), NumberStyles.Number, (IFormatProvider) null, out _param2);
  }

  private static bool \u0023\u003DzLFdoj5hQ0pjJ(string _param0, string _param1, out int _param2)
  {
    int num = 0;
    return TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(_param0, _param1, out _param2, ref num);
  }

  public static TypeFace \u0023\u003Dzy1JpMYyJEH_\u0024(
    string _param0)
  {
    TypeFace vixhEfLv3WebdfJtrVw = new TypeFace();
    vixhEfLv3WebdfJtrVw.\u0023\u003DzitHctvSMcYYt(new StreamReader(_param0).ReadToEnd());
    return vixhEfLv3WebdfJtrVw;
  }

  private static double \u0023\u003DzAzn\u0024r\u0024SQyO5k(string _param0, ref int _param1)
  {
    Match match = TypeFace.\u0023\u003DzygX_Z2V3pebJ.Match(_param0, _param1);
    string s = match.Value;
    _param1 = match.Index + match.Length;
    double num;
    ref double local = ref num;
    double.TryParse(s, NumberStyles.Number, (IFormatProvider) null, out local);
    return num;
  }

  private TypeFace.\u0023\u003DzBajRNyc\u003D \u0023\u003DziyY3Oh3pqQuixRvWMw\u003D\u003D(
    string _param1)
  {
    TypeFace.\u0023\u003DzBajRNyc\u003D zBajRnyc = new TypeFace.\u0023\u003DzBajRNyc\u003D();
    if (!TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(_param1, "horiz-adv-x", out zBajRnyc.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D))
      zBajRnyc.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D = this.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D;
    zBajRnyc.\u0023\u003DzJ0MRvdM\u003D = TypeFace.\u0023\u003DzKzcceY4\u003D(_param1, "glyph-name");
    string s = TypeFace.\u0023\u003DzKzcceY4\u003D(_param1, "unicode");
    if (s != null)
    {
      if (s.Length == 1)
      {
        zBajRnyc.\u0023\u003DzecBoJIo\u003D = (int) s[0];
      }
      else
      {
        if (s.Split(';').Length > 1 && s.Split(';')[1].Length > 0)
          throw new NotImplementedException("We do not currently support glyphs longer than one character.  You need to wirite the seach so that it will find them if you want to support this");
        if (!int.TryParse(s, NumberStyles.Number, (IFormatProvider) null, out zBajRnyc.\u0023\u003DzecBoJIo\u003D))
          int.TryParse(TypeFace.\u0023\u003DzauNNAETvl1IT(s, "&#x", ";"), NumberStyles.HexNumber, (IFormatProvider) null, out zBajRnyc.\u0023\u003DzecBoJIo\u003D);
      }
    }
    string str = TypeFace.\u0023\u003DzKzcceY4\u003D(_param1, "d");
    int index = 0;
    Vector2 jnelpsqX4Q78W2Ejd1 = new Vector2(0.0, 0.0);
    Vector2 jnelpsqX4Q78W2Ejd2 = new Vector2(0.0, 0.0);
    if (str == null || str.Length == 0)
      return zBajRnyc;
    while (index < str.Length)
    {
      char ch = str[index];
      Vector2 jnelpsqX4Q78W2Ejd3;
      switch (ch)
      {
        case '\n':
        case '\r':
        case ' ':
          ++index;
          jnelpsqX4Q78W2Ejd3 = jnelpsqX4Q78W2Ejd2;
          break;
        case 'H':
        case 'h':
          ++index;
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = jnelpsqX4Q78W2Ejd2.dje_zLPL6EZPA_ejd;
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          if (ch == 'h')
            jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd += jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd;
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003DzdYWObjwF7Rbj(jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd);
          break;
        case 'L':
        case 'l':
          ++index;
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          if (ch == 'l')
            jnelpsqX4Q78W2Ejd3 += jnelpsqX4Q78W2Ejd2;
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003DzQVwIFA0\u003D(jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd, jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd);
          break;
        case 'M':
          ++index;
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003DzfRDRUq8\u003D(jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd, jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd);
          break;
        case 'Q':
        case 'q':
          ++index;
          Vector2 jnelpsqX4Q78W2Ejd4;
          jnelpsqX4Q78W2Ejd4.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd4.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          if (ch == 'q')
          {
            jnelpsqX4Q78W2Ejd4 += jnelpsqX4Q78W2Ejd2;
            jnelpsqX4Q78W2Ejd3 += jnelpsqX4Q78W2Ejd2;
          }
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003DzjmwZ80wGkCgy(jnelpsqX4Q78W2Ejd4.dje_z3GBAX47U_ejd, jnelpsqX4Q78W2Ejd4.dje_zLPL6EZPA_ejd, jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd, jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd);
          break;
        case 'T':
        case 't':
          ++index;
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          if (ch == 't')
            jnelpsqX4Q78W2Ejd3 += jnelpsqX4Q78W2Ejd2;
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003DzjmwZ80wGkCgy(jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd, jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd);
          break;
        case 'V':
        case 'v':
          ++index;
          jnelpsqX4Q78W2Ejd3.dje_z3GBAX47U_ejd = jnelpsqX4Q78W2Ejd2.dje_z3GBAX47U_ejd;
          jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd = TypeFace.\u0023\u003DzAzn\u0024r\u0024SQyO5k(str, ref index);
          if (ch == 'v')
            jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd += jnelpsqX4Q78W2Ejd2.dje_zLPL6EZPA_ejd;
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003Dz9lJzoxD7fRXT(jnelpsqX4Q78W2Ejd3.dje_zLPL6EZPA_ejd);
          break;
        case 'Z':
        case 'z':
          ++index;
          jnelpsqX4Q78W2Ejd3 = jnelpsqX4Q78W2Ejd2;
          zBajRnyc.\u0023\u003DzY3eILVU\u003D.\u0023\u003Dznqbixy_A1don();
          break;
        default:
          throw new NotImplementedException($"unrecognized d command '{ch.ToString()}'.");
      }
      jnelpsqX4Q78W2Ejd2 = jnelpsqX4Q78W2Ejd3;
    }
    return zBajRnyc;
  }

  public void \u0023\u003DzitHctvSMcYYt(string _param1)
  {
    int num = 0;
    string str1 = TypeFace.\u0023\u003DzauNNAETvl1IT(_param1, "<font", ">", ref num);
    this.\u0023\u003DzmNYaxFg\u003D = TypeFace.\u0023\u003DzKzcceY4\u003D(str1, "id");
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str1, "horiz-adv-x", out this.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D);
    string str2 = TypeFace.\u0023\u003DzauNNAETvl1IT(_param1, "<font-face", "/>", ref num);
    this.\u0023\u003Dzz3d39DAyibKX = TypeFace.\u0023\u003DzKzcceY4\u003D(str2, "font-family");
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "font-weight", out this.\u0023\u003DzzS3aJw1Da6nl);
    this.\u0023\u003DzuaP8ojCFobQT7qAQgA\u003D\u003D = TypeFace.\u0023\u003DzKzcceY4\u003D(str2, "font-stretch");
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "units-per-em", out this.\u0023\u003DzCu36riKHj0j2);
    this.\u0023\u003DzfemHw0FF3xh2OBkJ\u0024A\u003D\u003D = new TypeFace.\u0023\u003DztrGFU0EyM2_0(TypeFace.\u0023\u003DzKzcceY4\u003D(str2, "panose-1"));
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "ascent", out this.\u0023\u003DzzGpJ4e08a\u0024\u0024_);
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "descent", out this.\u0023\u003DzJDD4k\u0024ciIH02);
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "x-height", out this.\u0023\u003DzmAVInH3niaSc);
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "cap-height", out this.\u0023\u003Dz_Qlon1QYYmxh);
    string[] strArray = TypeFace.\u0023\u003DzKzcceY4\u003D(str2, "bbox").Split(' ');
    int.TryParse(strArray[0], out this.\u0023\u003DzypY0S9AeVnD2.\u0023\u003DzP4R7yU0\u003D);
    int.TryParse(strArray[1], out this.\u0023\u003DzypY0S9AeVnD2.\u0023\u003DzRNV_Dpk\u003D);
    int.TryParse(strArray[2], out this.\u0023\u003DzypY0S9AeVnD2.\u0023\u003Dzp55dtus\u003D);
    int.TryParse(strArray[3], out this.\u0023\u003DzypY0S9AeVnD2.\u0023\u003DzSzOWcj8\u003D);
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "underline-thickness", out this.\u0023\u003DzmP5CGsKOimFZDt\u0024iKkRvlbUtn9dk);
    TypeFace.\u0023\u003DzLFdoj5hQ0pjJ(str2, "underline-position", out this.\u0023\u003Dze8tJZyduQTy9FV6iEha6Ipw\u003D);
    this.\u0023\u003DzZXLAFHk41BjN = TypeFace.\u0023\u003DzKzcceY4\u003D(str2, "unicode-range");
    this.\u0023\u003DzRz4XM2qWN534 = this.\u0023\u003DziyY3Oh3pqQuixRvWMw\u003D\u003D(TypeFace.\u0023\u003DzauNNAETvl1IT(_param1, "<missing-glyph", "/>", ref num));
    for (string str3 = TypeFace.\u0023\u003DzauNNAETvl1IT(_param1, "<glyph", "/>", ref num); str3 != null; str3 = TypeFace.\u0023\u003DzauNNAETvl1IT(_param1, "<glyph", "/>", ref num))
    {
      TypeFace.\u0023\u003DzBajRNyc\u003D zBajRnyc = this.\u0023\u003DziyY3Oh3pqQuixRvWMw\u003D\u003D(str3);
      if (zBajRnyc.\u0023\u003DzecBoJIo\u003D > 0)
        this.\u0023\u003DzBiidJptRYuVL.Add(zBajRnyc.\u0023\u003DzecBoJIo\u003D, zBajRnyc);
    }
  }

  public IVertexSource \u0023\u003DzNERJc2ZTLWjF(
    char _param1)
  {
    TypeFace.\u0023\u003DzBajRNyc\u003D zBajRnyc;
    return this.\u0023\u003DzBiidJptRYuVL.TryGetValue((int) _param1, out zBajRnyc) ? (IVertexSource) zBajRnyc.\u0023\u003DzY3eILVU\u003D : (IVertexSource) null;
  }

  public double \u0023\u003DzKFEh56TZQDn_(char _param1, char _param2)
  {
    TypeFace.\u0023\u003DzBajRNyc\u003D zBajRnyc;
    return this.\u0023\u003DzBiidJptRYuVL.TryGetValue((int) _param1, out zBajRnyc) ? (double) zBajRnyc.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D : 0.0;
  }

  public double \u0023\u003DzKFEh56TZQDn_(char _param1)
  {
    TypeFace.\u0023\u003DzBajRNyc\u003D zBajRnyc;
    return this.\u0023\u003DzBiidJptRYuVL.TryGetValue((int) _param1, out zBajRnyc) ? (double) zBajRnyc.\u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D : 0.0;
  }

  public void \u0023\u003Dz3M41kBkOgA7h(
    Graphics2D _param1)
  {
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nhyf2vFxdpD7yzk3jSuvaC4SjCnmZxFhnUntq4SU38DaaqYCd08\u003D untq4Su38DaaqYcd08_1 = new \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nhyf2vFxdpD7yzk3jSuvaC4SjCnmZxFhnUntq4SU38DaaqYCd08\u003D(this, 30.0);
    StringPrinter uvK7eC8LfFuyZatA = new StringPrinter(this.\u0023\u003Dzz3d39DAyibKX + " - 30 point", untq4Su38DaaqYcd08_1);
    RectangleDouble hvaqyP1QajNmmjbaj = untq4Su38DaaqYcd08_1.\u0023\u003DzJ2xmeT9_rf5KLMYoPQ\u003D\u003D();
    double num1 = 10.0 - hvaqyP1QajNmmjbaj.\u0023\u003DzP4R7yU0\u003D;
    double num2 = num1;
    double num3 = 10.0 - untq4Su38DaaqYcd08_1.\u0023\u003DzVrrs6g\u0024GmSx3p0uCE\u0024YVO\u0024M\u003D();
    int num4 = 50;
    RGBA_Bytes nwsEwePinXgsJj4Q1 = new RGBA_Bytes(0, 0, 0);
    RGBA_Bytes nwsEwePinXgsJj4Q2 = new RGBA_Bytes(0, 0, 0);
    RGBA_Bytes nwsEwePinXgsJj4Q3 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
    RGBA_Bytes nwsEwePinXgsJj4Q4 = new RGBA_Bytes((int) byte.MaxValue, 0, 0);
    RGBA_Bytes nwsEwePinXgsJj4Q5 = new RGBA_Bytes(12, 25, 200);
    RGBA_Bytes nwsEwePinXgsJj4Q6 = new RGBA_Bytes(12, 25, 200);
    RGBA_Bytes nwsEwePinXgsJj4Q7 = new RGBA_Bytes(0, 150, 55);
    _param1.\u0023\u003DzRKlLwRo\u003D(num2, num3, num2 + (double) num4, num3, nwsEwePinXgsJj4Q2);
    _param1.\u0023\u003DztzEIdzI\u003D(num2 + hvaqyP1QajNmmjbaj.\u0023\u003DzP4R7yU0\u003D, num3 + hvaqyP1QajNmmjbaj.\u0023\u003DzRNV_Dpk\u003D, num2 + hvaqyP1QajNmmjbaj.\u0023\u003Dzp55dtus\u003D, num3 + hvaqyP1QajNmmjbaj.\u0023\u003DzSzOWcj8\u003D, nwsEwePinXgsJj4Q1, 1.0);
    double num5 = num2 + untq4Su38DaaqYcd08_1.\u0023\u003DzJ2xmeT9_rf5KLMYoPQ\u003D\u003D().Width * 1.5;
    int num6 = num4 * 3;
    double num7 = untq4Su38DaaqYcd08_1.\u0023\u003DzpTe8MlgPKqOpM0copg\u003D\u003D();
    _param1.\u0023\u003DzRKlLwRo\u003D(num5, num3 + num7, num5 + (double) num6, num3 + num7, nwsEwePinXgsJj4Q3);
    double num8 = untq4Su38DaaqYcd08_1.\u0023\u003DzVrrs6g\u0024GmSx3p0uCE\u0024YVO\u0024M\u003D();
    _param1.\u0023\u003DzRKlLwRo\u003D(num5, num3 + num8, num5 + (double) num6, num3 + num8, nwsEwePinXgsJj4Q4);
    double num9 = untq4Su38DaaqYcd08_1.\u0023\u003Dz_7nTblYqT9jET\u0024CN9w\u003D\u003D();
    _param1.\u0023\u003DzRKlLwRo\u003D(num5, num3 + num9, num5 + (double) num6, num3 + num9, nwsEwePinXgsJj4Q5);
    double num10 = untq4Su38DaaqYcd08_1.\u0023\u003DzknIb_SVH363x_gd59w\u003D\u003D();
    _param1.\u0023\u003DzRKlLwRo\u003D(num5, num3 + num10, num5 + (double) num6, num3 + num10, nwsEwePinXgsJj4Q6);
    double num11 = untq4Su38DaaqYcd08_1.\u0023\u003DzHN4FEddtChvLyFQugg\u003D\u003D();
    _param1.\u0023\u003DzRKlLwRo\u003D(num5, num3 + num11, num5 + (double) num6, num3 + num11, nwsEwePinXgsJj4Q7);
    // ISSUE: variable of a boxed type
    __Boxed<Affine> local = (ValueType) (Affine.\u0023\u003Dz_tAu8gs\u003D() * Affine.\u0023\u003Dzi7nEj5k\u003D(10.0, num1));
    VertexSourceApplyTransform znpRwHj98V9LHhN68 = new VertexSourceApplyTransform((IVertexSource) uvK7eC8LfFuyZatA, (ITransform) local);
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) znpRwHj98V9LHhN68, RGBA_Bytes.\u0023\u003DzXMsQczM\u003D);
    \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nhyf2vFxdpD7yzk3jSuvaC4SjCnmZxFhnUntq4SU38DaaqYCd08\u003D untq4Su38DaaqYcd08_2 = new \u0023\u003DzzyimtvyB5d3orEuABYi\u0024nhyf2vFxdpD7yzk3jSuvaC4SjCnmZxFhnUntq4SU38DaaqYCd08\u003D(this, 12.0);
    Vector2 jnelpsqX4Q78W2Ejd = new Vector2(num5 + (double) (num6 / 2), num3 + untq4Su38DaaqYcd08_1.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D() * 1.5);
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("Descent"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q4);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("Underline"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q7);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("X Height"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q5);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("CapHeight"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q6);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("Ascent"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q3);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("Origin"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q2);
    jnelpsqX4Q78W2Ejd.dje_zLPL6EZPA_ejd += untq4Su38DaaqYcd08_2.\u0023\u003DzzI3F5hw6o7OxWT19ew\u003D\u003D();
    _param1.\u0023\u003DzLDS6T7I\u003D((IVertexSource) new StringPrinter("Bounding Box"), jnelpsqX4Q78W2Ejd, nwsEwePinXgsJj4Q1);
  }

  private sealed class \u0023\u003DzBajRNyc\u003D
  {
    public int \u0023\u003DzahUZ_5b_Y\u0024Z0qG5Yow\u003D\u003D;
    public int \u0023\u003DzecBoJIo\u003D;
    public string \u0023\u003DzJ0MRvdM\u003D;
    public PathStorage \u0023\u003DzY3eILVU\u003D = new PathStorage();
  }

  private sealed class \u0023\u003DztrGFU0EyM2_0
  {
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzjSk9D1c\u003D \u0023\u003DzmrA5Iss\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzeIEVLmaWCAZfxI\u0024XWw\u003D\u003D \u0023\u003Dz85AORdk72BHS;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzJ5DsYck\u003D \u0023\u003DzYHNc1vw\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003Dz1iVpwY8glZwko1s3tA\u003D\u003D \u0023\u003DzVJSWyijFEVx0bloigQ\u003D\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzverQJQc\u003D \u0023\u003DzxKmZBRY6DbH8oxLa4Q\u003D\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzX3uiN2c7v3uVKSnNobT5wLc\u003D \u0023\u003DzonEqPuF3gsDazXs8Ma4Ldxo\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003Dz2iTUbcxjUPiU \u0023\u003Dzg\u0024nMe3zVuj8E;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzBZC1hDjUZz2jdywqAA\u003D\u003D \u0023\u003DzotQzxj8AoxowJNGx7A\u003D\u003D;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DznZvwnyU1yjNf \u0023\u003Dz7SElfMttplUq;
    private TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzAp2Ea60\u003D \u0023\u003DzF_0wpiE\u003D;

    public \u0023\u003DztrGFU0EyM2_0(string _param1)
    {
      string[] strArray = _param1.Split(' ');
      int result;
      if (int.TryParse(strArray[0], out result))
        this.\u0023\u003DzmrA5Iss\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzjSk9D1c\u003D) result;
      if (int.TryParse(strArray[1], out result))
        this.\u0023\u003Dz85AORdk72BHS = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzeIEVLmaWCAZfxI\u0024XWw\u003D\u003D) result;
      if (int.TryParse(strArray[2], out result))
        this.\u0023\u003DzYHNc1vw\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzJ5DsYck\u003D) result;
      if (int.TryParse(strArray[3], out result))
        this.\u0023\u003DzVJSWyijFEVx0bloigQ\u003D\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003Dz1iVpwY8glZwko1s3tA\u003D\u003D) result;
      if (int.TryParse(strArray[4], out result))
        this.\u0023\u003DzxKmZBRY6DbH8oxLa4Q\u003D\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzverQJQc\u003D) result;
      if (int.TryParse(strArray[5], out result))
        this.\u0023\u003DzonEqPuF3gsDazXs8Ma4Ldxo\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzX3uiN2c7v3uVKSnNobT5wLc\u003D) result;
      if (int.TryParse(strArray[6], out result))
        this.\u0023\u003Dzg\u0024nMe3zVuj8E = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003Dz2iTUbcxjUPiU) result;
      if (int.TryParse(strArray[7], out result))
        this.\u0023\u003DzotQzxj8AoxowJNGx7A\u003D\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzBZC1hDjUZz2jdywqAA\u003D\u003D) result;
      if (int.TryParse(strArray[8], out result))
        this.\u0023\u003Dz7SElfMttplUq = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DznZvwnyU1yjNf) result;
      if (!int.TryParse(strArray[0], out result))
        return;
      this.\u0023\u003DzF_0wpiE\u003D = (TypeFace.\u0023\u003DztrGFU0EyM2_0.\u0023\u003DzAp2Ea60\u003D) result;
    }

    private enum \u0023\u003Dz1iVpwY8glZwko1s3tA\u003D\u003D
    {
    }

    private enum \u0023\u003Dz2iTUbcxjUPiU
    {
    }

    private enum \u0023\u003DzAp2Ea60\u003D
    {
    }

    private enum \u0023\u003DzBZC1hDjUZz2jdywqAA\u003D\u003D
    {
    }

    private enum \u0023\u003DzJ5DsYck\u003D
    {
    }

    private enum \u0023\u003DzX3uiN2c7v3uVKSnNobT5wLc\u003D
    {
    }

    private enum \u0023\u003DzeIEVLmaWCAZfxI\u0024XWw\u003D\u003D
    {
    }

    private enum \u0023\u003DzjSk9D1c\u003D
    {
    }

    private enum \u0023\u003DznZvwnyU1yjNf
    {
    }

    private enum \u0023\u003DzverQJQc\u003D
    {
    }
  }
}
