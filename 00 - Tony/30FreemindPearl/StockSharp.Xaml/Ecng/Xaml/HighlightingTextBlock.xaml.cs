// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.HighlightingTextBlock
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Ecng.Xaml
{
  /// <summary>
  /// A specialized highlighting text block control.
  /// http://www.jeff.wilcox.name/2008/11/highlighting-autocompletebox/
  /// </summary>
  /// <summary>HighlightingTextBlock</summary>
  public class HighlightingTextBlock : TextBlock, IComponentConnector
  {
    /// <summary>Identifies the HighlightText dependency property.</summary>
    public static readonly DependencyProperty HighlightTextProperty = DependencyProperty.Register(nameof(2127279644), typeof (string), typeof (HighlightingTextBlock), new PropertyMetadata(new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzCIWaffn2vL6y))));
    /// <summary>Identifies the HighlightBrush dependency property.</summary>
    public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register(nameof(2127279616), typeof (Brush), typeof (HighlightingTextBlock), new PropertyMetadata((object) null, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003DzniDvVhtZTvcj))));
    /// <summary>
    /// Identifies the HighlightFontWeight dependency property.
    /// </summary>
    public static readonly DependencyProperty HighlightFontWeightProperty = DependencyProperty.Register(nameof(2127279861), typeof (FontWeight), typeof (HighlightingTextBlock), new PropertyMetadata((object) FontWeights.Normal, new PropertyChangedCallback((object) null, __methodptr(\u0023\u003Dz1VqUvpVozkvSWm_8tA\u003D\u003D))));
    
    private bool \u0023\u003DzN9FSF60\u003D;
    
    private bool \u0023\u003DzwPHzQV2Vg5J\u0024;

    /// <summary>Initializes a new HighlightingTextBlock class.</summary>
    public HighlightingTextBlock()
    {
      this.DefaultStyleKey = (object) typeof (HighlightingTextBlock);
    }

    /// <summary>Gets or sets the highlighted text.</summary>
    public string HighlightText
    {
      get
      {
        return this.GetValue(HighlightingTextBlock.HighlightTextProperty) as string;
      }
      set
      {
        this.SetValue(HighlightingTextBlock.HighlightTextProperty, (object) value);
      }
    }

    private static void \u0023\u003DzCIWaffn2vL6y(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      ((HighlightingTextBlock) _param0).\u0023\u003DzOHU3GQ37arpA();
    }

    /// <summary>Gets or sets the highlight brush.</summary>
    public Brush HighlightBrush
    {
      get
      {
        return this.GetValue(HighlightingTextBlock.HighlightBrushProperty) as Brush;
      }
      set
      {
        this.SetValue(HighlightingTextBlock.HighlightBrushProperty, (object) value);
      }
    }

    private static void \u0023\u003DzniDvVhtZTvcj(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
      ((HighlightingTextBlock) _param0).\u0023\u003DzOHU3GQ37arpA();
    }

    /// <summary>
    /// Gets or sets the font weight used on highlighted text.
    /// </summary>
    public FontWeight HighlightFontWeight
    {
      get
      {
        return (FontWeight) this.GetValue(HighlightingTextBlock.HighlightFontWeightProperty);
      }
      set
      {
        this.SetValue(HighlightingTextBlock.HighlightFontWeightProperty, (object) value);
      }
    }

    private static void \u0023\u003Dz1VqUvpVozkvSWm_8tA\u003D\u003D(
      DependencyObject _param0,
      DependencyPropertyChangedEventArgs _param1)
    {
    }

    private void \u0023\u003DzOHU3GQ37arpA()
    {
      if (!this.\u0023\u003DzN9FSF60\u003D)
      {
        this.Inlines.Clear();
        foreach (char ch in this.Text)
          this.Inlines.Add((Inline) new Run()
          {
            Text = ch.To<string>()
          });
        this.\u0023\u003DzN9FSF60\u003D = true;
      }
      string str1 = this.Text ?? string.Empty;
      string str2 = this.HighlightText ?? string.Empty;
      int num1 = 0;
label_12:
      while (num1 < str1.Length)
      {
        int num2 = str2.Length == 0 ? -1 : str1.IndexOf(str2, num1, StringComparison.OrdinalIgnoreCase);
        for (int index = num2 < 0 ? str1.Length : num2; num1 < index && num1 < str1.Length; ++num1)
        {
          Inline inline = this.Inlines.ElementAt<Inline>(num1);
          inline.Foreground = this.Foreground;
          inline.FontWeight = this.FontWeight;
        }
        int num3 = num1;
        while (true)
        {
          if (num1 < num3 + str2.Length && num1 < str1.Length)
          {
            Inline inline = this.Inlines.ElementAt<Inline>(num1);
            inline.Foreground = this.HighlightBrush;
            inline.FontWeight = this.HighlightFontWeight;
            ++num1;
          }
          else
            goto label_12;
        }
      }
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    public void InitializeComponent()
    {
      if (this.\u0023\u003DzwPHzQV2Vg5J\u0024)
        return;
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
      Application.LoadComponent((object) this, new Uri(nameof(2127279839), UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "6.0.8.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.\u0023\u003DzwjqCwJRp5nvBkvFFuDtdoCHyTx2y(
      int _param1,
      object _param2)
    {
      this.\u0023\u003DzwPHzQV2Vg5J\u0024 = true;
    }
  }
}
