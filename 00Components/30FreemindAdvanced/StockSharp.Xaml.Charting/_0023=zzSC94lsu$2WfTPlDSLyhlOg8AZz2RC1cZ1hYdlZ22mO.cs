// Decompiled with JetBrains decompiler
// Type: #=zzSC94lsu$2WfTPlDSLyhlOg8AZz2RC1cZ1hYdlZ22mOTs6M1Rc5MHAxGSwybk_pTjg==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

using System;
using System.Globalization;

#nullable disable
internal sealed class \u0023\u003DzzSC94lsu\u00242WfTPlDSLyhlOg8AZz2RC1cZ1hYdlZ22mOTs6M1Rc5MHAxGSwybk_pTjg\u003D\u003D : 
  \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B
{
  public override void \u0023\u003DzWzUaFxw\u003D(
    \u0023\u003DzpWMIzYBzoypE5Wwh\u0024gRH6ek_dynWMOFzgH4RlW\u0024\u0024B0lB _param1)
  {
    if (!(_param1 is \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D))
      throw new ArgumentException(XXX.SSS(-539338386));
    base.\u0023\u003DzWzUaFxw\u003D(_param1);
  }

  public override \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D \u0023\u003Dz9xSd9Yg\u003D(
    \u0023\u003DzGf68ilGq59TJ0aVKr0K_9c1X8_XLOwuwCkANZ8F3lvgpooqqVw\u003D\u003D _param1,
    IComparable _param2)
  {
    \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D tmjze8uJg3v1qUa6q9M = this.\u0023\u003DzHZDgUSdfqmkx() as \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrnCKeTj4UlchcD8Tmjze8uJG3v1qUA6q9M\u003D;
    \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D lqkdKbY6smnsRhNQalw1 = (\u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D) _param1;
    lqkdKbY6smnsRhNQalw1.HasExponent = tmjze8uJg3v1qUa6q9M != null && tmjze8uJg3v1qUa6q9M.get_ScientificNotation() == \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.LogarithmicBase;
    if (lqkdKbY6smnsRhNQalw1.HasExponent)
    {
      string textFormatting = this.\u0023\u003DzHZDgUSdfqmkx().get_TextFormatting();
      int num1 = textFormatting.IndexOfAny(new char[2]
      {
        'e',
        'E'
      });
      string str1 = textFormatting.Substring(0, num1);
      string str2 = textFormatting.Substring(num1 + 1);
      string str3 = XXX.SSS(-539338442);
      string str4 = str3;
      if (str2.StartsWith(XXX.SSS(-539328743)))
        str4 = XXX.SSS(-539328743) + str4;
      string str5 = str4 + XXX.SSS(-539338491) + str3 + XXX.SSS(-539338468);
      double a = _param2.\u0023\u003Dzb9UCYbo\u003D();
      double y = Math.Log(a, tmjze8uJg3v1qUa6q9M.get_LogarithmicBase());
      double num2 = a / Math.Pow(tmjze8uJg3v1qUa6q9M.get_LogarithmicBase(), y);
      lqkdKbY6smnsRhNQalw1.HasExponent = true;
      lqkdKbY6smnsRhNQalw1.Text = string.Format((IFormatProvider) CultureInfo.InvariantCulture, XXX.SSS(-539430209) + str1 + XXX.SSS(-539338473), (object) num2);
      lqkdKbY6smnsRhNQalw1.Exponent = string.Format((IFormatProvider) CultureInfo.InvariantCulture, XXX.SSS(-539430209) + str5 + XXX.SSS(-539430223), (object) y);
      \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D lqkdKbY6smnsRhNQalw2 = lqkdKbY6smnsRhNQalw1;
      double logarithmicBase = tmjze8uJg3v1qUa6q9M.get_LogarithmicBase();
      string str6;
      if (!logarithmicBase.Equals(Math.E))
      {
        logarithmicBase = tmjze8uJg3v1qUa6q9M.get_LogarithmicBase();
        str6 = logarithmicBase.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
        str6 = textFormatting.Substring(num1, 1);
      lqkdKbY6smnsRhNQalw2.Separator = str6;
    }
    else
      _param1 = base.\u0023\u003Dz9xSd9Yg\u003D(_param1, _param2);
    return _param1;
  }
}
