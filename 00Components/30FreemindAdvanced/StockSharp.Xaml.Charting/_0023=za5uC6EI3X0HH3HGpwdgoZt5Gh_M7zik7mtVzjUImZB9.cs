// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using \u002D;
using Ecng.Common;
using System;
using System.Globalization;

#nullable disable
internal class \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B : 
  \u0023\u003DziARJyOecclYiJO5UbZqQJ8tTDgXi4mHI9\u0024dB3n962FPd
{
  public override \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D \u0023\u003DzILIqSWE\u003D(
    IComparable _param1)
  {
    return this.\u0023\u003Dz9xSd9Yg\u003D((\u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D) new \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D(), _param1);
  }

  public override \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D \u0023\u003Dz9xSd9Yg\u003D(
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D _param1,
    IComparable _param2)
  {
    base.\u0023\u003Dz9xSd9Yg\u003D(_param1, _param2);
    \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D lqkdKbY6smnsRhNQalw = (\u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D) _param1;
    dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd mrfV9Sq7FuggeEjd = (dje_zX3N6Q9TLRHQ6K6VPWM4NP8NG2M44BK7RCDU397R9MRFV9SQ7FUGGE_ejd) this.\u0023\u003DzHZDgUSdfqmkx();
    string text = _param1.get_Text();
    lqkdKbY6smnsRhNQalw.Text = text;
    int num = text.IndexOfAny(new char[2]{ 'e', 'E' });
    lqkdKbY6smnsRhNQalw.HasExponent = mrfV9Sq7FuggeEjd.ScientificNotation != \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None && num >= 0;
    if (lqkdKbY6smnsRhNQalw.HasExponent)
    {
      lqkdKbY6smnsRhNQalw.Text = text.Substring(0, num);
      lqkdKbY6smnsRhNQalw.Exponent = text.Substring(num + 1);
      lqkdKbY6smnsRhNQalw.Separator = mrfV9Sq7FuggeEjd.ScientificNotation == \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.Normalized ? XXX.SSS(-539337168) : text[num].ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }
    return (\u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D) lqkdKbY6smnsRhNQalw;
  }

  public override string \u0023\u003Dz\u0024WinkXTLMGVP(IComparable _param1, bool _param2)
  {
    string str = _param2 ? this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting() : this.\u0023\u003DzHZDgUSdfqmkx().get_TextFormatting();
    return !string.IsNullOrEmpty(str) ? \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B.\u0023\u003DzRDs3D1Q\u003D(_param1, str) : this.\u0023\u003DzkqN2vZ4\u003D(_param1);
  }

  private static string \u0023\u003DzRDs3D1Q\u003D(IComparable _param0, string _param1)
  {
    string str = string.Format(XXX.SSS(-539430209) + _param1 + XXX.SSS(-539430223), (object) _param0);
    if (StringHelper.IsEmpty(str) && _param0 is double num)
      str = ((Decimal) num).ToString(XXX.SSS(-539337869));
    return str;
  }

  public override string \u0023\u003DzkqN2vZ4\u003D(IComparable _param1)
  {
    return string.Format(XXX.SSS(-539430209) + this.\u0023\u003DzHZDgUSdfqmkx().get_TextFormatting() + XXX.SSS(-539430223), (object) _param1);
  }
}
