// Decompiled with JetBrains decompiler
// Type: #=z5CbAZMXp7dgzzBe$G3xhis$LTAR$BJC8orkn13FnKLIfi3A9i$6SqEijqyQF
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
internal sealed class \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D> : 
  ChartCompentView<ChartLineElement>,
  \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D
  where \u0023\u003DzulcL8RA\u003D : struct, IComparable
{
  
  private readonly \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, double, double> \u0023\u003DzlkmfHYgr1H49;
  
  private \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D \u0023\u003DzKj7nvWQ\u003D;
  
  private ChartElementViewModel \u0023\u003DzZYTLjjg\u003D;
  
  private IComparable \u0023\u003DzFEDR40ugZMK3;
  
  private Func<IComparable, Color?> \u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;

  public \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF(
    ChartLineElement _param1)
    : base(_param1)
  {
    Type type = typeof (\u0023\u003DzulcL8RA\u003D);
    if (type != typeof (DateTime) && type != typeof (double))
      throw new NotSupportedException($"X type {type.Name} is not supported");
    this.\u0023\u003DzlkmfHYgr1H49 = new \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, double, double>();
  }

  protected override void Init()
  {
    base.Init();
    DrawStyles[] drawStylesArray = new DrawStyles[9]
    {
      DrawStyles.Line,
      DrawStyles.NoGapLine,
      DrawStyles.StepLine,
      DrawStyles.DashedLine,
      DrawStyles.Dot,
      DrawStyles.Histogram,
      DrawStyles.Bubble,
      DrawStyles.StackedBar,
      DrawStyles.Area
    };
    ChartCompentView<ChartLineElement>.AddStylePropertyChanging<DrawStyles>((IChartComponent) this.ChartComponentView, "Style", drawStylesArray);
    string[] strArray = new string[2]
    {
      "Color",
      "AdditionalColor"
    };
    this.ChartViewModel.AddChild(this.\u0023\u003DzZYTLjjg\u003D = new ChartElementViewModel((INotifyPropertyChanged) this.ChartComponentView, new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, Color>(this.\u0023\u003DzQiS8RB0xqqQL6lh\u0024nA\u003D\u003D), \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D ?? (\u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.\u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D = new Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string>(\u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383.SomeMethond0343.\u0023\u003DzHJXYrcAe2iQ0KKyLTQ\u003D\u003D)), strArray));
    this.\u0023\u003DzKj7nvWQ\u003D = new \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEf8BMO_ZOBlhDA\u003D\u003D((\u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D) this.\u0023\u003DzlkmfHYgr1H49, (IRenderableSeries) null);
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
    this.ScichartSurfaceMVVM.\u0023\u003DzBE5I4io\u003D(this.RootElem, (IRenderableSeries) this.\u0023\u003DzKj7nvWQ\u003D);
  }

  private Type \u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D()
  {
    switch (this.ChartComponentView.Style)
    {
      case DrawStyles.Line:
      case DrawStyles.NoGapLine:
      case DrawStyles.StepLine:
      case DrawStyles.DashedLine:
        return typeof (FastLineRenderableSeries);
      case DrawStyles.Dot:
        return typeof (XyScatterRenderableSeries);
      case DrawStyles.Histogram:
        return typeof (FastColumnRenderableSeries);
      case DrawStyles.Bubble:
        return typeof (FastBubbleRenderableSeries);
      case DrawStyles.StackedBar:
        return typeof (StackedColumnRenderableSeries);
      case DrawStyles.Area:
        return typeof (FastMountainRenderableSeries);
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  private void \u0023\u003DzaQ1S7AaIPkwA4Hm2eKJs7\u0024w\u003D()
  {
    if (this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries is BaseRenderableSeries renderSeries)
    {
      BindingOperations.ClearAllBindings((DependencyObject) renderSeries);
      this.ClearAll();
    }
    BaseRenderableSeries ls4St64EqzfbaEjd;
    switch (this.ChartComponentView.Style)
    {
      case DrawStyles.Line:
      case DrawStyles.NoGapLine:
      case DrawStyles.StepLine:
      case DrawStyles.DashedLine:
        ls4St64EqzfbaEjd = (BaseRenderableSeries) this.CreateRenderableSeries<FastLineRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        });
        ls4St64EqzfbaEjd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this.ChartComponentView, "Color");
        break;
      case DrawStyles.Dot:
        ls4St64EqzfbaEjd = (BaseRenderableSeries) this.CreateRenderableSeries<XyScatterRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        });
        ls4St64EqzfbaEjd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this.ChartComponentView, "Color");
        ls4St64EqzfbaEjd.SetBindings(BaseRenderableSeries.\u0023\u003DzNGe3htdX6rpV, (object) this.ChartComponentView, "DrawTemplate");
        break;
      case DrawStyles.Histogram:
        FastColumnRenderableSeries k3EqE9D32HkF4Ejd;
        ls4St64EqzfbaEjd = (BaseRenderableSeries) (k3EqE9D32HkF4Ejd = this.CreateRenderableSeries<FastColumnRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        k3EqE9D32HkF4Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzwCSejucukq6W, (object) this.ChartComponentView, "Color", converter: (IValueConverter) new ColorToBrushConverter());
        k3EqE9D32HkF4Ejd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this.ChartComponentView, "Color");
        break;
      case DrawStyles.Bubble:
        FastBubbleRenderableSeries b5lfOfnUo8w7EyJw;
        ls4St64EqzfbaEjd = (BaseRenderableSeries) (b5lfOfnUo8w7EyJw = this.CreateRenderableSeries<FastBubbleRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        b5lfOfnUo8w7EyJw.ResamplingMode = \u0023\u003Dzr3AyUEt11qAsNGjKm7GKWxmriZN_\u0024I_fB5TLZqozNbfOHxiykg\u003D\u003D.None;
        b5lfOfnUo8w7EyJw.SetBindings(FastBubbleRenderableSeries.\u0023\u003DzWsyKEigY1Lm6, (object) this.ChartComponentView, "Color", converter: (IValueConverter) new ColorToBrushConverter());
        b5lfOfnUo8w7EyJw.SetBindings(FastBubbleRenderableSeries.\u0023\u003DzgLLxE9j2DbxR, (object) this.ChartComponentView, "StrokeThickness", converter: (IValueConverter) new \u0023\u003DzQ4iRj1YTApc8D349VbLPOcYfVH1n3cgfJefIZNttPgl056hG45kULRE\u003D());
        break;
      case DrawStyles.StackedBar:
        StackedColumnRenderableSeries d5Uc36Ku2HzS32Ejd;
        ls4St64EqzfbaEjd = (BaseRenderableSeries) (d5Uc36Ku2HzS32Ejd = this.CreateRenderableSeries<StackedColumnRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        d5Uc36Ku2HzS32Ejd.UseUniformWidth = true;
        d5Uc36Ku2HzS32Ejd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this.ChartComponentView, "Color", converter: (IValueConverter) new \u0023\u003DzY\u0024Fbb9SLuCh5D6t_XKcJqKuhsUbi2xjkbRlj6EVaEl1lCbDsuw\u003D\u003D(), parameter: (object) 51);
        d5Uc36Ku2HzS32Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzwCSejucukq6W, (object) this.ChartComponentView, "Color", converter: (IValueConverter) new ColorToBrushConverter());
        d5Uc36Ku2HzS32Ejd.SetBindings(dje_zHYRPNQHHBLUELC2PHC86VUXPKVH2ZMKXPKFTYYKX6SKX4JY2EDLNT4C8JKXXPNRQ42CXGSK8E94CEP2_ejd.\u0023\u003DzVvc2lVdKTrj8, (object) this.ChartComponentView, "StrokeThickness", converter: (IValueConverter) new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhiiYRKe0897RDjLr\u0024L9wcxjXImUKaPnpxZj0\u003D());
        break;
      case DrawStyles.Area:
        FastMountainRenderableSeries curmvvpxnR5WlzaEjd;
        ls4St64EqzfbaEjd = (BaseRenderableSeries) (curmvvpxnR5WlzaEjd = this.CreateRenderableSeries<FastMountainRenderableSeries>(new ChartElementViewModel[1]
        {
          this.\u0023\u003DzZYTLjjg\u003D
        }));
        curmvvpxnR5WlzaEjd.SetBindings(BaseRenderableSeries.\u0023\u003DzIcVMwZBBZ1n3, (object) this.ChartComponentView, "Color");
        curmvvpxnR5WlzaEjd.SetBindings(BaseMountainRenderableSeries.\u0023\u003DzXc9apgJiH9mm, (object) this.ChartComponentView, "AdditionalColor", converter: (IValueConverter) new ColorToBrushConverter());
        break;
      default:
        throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, new object[1]
        {
          (object) this.ChartComponentView.Style
        }));
    }
    ls4St64EqzfbaEjd.SetBindings(BaseRenderableSeries.\u0023\u003Dz13qAkT\u0024eFXTPuGCUqjA\u0024Svw\u003D, (object) this.ChartComponentView, "DrawTemplate");
    ls4St64EqzfbaEjd.PaletteProvider = (\u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D) this;
    this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries = (IRenderableSeries) ls4St64EqzfbaEjd;
    this.SetupAxisMarkerAndBinding(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries, (IChartComponent) this.ChartComponentView, "ShowAxisMarker", "Color");
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
  }

  private void \u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D()
  {
    Type type = this.\u0023\u003Dzq1K_twB2qI4CAVGTXkCEBpY\u003D();
    if (this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries?.GetType() != type)
    {
      this.\u0023\u003DzaQ1S7AaIPkwA4Hm2eKJs7\u0024w\u003D();
    }
    else
    {
      if (!(this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries is FastLineRenderableSeries renderSeries))
        return;
      renderSeries.StrokeDashArray = (double[]) null;
      renderSeries.IsDigitalLine = false;
      renderSeries.DrawNaNAs = \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.Gaps;
      if (this.ChartComponentView.Style == DrawStyles.DashedLine)
        renderSeries.StrokeDashArray = new double[2]
        {
          5.0,
          5.0
        };
      else if (this.ChartComponentView.Style == DrawStyles.StepLine)
      {
        renderSeries.IsDigitalLine = true;
      }
      else
      {
        if (this.ChartComponentView.Style != DrawStyles.NoGapLine)
          return;
        renderSeries.DrawNaNAs = \u0023\u003DzV9O5tWduWosGLvu_87Zf5KIXjvA0HjqD6negDKigZjec_mB\u0024hq2WcZE\u003D.ClosedLines;
      }
    }
  }

  protected override void Clear()
  {
    this.ScichartSurfaceMVVM.\u0023\u003Dzwh_e_TheVZKh(this.RootElem);
  }

  protected override void UpdateUi()
  {
    this.\u0023\u003DzlkmfHYgr1H49.Clear();
    this.\u0023\u003DzFEDR40ugZMK3 = (IComparable) default (\u0023\u003DzulcL8RA\u003D);
  }

  public override bool Draw(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
  {
    return this.Draw<\u0023\u003DzulcL8RA\u003D>(CollectionHelper.ToEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(((IEnumerable) _param1).Cast<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<\u0023\u003DzulcL8RA\u003D>>(), ((IEnumerableEx) _param1).Count));
  }

  public bool Draw<TX1>(
    IEnumerableEx<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>> _param1)
    where TX1 : struct, IComparable
  {
    if (this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D != this.ChartComponentView.Colorer)
    {
      this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D = this.ChartComponentView.Colorer;
      this.\u0023\u003DzKj7nvWQ\u003D.RenderSeries.\u0023\u003Dzu\u0024P3XgkcE7BC()?.\u0023\u003Dz2VqWonc\u003D<ISciChartSurface>()?.InvalidateElement();
    }
    if (_param1 == null || CollectionHelper.IsEmpty<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>((IEnumerable<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>) _param1))
      return false;
    int count = ((IEnumerableEx) _param1).Count;
    IComparable comparable = this.\u0023\u003DzFEDR40ugZMK3;
    int index = -1;
    \u0023\u003DzulcL8RA\u003D[] array1 = new \u0023\u003DzulcL8RA\u003D[count];
    double[] array2 = new double[count];
    double[] array3 = new double[count];
    List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>> z6MdlWkBsH4List = new List<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>();
    foreach (ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1> z6MdlWkBsH4 in (IEnumerable<ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1>>) _param1)
    {
      \u0023\u003DzulcL8RA\u003D zulcL8Ra = (\u0023\u003DzulcL8RA\u003D) (ValueType) z6MdlWkBsH4.\u0023\u003Dz2_4KSTY\u003D();
      switch (zulcL8Ra.CompareTo((object) comparable))
      {
        case -1:
          throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.CannotChangeCandleValue, new object[2]
          {
            (object) zulcL8Ra,
            (object) comparable
          }));
        case 0:
          if (z6MdlWkBsH4.\u0023\u003Dz6qkMxm4QKemy() == 0)
            this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003DzFkV86a8\u003D(zulcL8Ra, z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv(), z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA(), 0);
          --count;
          break;
        default:
          ++index;
          array1[index] = zulcL8Ra;
          if (z6MdlWkBsH4.\u0023\u003Dz6qkMxm4QKemy() > 0)
          {
            array2[index] = double.NaN;
            array3[index] = double.NaN;
            break;
          }
          array2[index] = z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv();
          array3[index] = z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA();
          break;
      }
      if (z6MdlWkBsH4.\u0023\u003Dz6qkMxm4QKemy() > 0)
        z6MdlWkBsH4List.Add(z6MdlWkBsH4);
      comparable = (IComparable) zulcL8Ra;
    }
    if (count == 0)
      return false;
    Array.Resize<\u0023\u003DzulcL8RA\u003D>(ref array1, count);
    Array.Resize<double>(ref array2, count);
    Array.Resize<double>(ref array3, count);
    this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003Dznc8esWY\u003D((IEnumerable<\u0023\u003DzulcL8RA\u003D>) array1, (IEnumerable<double>) array2, (IEnumerable<double>) array3);
    if (z6MdlWkBsH4List.Count > 0)
    {
      foreach (ChartDrawData.\u0023\u003Dz6MdlWkBS_h\u00244<TX1> z6MdlWkBsH4 in z6MdlWkBsH4List)
        this.\u0023\u003DzlkmfHYgr1H49.\u0023\u003DzFkV86a8\u003D((\u0023\u003DzulcL8RA\u003D) (ValueType) z6MdlWkBsH4.\u0023\u003Dz2_4KSTY\u003D(), z6MdlWkBsH4.\u0023\u003DzZB\u0024O5xT4bzKv(), z6MdlWkBsH4.\u0023\u003Dzggdh\u0024\u00245CXRMA(), z6MdlWkBsH4.\u0023\u003Dz6qkMxm4QKemy());
    }
    this.\u0023\u003DzFEDR40ugZMK3 = comparable;
    return true;
  }

  protected override void RootElementPropertyChanged(
    IChartComponent _param1,
    string _param2)
  {
    base.RootElementPropertyChanged(_param1, _param2);
    if (!(_param2 == "Style"))
      return;
    this.\u0023\u003DzAKENEAn7IdgA5685VZNYI6E\u003D();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzaP7vgnwtOd1ghQwnj\u00248jG1HXnj3oIOVYNqLIS92dT0MqcEWOD7IYv5ohC5gQ(
    IRenderableSeries _param1,
    double _param2,
    double _param3,
    double _param4,
    double _param5,
    double _param6)
  {
    return new Color?();
  }

  Color? \u0023\u003Dz8HlC6EDl\u0024btRSPRwAzbJh1gj3_fBHIvbLIG5Htg5ScQRmCkwmAANyPA\u003D.\u0023\u003DzbcX\u0024ot\u0024Zhy6wUdB9J7NC3yzxavQxmzggPmZ8V62OI0Kr0hq2Km30eq101sCk(
    IRenderableSeries _param1,
    double _param2,
    double _param3)
  {
    if (!(_param1.get_DataSeries() is \u0023\u003DzlbW67NDum9APBLuSxcbgNFrHbXEQ3PaVam0xJhsXKfF7<\u0023\u003DzulcL8RA\u003D, double, double> dataSeries))
      return new Color?();
    int index = (int) _param2;
    if (!(typeof (\u0023\u003DzulcL8RA\u003D) == typeof (DateTime)))
    {
      Func<IComparable, Color?> gdlrKjMgW0aU9TwiA = this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;
      return gdlrKjMgW0aU9TwiA == null ? new Color?() : gdlrKjMgW0aU9TwiA((IComparable) _param2);
    }
    Func<IComparable, Color?> gdlrKjMgW0aU9TwiA1 = this.\u0023\u003DzpGDlrKJMgW0aU9TwiA\u003D\u003D;
    return gdlrKjMgW0aU9TwiA1 == null ? new Color?() : gdlrKjMgW0aU9TwiA1((IComparable) TimeHelper.ToDateTimeOffset((DateTime) (ValueType) dataSeries.XValues[index], TimeZoneInfo.Utc));
  }

  private Color \u0023\u003DzQiS8RB0xqqQL6lh\u0024nA\u003D\u003D(
    \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
  {
    return this.ChartComponentView.Style == DrawStyles.StackedBar || this.ChartComponentView.Style == DrawStyles.Area ? ChartElementViewModel.GetHigherAlphaColor(this.ChartComponentView.Color, this.ChartComponentView.AdditionalColor) : this.ChartComponentView.Color;
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383 SomeMethond0343 = new \u0023\u003Dz5CbAZMXp7dgzzBe\u0024G3xhis\u0024LTAR\u0024BJC8orkn13FnKLIfi3A9i\u00246SqEijqyQF<\u0023\u003DzulcL8RA\u003D>.SomeClass34343383();
    public static Func<\u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D, string> \u0023\u003DziezdSvgFTxAlfqI9CA\u003D\u003D;

    internal string \u0023\u003DzHJXYrcAe2iQ0KKyLTQ\u003D\u003D(
      \u0023\u003DzYH1zUE63H2wnu5PkgVdj8C0KCtI6r27_gg\u003D\u003D _param1)
    {
      return _param1.FormattedYValue;
    }
  }
}
