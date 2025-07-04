// Decompiled with JetBrains decompiler
// Type: #=z3arZou$KE51WuqbncgcGPrum0As6lkvmcC63nlpCpixc
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

#nullable disable
internal sealed class \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrum0As6lkvmcC63nlpCpixc
{
  private bool \u0023\u003DzvEWdpLc\u0024gZvh;
  private TextBlock \u0023\u003DzW7z__AO00I_xgQP3P9j\u0024itfsbPpa;
  private TextBlock \u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D;
  private TextBlock \u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D;
  private Rect \u0023\u003DzSeN7xO1fyKm6Q3nypHODkAQ\u003D;
  private Thickness \u0023\u003DzNxAageK5is\u0024L8GeICO3cQjY\u003D;
  private double \u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D;

  public \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrum0As6lkvmcC63nlpCpixc(
    double _param1,
    double _param2,
    string _param3,
    TextAlignment _param4,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a _param5 = \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None)
    : this(_param1, _param2, new Thickness(), _param3, _param4, 0.0, _param5)
  {
  }

  public \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrum0As6lkvmcC63nlpCpixc(
    double _param1,
    double _param2,
    string _param3,
    TextAlignment _param4,
    double _param5,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a _param6 = \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None)
    : this(_param1, _param2, new Thickness(), _param3, _param4, _param5, _param6)
  {
  }

  public \u0023\u003Dz3arZou\u0024KE51WuqbncgcGPrum0As6lkvmcC63nlpCpixc(
    double _param1,
    double _param2,
    Thickness _param3,
    string _param4,
    TextAlignment _param5,
    double _param6,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a _param7 = \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None)
  {
    this.\u0023\u003DzvEWdpLc\u0024gZvh = false;
    this.\u0023\u003Dz5vqKZdNzQ7qHBD8RpA\u003D\u003D(_param3);
    if (_param6 > 0.0)
      this.FontSize = _param6;
    this.\u0023\u003Dz1nPVjH8\u003D(_param4, _param7);
    this.\u0023\u003DznjNs4ck\u003D(_param1, _param2, _param5);
  }

  public TextBlock \u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D()
  {
    return this.\u0023\u003DzW7z__AO00I_xgQP3P9j\u0024itfsbPpa;
  }

  private void \u0023\u003Dzl1Fs5HIfvjWBtDBUat7Qwj0\u003D(TextBlock _param1)
  {
    this.\u0023\u003DzW7z__AO00I_xgQP3P9j\u0024itfsbPpa = _param1;
  }

  public TextBlock Exponent
  {
    get => this.\u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D;
    private set => this.\u0023\u003Dziis2LAS1n\u0024lcleXxEQ\u003D\u003D = value;
  }

  public TextBlock Separator
  {
    get => this.\u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D;
    private set => this.\u0023\u003Dzk0QvTIqzWPOhJybPtA\u003D\u003D = value;
  }

  public Rect \u0023\u003Dz3X9GY8Ar8mu5() => this.\u0023\u003DzSeN7xO1fyKm6Q3nypHODkAQ\u003D;

  private void \u0023\u003DzJE\u0024BuzVbe28n(Rect _param1)
  {
    this.\u0023\u003DzSeN7xO1fyKm6Q3nypHODkAQ\u003D = _param1;
  }

  public Thickness \u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D()
  {
    return this.\u0023\u003DzNxAageK5is\u0024L8GeICO3cQjY\u003D;
  }

  private void \u0023\u003Dz5vqKZdNzQ7qHBD8RpA\u003D\u003D(Thickness _param1)
  {
    this.\u0023\u003DzNxAageK5is\u0024L8GeICO3cQjY\u003D = _param1;
  }

  public double FontSize
  {
    get => this.\u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D;
    private set => this.\u0023\u003DzBRCWtDn5nuSwxqXxOA\u003D\u003D = value;
  }

  public bool HasExponent => this.\u0023\u003DzvEWdpLc\u0024gZvh;

  private void \u0023\u003Dz1nPVjH8\u003D(
    string _param1,
    \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a _param2)
  {
    string str1 = _param1;
    string str2 = string.Empty;
    string str3 = string.Empty;
    int num = _param1.IndexOfAny(new char[2]{ 'e', 'E' });
    if (num > 0 && _param2 != \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.None)
    {
      this.\u0023\u003DzvEWdpLc\u0024gZvh = true;
      str1 = _param1.Substring(0, num);
      str2 = _param1.Substring(num + 1);
      str3 = _param1[num].ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (_param2 == \u0023\u003Dz9V3XmNblPtIESO78oE\u0024lpcGozNFuVHzTBvU7g3nrOy\u0024a.Normalized)
        str3 = "x10";
    }
    this.\u0023\u003Dzl1Fs5HIfvjWBtDBUat7Qwj0\u003D(new TextBlock()
    {
      Text = str1
    });
    if (this.FontSize.CompareTo(0.0) != 0)
      this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().FontSize = this.FontSize;
    TextBlock textBlock1;
    if (!this.\u0023\u003DzvEWdpLc\u0024gZvh)
    {
      textBlock1 = (TextBlock) null;
    }
    else
    {
      textBlock1 = new TextBlock();
      textBlock1.Text = str2;
    }
    this.Exponent = textBlock1;
    TextBlock textBlock2;
    if (!this.\u0023\u003DzvEWdpLc\u0024gZvh)
    {
      textBlock2 = (TextBlock) null;
    }
    else
    {
      textBlock2 = new TextBlock();
      textBlock2.Text = str3;
    }
    this.Separator = textBlock2;
  }

  private void \u0023\u003DznjNs4ck\u003D(double _param1, double _param2, TextAlignment _param3)
  {
    this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().\u0023\u003DzI0WdlDcUgrX_();
    if (this.\u0023\u003DzvEWdpLc\u0024gZvh)
    {
      this.Exponent.FontSize = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().FontSize / 1.2;
      this.Separator.FontSize = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().FontSize;
      this.Separator.\u0023\u003DzI0WdlDcUgrX_();
      this.Exponent.\u0023\u003DzI0WdlDcUgrX_();
    }
    double num1 = this.Separator == null ? 0.0 : this.Separator.ActualWidth;
    double num2 = this.Exponent == null ? 0.0 : this.Exponent.ActualWidth;
    string str = this.Exponent == null ? string.Empty : this.Exponent.Text;
    double num3 = this.Exponent == null ? 0.0 : this.Exponent.ActualHeight;
    double num4 = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + num1 + num2 + (this.\u0023\u003DzvEWdpLc\u0024gZvh ? 0.0 : 2.0);
    double num5 = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualHeight + num3 / 2.0;
    double num6 = _param2 + num3 / 2.0;
    double num7 = _param1 - num4 / 2.0;
    double num8;
    switch (_param3)
    {
      case TextAlignment.Left:
      case TextAlignment.Right:
        num7 = _param1;
        double num9 = _param2 - this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualHeight / 2.0;
        this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D(), num7);
        this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D(), num9);
        if (this.\u0023\u003DzvEWdpLc\u0024gZvh)
        {
          this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.Separator, num7 + this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + 1.0);
          this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.Separator, num9);
          this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.Exponent, num7 + this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + num1 + 1.0);
          this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.Exponent, num9 - num3 / 2.0);
        }
        num8 = num9 - num3 / 2.0;
        double num10 = _param1 - this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D().Left;
        double num11 = num8 - this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D().Top;
        double num12 = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + num1 + num2 + (str.\u0023\u003DzHHfYuvvaA57ehwCJow\u003D\u003D() ? 0.0 : 2.0);
        Thickness thickness1 = this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D();
        double right = thickness1.Right;
        double num13 = num12 + right;
        double num14 = this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualHeight + num3 / 2.0;
        thickness1 = this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D();
        double bottom = thickness1.Bottom;
        double num15 = num14 + bottom;
        this.\u0023\u003DzJE\u0024BuzVbe28n(new Rect(num10, num11, num13, num15));
        break;
      case TextAlignment.Center:
        this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D(), num7);
        this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D(), num6);
        if (this.\u0023\u003DzvEWdpLc\u0024gZvh)
        {
          this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.Separator, num7 + this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + 1.0);
          this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.Separator, num6);
          this.\u0023\u003DznBjYTD8\u003D((FrameworkElement) this.Exponent, num7 + this.\u0023\u003Dzm2ay5CW7X3HpmtcKHYbZifc\u003D().ActualWidth + num1 + 1.0);
          this.\u0023\u003DzulruhlM\u003D((FrameworkElement) this.Exponent, _param2);
        }
        num8 = _param2;
        double num16 = num7;
        Thickness thickness2 = this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D();
        double left = thickness2.Left;
        double num17 = num16 - left;
        double num18 = num8;
        thickness2 = this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D();
        double top = thickness2.Top;
        double num19 = num18 - top;
        double num20 = num4 + this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D().Right;
        double num21 = num5 + this.\u0023\u003Dzcev_Bm2dyEAF00e9TA\u003D\u003D().Bottom;
        this.\u0023\u003DzJE\u0024BuzVbe28n(new Rect(num17, num19, num20, num21));
        break;
      default:
        throw new InvalidOperationException("Invalid TextAlignment");
    }
    this.\u0023\u003DzJE\u0024BuzVbe28n(new Rect(num7, num8, num4, num5));
  }

  private void \u0023\u003DzulruhlM\u003D(FrameworkElement _param1, double _param2)
  {
    FrameworkElement frameworkElement = _param1;
    Thickness margin = _param1.Margin;
    double left = margin.Left;
    double top = _param2;
    margin = _param1.Margin;
    double right = margin.Right;
    margin = _param1.Margin;
    double bottom = margin.Bottom;
    Thickness thickness = new Thickness(left, top, right, bottom);
    frameworkElement.Margin = thickness;
  }

  private void \u0023\u003DznBjYTD8\u003D(FrameworkElement _param1, double _param2)
  {
    FrameworkElement frameworkElement = _param1;
    double left = _param2;
    Thickness margin = _param1.Margin;
    double top = margin.Top;
    margin = _param1.Margin;
    double right = margin.Right;
    margin = _param1.Margin;
    double bottom = margin.Bottom;
    Thickness thickness = new Thickness(left, top, right, bottom);
    frameworkElement.Margin = thickness;
  }
}
