// Decompiled with JetBrains decompiler
// Type: #=za5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Common;
using System;
using System.Globalization;

#nullable disable
public class \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B : 
  LabelProviderBase
{
  public override ITickLabelViewModel CreateDataContext(
    IComparable _param1)
  {
    return this.UpdateDataContext((ITickLabelViewModel) new \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D(), _param1);
  }

  public override ITickLabelViewModel UpdateDataContext(
    ITickLabelViewModel _param1,
    IComparable _param2)
  {
    base.UpdateDataContext(_param1, _param2);
    \u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D lqkdKbY6smnsRhNQalw = (\u0023\u003DzkNYFXojT3SDc_K8aE\u0024Ajg285baMQh1LqkdKbY6smnsRH\u0024nQalw\u003D\u003D) _param1;
    NumericAxis mrfV9Sq7FuggeEjd = (NumericAxis) this.\u0023\u003DzHZDgUSdfqmkx();
    string text = _param1.get_Text();
    lqkdKbY6smnsRhNQalw.Text = text;
    int num = text.IndexOfAny(new char[2]{ 'e', 'E' });
    lqkdKbY6smnsRhNQalw.HasExponent = mrfV9Sq7FuggeEjd.ScientificNotation != \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None && num >= 0;
    if (lqkdKbY6smnsRhNQalw.HasExponent)
    {
      lqkdKbY6smnsRhNQalw.Text = text.Substring(0, num);
      lqkdKbY6smnsRhNQalw.Exponent = text.Substring(num + 1);
      lqkdKbY6smnsRhNQalw.Separator = mrfV9Sq7FuggeEjd.ScientificNotation == \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.Normalized ? "x10" : text[num].ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }
    return (ITickLabelViewModel) lqkdKbY6smnsRhNQalw;
  }

  public override string FormatCursorLabel(IComparable _param1, bool _param2)
  {
    string str = _param2 ? this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting() : this.\u0023\u003DzHZDgUSdfqmkx().get_TextFormatting();
    return !string.IsNullOrEmpty(str) ? \u0023\u003Dza5uC6EI3X0HH3HGpwdgoZt5Gh_M7zik7mtVzjUImZB9B.\u0023\u003DzRDs3D1Q\u003D(_param1, str) : this.FormatLabel(_param1);
  }

  private static string \u0023\u003DzRDs3D1Q\u003D(IComparable _param0, string _param1)
  {
    string str = string.Format($"{{0:{_param1}}}", (object) _param0);
    if (StringHelper.IsEmpty(str) && _param0 is double num)
      str = ((Decimal) num).ToString("0.0000000000");
    return str;
  }

  public override string FormatLabel(IComparable _param1)
  {
    return string.Format($"{{0:{this.\u0023\u003DzHZDgUSdfqmkx().get_TextFormatting()}}}", (object) _param1);
  }
}
