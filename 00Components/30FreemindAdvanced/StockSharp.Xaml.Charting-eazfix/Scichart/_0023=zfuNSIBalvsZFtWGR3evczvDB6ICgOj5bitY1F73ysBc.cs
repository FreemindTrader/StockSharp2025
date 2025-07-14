// Decompiled with JetBrains decompiler
// Type: #=zfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ==
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using StockSharp.Messages;
using StockSharp.Xaml.Charting.Model.DataSeries.SegmentDataSeries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
internal abstract class \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D : 
  BaseRenderableSeries
{
  
  private static readonly Brush \u0023\u003Dzqh5vkWSCXpeDFfTKRw\u003D\u003D = (Brush) new LinearGradientBrush(Color.FromRgb((byte) 0, (byte) 128 /*0x80*/, (byte) 0), Color.FromRgb((byte) 0, (byte) 15, (byte) 0), 90.0);
  
  private Dictionary<double, Tuple<double, CandlePriceLevel>> \u0023\u003Dzwzwqm6ek0X1oFaJOBAtZrz4\u003D = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
  
  public static readonly DependencyProperty \u0023\u003DzclgWzqVX9aMz0ymbjg\u003D\u003D = DependencyProperty.Register(nameof (PriceStep), typeof (Decimal?), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) 0.000001M, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dzi0KcPWeppPsdH7enWLwUIiA\u003D = DependencyProperty.Register(nameof (LocalHorizontalVolumes), typeof (bool), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) false, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzQertKBH63fdmC8AJyRsykJc\u003D = DependencyProperty.Register(nameof (ShowHorizontalVolumes), typeof (bool), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzItT23YVSDC4D2EO\u0024iA\u003D\u003D = DependencyProperty.Register(nameof (HorizontalVolumeWidthFraction), typeof (double), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) 0.15, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9), new CoerceValueCallback(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzHJJ8vuq4bO\u0024yTwjWdw\u003D\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzkZL83tvq\u0024ktX = DependencyProperty.Register(nameof (VolumeBarsBrush), typeof (Brush), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzqh5vkWSCXpeDFfTKRw\u003D\u003D, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzagbwXpWOZg7bLqSU7A\u003D\u003D = DependencyProperty.Register(nameof (VolBarsFontColor), typeof (Color), typeof (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D), new PropertyMetadata((object) Colors.White, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dz6m31HqKWmW3sf6kpzl00drQ\u003D = DependencyProperty.Register("DrawSeparateVolumes", typeof (bool), typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd), new PropertyMetadata((object) true, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzsiwadMWumB4q = DependencyProperty.Register("BuyColor", typeof (Color), typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd), new PropertyMetadata((object) Colors.Green, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzyQkELvBo\u002432H = DependencyProperty.Register("SellColor", typeof (Color), typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd), new PropertyMetadata((object) Colors.Red, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003Dzd0DbwufxB45U = DependencyProperty.Register("UpColor", typeof (Color), typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd), new PropertyMetadata((object) Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  public static readonly DependencyProperty \u0023\u003DzMymlj1BxH8MY = DependencyProperty.Register("DownColor", typeof (Color), typeof (dje_zMFW7VEH9YQSML9Y7R42FYSK685GBSP6YDHCWWGNXQE6V7BBN8HWC7XLY67P47NZHA3X42ZPH5UUQPRZRVLJWD_ejd), new PropertyMetadata((object) Colors.DarkGreen, new PropertyChangedCallback(BaseRenderableSeries.\u0023\u003Dzmf\u0024vfR3OJQU9)));
  
  protected \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D \u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D;

  public bool LocalHorizontalVolumes
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzi0KcPWeppPsdH7enWLwUIiA\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzi0KcPWeppPsdH7enWLwUIiA\u003D, (object) value);
    }
  }

  public bool ShowHorizontalVolumes
  {
    get
    {
      return (bool) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzQertKBH63fdmC8AJyRsykJc\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzQertKBH63fdmC8AJyRsykJc\u003D, (object) value);
    }
  }

  public double HorizontalVolumeWidthFraction
  {
    get
    {
      return (double) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzItT23YVSDC4D2EO\u0024iA\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzItT23YVSDC4D2EO\u0024iA\u003D\u003D, (object) value);
    }
  }

  public Brush VolumeBarsBrush
  {
    get
    {
      return (Brush) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzkZL83tvq\u0024ktX);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzkZL83tvq\u0024ktX, (object) value);
    }
  }

  public Color VolBarsFontColor
  {
    get
    {
      return (Color) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzagbwXpWOZg7bLqSU7A\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzagbwXpWOZg7bLqSU7A\u003D\u003D, (object) value);
    }
  }

  public bool \u0023\u003DzRNPBYH\u0024r1v_XVePnsQd4gRM\u003D()
  {
    return (bool) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz6m31HqKWmW3sf6kpzl00drQ\u003D);
  }

  public void \u0023\u003Dz2Hom_KjPHnB5jKfkq1g_w6U\u003D(bool _param1)
  {
    this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz6m31HqKWmW3sf6kpzl00drQ\u003D, (object) _param1);
  }

  public Color \u0023\u003DzdOa9LxKWm9_R()
  {
    return (Color) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzsiwadMWumB4q);
  }

  public void \u0023\u003DzCAEo7KpUPRGr(Color _param1)
  {
    this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzsiwadMWumB4q, (object) _param1);
  }

  public Color \u0023\u003DzMGEDv1J2E_VL()
  {
    return (Color) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzyQkELvBo\u002432H);
  }

  public void \u0023\u003DzKNu_W\u0024G42yk5(Color _param1)
  {
    this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzyQkELvBo\u002432H, (object) _param1);
  }

  public Color \u0023\u003Dzc3UwYbhl1TD\u0024()
  {
    return (Color) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzd0DbwufxB45U);
  }

  public void \u0023\u003DzLzufL4HhRdm8(Color _param1)
  {
    this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dzd0DbwufxB45U, (object) _param1);
  }

  public Color \u0023\u003DzMrEHemSZ_hHJ()
  {
    return (Color) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzMymlj1BxH8MY);
  }

  public void \u0023\u003Dz2DFaO_kh9og7(Color _param1)
  {
    this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzMymlj1BxH8MY, (object) _param1);
  }

  public Decimal? PriceStep
  {
    get
    {
      return (Decimal?) this.GetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzclgWzqVX9aMz0ymbjg\u003D\u003D);
    }
    set
    {
      this.SetValue(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzclgWzqVX9aMz0ymbjg\u003D\u003D, (object) value);
    }
  }

  protected TimeSpan? \u0023\u003DzbEc2QrSSD6cXzW5oVw\u003D\u003D()
  {
    return ((\u0023\u003DztorG3HTUDpMsfjPqFEEe9HUBXNG1JlzD7u56rrBDlcZ3bDSt5iNajas\u003D) this.DataSeries)?.get_Timeframe();
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz__R3\u0024ryThR5H(
    Point _param1,
    double _param2,
    bool _param3)
  {
    return base.\u0023\u003Dz__R3\u0024ryThR5H(_param1, _param2, false);
  }

  private static object \u0023\u003DzHJJ8vuq4bO\u0024yTwjWdw\u003D\u003D(
    DependencyObject _param0,
    object _param1)
  {
    double num = (double) _param1;
    if (num < 0.0)
      return (object) 0.0;
    return num <= 1.0 ? _param1 : (object) 1.0;
  }

  protected abstract void \u0023\u003DzKsKC4kB3l9RI(
    IRenderContext2D _param1,
    IRenderPassData _param2);

  protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs _param1)
  {
    base.OnPropertyChanged(_param1);
    if (_param1.Property != Control.FontFamilyProperty && _param1.Property != Control.FontSizeProperty && _param1.Property != Control.FontWeightProperty)
      return;
    this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D = (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D) null;
    this.\u0023\u003Dzmf\u0024vfR3OJQU9();
  }

  protected override void \u0023\u003Dz_mrkCOu7iZTY(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    if (_param2.\u0023\u003DzDoU1CJhSUWFV())
      throw new NotSupportedException("vertical charts not supported");
    if (this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D().\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D() || this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D().\u0023\u003DzTlJPop1Rus3dbxPf\u0024iJeyAQ\u003D())
      throw new NotSupportedException("flipped axes not supported");
    if (this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI().\u0023\u003DzlpVGw6E\u003D() < 1 || !(this.DataSeries is TimeframeSegmentDataSeries))
      return;
    if (this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D == null)
    {
      string str = this.FontFamily != null ? this.FontFamily.Source : "Tahoma";
      float num = ((float) this.FontSize).Round(0.5f);
      FontWeight fontWeight = this.FontWeight;
      this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D(_param1, str, num, fontWeight);
    }
    this.\u0023\u003DzKsKC4kB3l9RI(_param1, _param2);
    if (this.ShowHorizontalVolumes)
      this.\u0023\u003Dzn9I1y8FguWK2lTmZLQ\u003D\u003D(_param1, _param2);
    else
      this.\u0023\u003Dzwzwqm6ek0X1oFaJOBAtZrz4\u003D = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
  }

  protected static void \u0023\u003DzIhSOYOaWRQ6n(
    IRenderContext2D _param0,
    \u0023\u003DzwiFpns0jAJgM6CtgGDKjwZ2s36fn39ERfeUyF1co1A56IluL6N4L8CSqVgQQ _param1,
    Point _param2,
    Point _param3,
    int _param4,
    int _param5,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param6,
    \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J _param7,
    IBrush2D _param8)
  {
    if (_param2.X >= _param3.X || _param2.Y >= _param3.Y)
      return;
    double num1 = _param3.X - _param2.X;
    double num2 = _param3.Y - _param2.Y;
    double num3 = (_param3.X - _param2.X) / (double) _param4;
    double num4 = (_param3.Y - _param2.Y) / (double) _param5;
    if (num1 > 1.0 && num2 > 1.0)
    {
      if (num3 > 2.0 && num4 > 2.0)
      {
        for (int index = 1; index < _param4; ++index)
        {
          double num5 = _param2.X + (double) index * num3;
          _param1.\u0023\u003Dzk8_eoWQ\u003D(new Point(num5, _param2.Y), new Point(num5, _param3.Y), _param7);
        }
        for (int index = 1; index < _param5; ++index)
        {
          double num6 = _param2.Y + (double) index * num4;
          _param1.\u0023\u003Dzk8_eoWQ\u003D(new Point(_param2.X, num6), new Point(_param3.X, num6), _param7);
        }
      }
      else
        _param0.\u0023\u003DzVRUUvzhAr5SR(_param8, _param2, _param3, 0.0);
    }
    _param1.\u0023\u003Dz7zUbWtTKc3tA(_param6, _param2, _param3);
  }

  protected static void \u0023\u003Dz9oVGnVjC1spU(
    List<\u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB> _param0,
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] _param1,
    int _param2,
    TimeSpan _param3)
  {
    _param0.Clear();
    Tuple<DateTime, DateTime, long> timeframePeriod = TimeframeSegmentDataSeries.GetTimeframePeriod(_param1[_param2].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dzg86amuQ\u003D(), _param3);
    do
    {
      _param0.Add(_param1[_param2]);
    }
    while (_param2 < _param1.Length - 1 && !(_param1[++_param2].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003Dzg86amuQ\u003D() >= timeframePeriod.Item2));
  }

  private void \u0023\u003Dzn9I1y8FguWK2lTmZLQ\u003D\u003D(
    IRenderContext2D _param1,
    IRenderPassData _param2)
  {
    \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D u8drb9mTwzt9HClWjy = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D();
    \u0023\u003DzbKeMmKPk2OqoW3MAcU5vNS01UJmP40FPxAl2jmQ\u003D dataSeries = this.DataSeries;
    u8drb9mTwzt9HClWjy.\u0023\u003DzPBKNgzqH_\u0024\u0024_ = dataSeries as TimeframeSegmentDataSeries;
    if (u8drb9mTwzt9HClWjy.\u0023\u003DzPBKNgzqH_\u0024\u0024_ == null)
      return;
    int num1 = this.LocalHorizontalVolumes ? 1 : 0;
    \u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D anaOdfaeo1Ed4fSw = (\u0023\u003Dz2J8xPQFzEv6\u0024SGdBVtIkvMU02SB0BVAuIu3Yy0oUK9bg_GMIO2ANAOdfaeo1Ed4fSw\u003D\u003D) this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzSKfyjpipx8dI();
    u8drb9mTwzt9HClWjy.\u0023\u003DzgSMmZSBP2RDX = anaOdfaeo1Ed4fSw.\u0023\u003DzTmtGqP_rl3YU6gjEDQ\u003D\u003D();
    \u0023\u003DzUTCl8jvgS_4weG5YU7g\u00240QmcKiogQM05LteyR4wgh0miJ9sJkRF4wMmhD3hB[] j9sJkRf4wMmhD3hBArray = anaOdfaeo1Ed4fSw.\u0023\u003Dz_xjf3ZVIHzP_();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    double width = _param1.\u0023\u003Dz8DEW4l1E337F().Width;
    double height = _param1.\u0023\u003Dz8DEW4l1E337F().Height;
    double num2 = Math.Abs(xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hBArray[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D()) - xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(j9sJkRf4wMmhD3hBArray[0].\u0023\u003Dz0IPdd6wsmxZJ().\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D() + u8drb9mTwzt9HClWjy.\u0023\u003DzgSMmZSBP2RDX));
    double num3 = num2 / 2.0;
    u8drb9mTwzt9HClWjy.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D = xkzemsMs5tGkouk5w.\u0023\u003DzACwLhyc\u003D(-num2);
    u8drb9mTwzt9HClWjy.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D = xkzemsMs5tGkouk5w.\u0023\u003DzACwLhyc\u003D(height + num2);
    IEnumerable<double> source = num1 != 0 ? anaOdfaeo1Ed4fSw.\u0023\u003DzQJbf5e\u0024oTzuvDw2moQ\u003D\u003D().Where<double>(new Func<double, bool>(u8drb9mTwzt9HClWjy.\u0023\u003DzaRGXRoZmlum1ArFUPdKhN1HIdaER)) : (IEnumerable<double>) TimeframeSegmentDataSeries.GeneratePrices(u8drb9mTwzt9HClWjy.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D, u8drb9mTwzt9HClWjy.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D, u8drb9mTwzt9HClWjy.\u0023\u003DzgSMmZSBP2RDX);
    KeyValuePair<double, CandlePriceLevel>[] array = (num1 != 0 ? (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D ?? (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D = new Func<double, double>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzKBZNHp1oDCmRkKG885kIhLueFZ1lDhX69w\u003D\u003D)), new Func<double, CandlePriceLevel>(anaOdfaeo1Ed4fSw.\u0023\u003DzydYW1JHaibII1h_UgQ\u003D\u003D)) : (IEnumerable<KeyValuePair<double, CandlePriceLevel>>) source.ToDictionary<double, double, CandlePriceLevel>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D ?? (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D = new Func<double, double>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzetzA7_VqP5PPGCJmkZk73EvpkDxRjOED9w\u003D\u003D)), new Func<double, CandlePriceLevel>(u8drb9mTwzt9HClWjy.\u0023\u003DzAo1\u0024ndOCIEmE3q7iVa0gqqG3ZEgh))).Where<KeyValuePair<double, CandlePriceLevel>>(new Func<KeyValuePair<double, CandlePriceLevel>, bool>(u8drb9mTwzt9HClWjy.\u0023\u003DzTxCbbIqjlYqMrkZh9\u00249pC9BqtVdY)).ToArray<KeyValuePair<double, CandlePriceLevel>>();
    Brush volumeBarsBrush = this.VolumeBarsBrush;
    Color volBarsFontColor = this.VolBarsFontColor;
    LinearGradientBrush linearGradientBrush = volumeBarsBrush as LinearGradientBrush;
    Dictionary<double, Tuple<double, CandlePriceLevel>> dictionary = new Dictionary<double, Tuple<double, CandlePriceLevel>>();
    if (!\u0023\u003DzsIIzg9COgILMyUKVNisy8sT1ePq3.\u0023\u003DzDCv6G5Q\u003D<KeyValuePair<double, CandlePriceLevel>>(array))
      return;
    using (\u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D vQiJuKqUi9jtIaha = new \u0023\u003DzYmjweh1bAvPkbiZkK_vQiJuKQUi9jtIAHA\u003D\u003D(_param1, false, (float) this.StrokeThickness, this.Opacity))
    {
      \u0023\u003DzoiCXU3qThVGehVE_V2hzF44e\u0024nRHwYsZxA33iRU6ID7J rhwYsZxA33iRu6Id7J = vQiJuKqUi9jtIaha.\u0023\u003Dzc8S9rSE\u003D(linearGradientBrush != null ? linearGradientBrush.GradientStops[0].Color : volBarsFontColor);
      IBrush2D xrgcdFbSdWgN9GcT8_1 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(volumeBarsBrush);
      IBrush2D xrgcdFbSdWgN9GcT8_2 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(this.\u0023\u003DzdOa9LxKWm9_R());
      IBrush2D xrgcdFbSdWgN9GcT8_3 = vQiJuKqUi9jtIaha.\u0023\u003DzNryPIU0\u003D(this.\u0023\u003DzMGEDv1J2E_VL());
      double num4 = (double) ((IEnumerable<KeyValuePair<double, CandlePriceLevel>>) array).Max<KeyValuePair<double, CandlePriceLevel>>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzZEdrJU5AGATCLr5zPg\u003D\u003D ?? (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.\u0023\u003DzZEdrJU5AGATCLr5zPg\u003D\u003D = new Func<KeyValuePair<double, CandlePriceLevel>, Decimal>(\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383.SomeMethond0343.\u0023\u003DzysAJjFChL7t5jDsdHzdXQkULmvNn67k19w\u003D\u003D)));
      double volumeWidthFraction = this.HorizontalVolumeWidthFraction;
      double num5 = width * volumeWidthFraction;
      foreach ((double key, CandlePriceLevel candlePriceLevel) in array)
      {
        double num6 = xkzemsMs5tGkouk5w.\u0023\u003DzhL6gsJw\u003D(key) - num3;
        double num7 = num6 + num2;
        Decimal totalVolume = ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
        double num8 = num5 * (double) totalVolume / num4;
        Point point1 = new Point(0.0, num6);
        Point point2 = new Point(num8, num7);
        double num9 = 3.0 * Math.PI / 2.0;
        if (num2 > 1.5)
        {
          _param1.\u0023\u003Dzk8_eoWQ\u003D(rhwYsZxA33iRu6Id7J, point1, new Point(num8, num6));
          if (this.\u0023\u003DzRNPBYH\u0024r1v_XVePnsQd4gRM\u003D() && (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M || ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M))
          {
            double num10 = num8 * (double) (((CandlePriceLevel) ref candlePriceLevel).BuyVolume / totalVolume);
            if (((CandlePriceLevel) ref candlePriceLevel).BuyVolume > 0M)
              _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_2, new Point(0.0, num6 + 1.0), ((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M ? new Point(num10, num7) : point2, num9);
            if (((CandlePriceLevel) ref candlePriceLevel).SellVolume > 0M)
              _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_3, new Point(num10, num6 + 1.0), point2, num9);
          }
          else
            _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_1, new Point(0.0, num6 + 1.0), point2, num9);
        }
        else
          _param1.\u0023\u003DzVRUUvzhAr5SR(xrgcdFbSdWgN9GcT8_1, point1, point2, num9);
        dictionary[key] = Tuple.Create<double, CandlePriceLevel>(num8, candlePriceLevel);
        string str = totalVolume.ToString();
        int num11 = 2;
        Rect rect = new Rect(new Point((double) num11, num6), new Point(num5 + (double) num11, num7));
        (float, FontWeight, bool) tuple = this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzwjCzmT8\u003D(rect.Size, totalVolume.GetDecimalLength(), 9f);
        float num12 = tuple.Item1;
        FontWeight fontWeight = tuple.Item2;
        if (tuple.Item3)
          _param1.\u0023\u003DzI6mwN\u0024I\u003D(str, rect, AlignmentX.Left, AlignmentY.Center, volBarsFontColor, num12, this.\u0023\u003DzYzTHvXwQXYl6LsCc8L5dk8U\u003D.\u0023\u003DzfFpWmUYdz7xm(), fontWeight);
      }
      this.\u0023\u003Dzwzwqm6ek0X1oFaJOBAtZrz4\u003D = dictionary;
    }
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dzr7PRxQcLL3EF(
    Point _param1,
    double _param2,
    \u0023\u003DzNCoz_cr7eiA6K6bzw3PTSVworRoy7o1mkb\u0024GDjE\u003D _param3,
    bool _param4)
  {
    if (!(this.DataSeries is TimeframeSegmentDataSeries dataSeries) || dataSeries.Count < 1)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    double num1 = (double) (this.PriceStep ?? 0.000001M);
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w1 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzALAI0HJjgPAt2SK7K6oMPzM\u003D();
    \u0023\u003DzTNhhT9A_S5PTAzjbiBFcpNIoInlQX1N\u0024OPHOD8Iz0mvW4gRY24UkaXKzemsMS5t\u0024gkouk5w\u003D<double> xkzemsMs5tGkouk5w2 = this.\u0023\u003Dzvbgbx_fEYDj8gNf2vA\u003D\u003D().\u0023\u003DzYYiX3TcVi5rbqTSkh06tXQM\u003D();
    int index = (int) xkzemsMs5tGkouk5w1.\u0023\u003DzACwLhyc\u003D(_param1.X);
    double num2 = xkzemsMs5tGkouk5w2.\u0023\u003DzACwLhyc\u003D(_param1.Y);
    double key = num2.NormalizePrice(num1);
    double num3 = Math.Abs(xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(1.0) - xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D(0.0));
    double num4 = Math.Abs(xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(key) - xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(key + num1));
    if (num3 < 1.0 || num4 < 1.0)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    Tuple<double, CandlePriceLevel> tuple;
    if (this.ShowHorizontalVolumes && this.\u0023\u003Dzwzwqm6ek0X1oFaJOBAtZrz4\u003D.TryGetValue(key, out tuple) && _param1.X <= tuple.Item1)
    {
      \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy = new \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D();
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzOCYm7g4gfYSc(dataSeries.SeriesName);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzQ9xCEGz0Gl\u0024q(dataSeries.DataSeriesType);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzBswzhzuQHrrX((IComparable) key);
      zldchDrVsrVyHh6WyiGy.\u0023\u003DzwhsEO3Tu4\u0024U9(tuple.Item2);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dzn3o1RS9wuET8(true);
      zldchDrVsrVyHh6WyiGy.\u0023\u003Dzo2ftAfxjqC04(new Point(tuple.Item1, xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(key)));
      return zldchDrVsrVyHh6WyiGy;
    }
    if (index < 0 || index >= dataSeries.Count)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    \u0023\u003Dz6SSn5QQkepq6NeBmeacJnNsdAHCdcYM4YGDu_\u0024RkzGkLTdBuqAINYDLZs6uj segment = dataSeries.Segments[index];
    CandlePriceLevel candlePriceLevel = segment.\u0023\u003DzHH6Br74\u003D(num2, num1);
    if (((CandlePriceLevel) ref candlePriceLevel).TotalVolume == 0M)
      return \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D.\u0023\u003Dzz_6Dy9M\u003D;
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy1 = new \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D();
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzOCYm7g4gfYSc(dataSeries.SeriesName);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzQ9xCEGz0Gl\u0024q(dataSeries.DataSeriesType);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz2Iv\u0024sxQuGDBR((IComparable) segment.\u0023\u003Dzg86amuQ\u003D());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzBswzhzuQHrrX((IComparable) key);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzV4wgjRUOXtRf(index);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzwhsEO3Tu4\u0024U9(candlePriceLevel);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzn3o1RS9wuET8(true);
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dzo2ftAfxjqC04(new Point(xkzemsMs5tGkouk5w1.\u0023\u003DzhL6gsJw\u003D((double) index), xkzemsMs5tGkouk5w2.\u0023\u003DzhL6gsJw\u003D(key)));
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz5MmJ0gJc_yvJ((IComparable) segment.\u0023\u003DzJCVDIhjSn3vnyz7CPg\u003D\u003D());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzA4iUVKOE1DJm((IComparable) segment.\u0023\u003Dz\u00247vYCeZPjqodBoaskg\u003D\u003D());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003DzAvOnsWR70_Q9((IComparable) segment.\u0023\u003DzkEdydKqKob5B7GqY\u0024w\u003D\u003D());
    zldchDrVsrVyHh6WyiGy1.\u0023\u003Dz566Xwh\u0024CapeW((IComparable) segment.\u0023\u003DznrHfMbDuUs5Ac94Iyw\u003D\u003D());
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D zldchDrVsrVyHh6WyiGy2 = zldchDrVsrVyHh6WyiGy1;
    return this.\u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(_param1, zldchDrVsrVyHh6WyiGy2, _param2);
  }

  protected override \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D \u0023\u003Dz1SLEyANHenbwANn\u0024\u0024w\u003D\u003D(
    Point _param1,
    \u0023\u003Dzu7d8MJ0yYYsoCxUauUw1x5zQ41nis3yh_pqZLdchDrVSrVYHh6WyiGY\u003D _param2,
    double _param3)
  {
    return _param2;
  }

  protected sealed class \u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D
  {
    private readonly \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D[] \u0023\u003DzEDGaOlhSE7XW;
    private readonly \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D \u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D;
    private readonly \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D \u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D;
    private readonly string \u0023\u003DztSljFjtK7JnB;
    private readonly Dictionary<Tuple<int, int>, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D> \u0023\u003DzBMY\u00244COSpP9\u0024 = new Dictionary<Tuple<int, int>, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D>();
    private readonly Dictionary<float, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D> \u0023\u003DzmwcQnxGZTX62VDWjDA\u003D\u003D = new Dictionary<float, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D>();
    private readonly Stopwatch \u0023\u003DzcUm4iRE\u003D = new Stopwatch();

    public \u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D(
      IRenderContext2D _param1,
      string _param2,
      float _param3,
      FontWeight _param4)
    {
      this.\u0023\u003DzcUm4iRE\u003D.Restart();
      this.\u0023\u003DztSljFjtK7JnB = _param2;
      _param3 = Math.Min(32f, _param3).Round(0.5f);
      this.\u0023\u003DzEDGaOlhSE7XW = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D[1 + (int) Math.Round(((double) _param3 - 7.0) / 0.5)];
      for (float num = 7f; (double) num <= (double) _param3; num += 0.5f)
      {
        FontWeight fontWeight = (double) num <= 8.5 ? FontWeights.ExtraLight : ((double) num <= 10.0 ? FontWeights.Light : _param4);
        Size size = _param1.\u0023\u003DzM2zC99cVJOSN(num, this.\u0023\u003DztSljFjtK7JnB, fontWeight);
        this.\u0023\u003DzEDGaOlhSE7XW[(int) Math.Round(((double) num - 7.0) / 0.5)] = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D(num, fontWeight, size);
      }
      this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D = this.\u0023\u003DzEDGaOlhSE7XW[0];
      \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D[] zEdGaOlhSe7Xw = this.\u0023\u003DzEDGaOlhSE7XW;
      this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D = zEdGaOlhSe7Xw[zEdGaOlhSe7Xw.Length - 1];
      double d1;
      double d2 = d1 = double.MaxValue;
      double a1;
      double a2 = a1 = double.MinValue;
      foreach (\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D zvzdRt5Y in this.\u0023\u003DzEDGaOlhSE7XW)
      {
        Size size = zvzdRt5Y.\u0023\u003Dz8OGuO4J3wG6m();
        double width = size.Width;
        size = zvzdRt5Y.\u0023\u003Dz8OGuO4J3wG6m();
        double height = size.Height;
        if (width < d2)
          d2 = width;
        if (width > a2)
          a2 = width;
        if (height < d1)
          d1 = height;
        if (height > a1)
          a1 = height;
        this.\u0023\u003DzBMY\u00244COSpP9\u0024[Tuple.Create<int, int>((int) Math.Round(width), (int) Math.Round(height))] = zvzdRt5Y;
        this.\u0023\u003DzmwcQnxGZTX62VDWjDA\u003D\u003D[zvzdRt5Y.FontSize] = zvzdRt5Y;
      }
      int num1 = (int) Math.Floor(d2);
      int num2 = (int) Math.Ceiling(a2);
      int num3 = (int) Math.Floor(d1);
      int num4 = (int) Math.Ceiling(a1);
      int[] array = Enumerable.Range(0, this.\u0023\u003DzEDGaOlhSE7XW.Length).Reverse<int>().ToArray<int>();
      \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D qws5VcJaSLcUcBty = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D();
      for (qws5VcJaSLcUcBty.\u0023\u003Dze2ZNMQU\u003D = num1; qws5VcJaSLcUcBty.\u0023\u003Dze2ZNMQU\u003D <= num2; ++qws5VcJaSLcUcBty.\u0023\u003Dze2ZNMQU\u003D)
      {
        \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzAsDBfEiGfnlxs7_0MiqCAxA\u003D eiGfnlxs70MiqCaxA = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzAsDBfEiGfnlxs7_0MiqCAxA\u003D();
        eiGfnlxs70MiqCaxA.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D = qws5VcJaSLcUcBty;
        for (eiGfnlxs70MiqCaxA.\u0023\u003DzZFj5Dy0\u003D = num3; eiGfnlxs70MiqCaxA.\u0023\u003DzZFj5Dy0\u003D <= num4; ++eiGfnlxs70MiqCaxA.\u0023\u003DzZFj5Dy0\u003D)
        {
          Tuple<int, int> key = Tuple.Create<int, int>(eiGfnlxs70MiqCaxA.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dze2ZNMQU\u003D, eiGfnlxs70MiqCaxA.\u0023\u003DzZFj5Dy0\u003D);
          if (!this.\u0023\u003DzBMY\u00244COSpP9\u0024.ContainsKey(key))
            this.\u0023\u003DzBMY\u00244COSpP9\u0024[key] = ((IEnumerable<int>) array).Select<int, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D>(new Func<int, \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D>(this.OnLineOnePropertyChanged)).First<\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D>(new Func<\u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D, bool>(eiGfnlxs70MiqCaxA.OnChartAreaElementsRemovingAt));
        }
      }
      this.\u0023\u003DzcUm4iRE\u003D.Stop();
      \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("Initialized font calculator. Font={0}, MaxSize={1}, MaxWeight={2}, dict.Size={3}, initTime={4:F3}ms", new object[5]
      {
        (object) _param2,
        (object) _param3,
        (object) _param4,
        (object) this.\u0023\u003DzBMY\u00244COSpP9\u0024.Count,
        (object) this.\u0023\u003DzcUm4iRE\u003D.Elapsed.TotalMilliseconds
      });
    }

    public string \u0023\u003DzfFpWmUYdz7xm() => this.\u0023\u003DztSljFjtK7JnB;

    public double \u0023\u003Dz6puXp2RlkrXBtk\u0024DRw\u003D\u003D()
    {
      return this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m().Height;
    }

    public double \u0023\u003DzYu4\u0024yZJXP2Ni2CKGCQ\u003D\u003D()
    {
      return this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m().Width;
    }

    public (float, FontWeight, bool) \u0023\u003DzwjCzmT8\u003D(
      Size _param1,
      int _param2,
      float _param3)
    {
      _param2 = Math.Max(_param2, 1);
      if ((double) _param3 != 0.0)
        _param3 = Math.Min(Math.Max(_param3, 7f), 32f).Round(0.5f);
      try
      {
        double num1 = _param1.Width / (double) _param2;
        double num2 = num1;
        Size size = this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
        double width1 = size.Width;
        if (num2 >= width1)
        {
          double height1 = _param1.Height;
          size = this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
          double height2 = size.Height;
          if (height1 >= height2)
          {
            double num3 = num1;
            size = this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
            double width2 = size.Width;
            if (num3 >= width2)
            {
              double height3 = _param1.Height;
              size = this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
              double height4 = size.Height;
              if (height3 >= height4)
                return (this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.FontSize, this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.\u0023\u003DzmTA5w5GPXfNk(), true);
            }
            double val1 = num1;
            size = this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
            double width3 = size.Width;
            int num4 = (int) Math.Floor(Math.Min(val1, width3));
            double height5 = _param1.Height;
            size = this.\u0023\u003Dzrvi_agq_n0FwCIAA7Q\u003D\u003D.\u0023\u003Dz8OGuO4J3wG6m();
            double height6 = size.Height;
            int num5 = (int) Math.Floor(Math.Min(height5, height6));
            \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D tmaYvnm94O29pOcja = this.\u0023\u003DzBMY\u00244COSpP9\u0024[Tuple.Create<int, int>(num4, num5)];
            if ((double) _param3 == 0.0 || (double) tmaYvnm94O29pOcja.FontSize >= (double) _param3)
              return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.\u0023\u003DzmTA5w5GPXfNk(), true);
            if (!this.\u0023\u003DzmwcQnxGZTX62VDWjDA\u003D\u003D.TryGetValue(_param3, out tmaYvnm94O29pOcja))
              tmaYvnm94O29pOcja = this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D;
            return (tmaYvnm94O29pOcja.FontSize, tmaYvnm94O29pOcja.\u0023\u003DzmTA5w5GPXfNk(), false);
          }
        }
        if ((double) _param3 == 0.0)
          return ();
        \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D tmaYvnm94O29pOcja1;
        if (!this.\u0023\u003DzmwcQnxGZTX62VDWjDA\u003D\u003D.TryGetValue(_param3, out tmaYvnm94O29pOcja1))
          tmaYvnm94O29pOcja1 = this.\u0023\u003DzxTmaYVnm94O29pOCJA\u003D\u003D;
        return (tmaYvnm94O29pOcja1.FontSize, tmaYvnm94O29pOcja1.\u0023\u003DzmTA5w5GPXfNk(), false);
      }
      catch (Exception ex)
      {
        \u0023\u003DzSnHC0BRBQCx0F\u0024gJzRjVTI2frk8jMoa7AO0kEjY6wcnQ6fBfXg\u003D\u003D.\u0023\u003DzFvAsfEI\u003D().\u0023\u003Dz3jAE7bQ\u003D("GetFont({0}x{1}, {2}, {3:0.##}) error: {4}", new object[5]
        {
          (object) _param1.Width,
          (object) _param1.Height,
          (object) _param2,
          (object) _param3,
          (object) ex
        });
        \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D zvzdRt5Y = this.\u0023\u003DzmwcQnxGZTX62VDWjDA\u003D\u003D[Math.Max(_param3, 7f)];
        return (zvzdRt5Y.FontSize, zvzdRt5Y.\u0023\u003DzmTA5w5GPXfNk(), false);
      }
    }

    public bool \u0023\u003Dzyv\u0024EfaBUnbgQ(Size _param1, int _param2)
    {
      (float, FontWeight, bool) tuple = this.\u0023\u003DzwjCzmT8\u003D(_param1, _param2, 0.0f);
      return (double) tuple.Item1 != 0.0 || tuple.Item2 != new FontWeight() || tuple.Item3;
    }

    private \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D OnLineOnePropertyChanged(
      int _param1)
    {
      return this.\u0023\u003DzEDGaOlhSE7XW[_param1];
    }

    private sealed class \u0023\u003DzAsDBfEiGfnlxs7_0MiqCAxA\u003D
    {
      public int \u0023\u003DzZFj5Dy0\u003D;
      public \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D \u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D;

      internal bool OnChartAreaElementsRemovingAt(
        \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.\u0023\u003Dz\u0024AaE22ukwso1AHLPUy8DdSc\u003D.\u0023\u003DzvzdRt5Y\u003D _param1)
      {
        return _param1.\u0023\u003Dz8OGuO4J3wG6m().Width <= (double) this.\u0023\u003Dq2iriNTb7rAhPHinDq54UgqLb2kUlUKGXkBNeEWzP3h0\u003D.\u0023\u003Dze2ZNMQU\u003D && _param1.\u0023\u003Dz8OGuO4J3wG6m().Height <= (double) this.\u0023\u003DzZFj5Dy0\u003D;
      }
    }

    private sealed class \u0023\u003DzVH9kNoQws5VCJa_sLcUcBTY\u003D
    {
      public int \u0023\u003Dze2ZNMQU\u003D;
    }

    private sealed class \u0023\u003DzvzdRt5Y\u003D(
      float _param1,
      FontWeight _param2,
      Size _param3)
    {
      private readonly float \u0023\u003DzItQGboHj57Hj = _param1.Round(0.5f);
      private readonly FontWeight \u0023\u003DzVykjiWPdJqgM = _param2;
      private readonly Size \u0023\u003Dz0\u0024xkWzYoJTNP = _param3;

      public float FontSize => this.\u0023\u003DzItQGboHj57Hj;

      public FontWeight \u0023\u003DzmTA5w5GPXfNk() => this.\u0023\u003DzVykjiWPdJqgM;

      public Size \u0023\u003Dz8OGuO4J3wG6m() => this.\u0023\u003Dz0\u0024xkWzYoJTNP;
    }
  }

  [Serializable]
  private new sealed class SomeClass34343383
  {
    public static readonly \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383 SomeMethond0343 = new \u0023\u003DzfuNSIBalvsZFtWGR3evczvDB6ICgOj5bitY1F73ysBc2wk6CsoQCv63dERVcBTRfw9WRz_f8qIcTicp0wQ\u003D\u003D.SomeClass34343383();
    public static Func<double, double> \u0023\u003DzFJ\u0024eLsYNB1rWEF3RtQ\u003D\u003D;
    public static Func<double, double> \u0023\u003DzQqTi4Ck_kgzYUKbIuw\u003D\u003D;
    public static Func<KeyValuePair<double, CandlePriceLevel>, Decimal> \u0023\u003DzZEdrJU5AGATCLr5zPg\u003D\u003D;

    internal double \u0023\u003DzKBZNHp1oDCmRkKG885kIhLueFZ1lDhX69w\u003D\u003D(double _param1)
    {
      return _param1;
    }

    internal double \u0023\u003DzetzA7_VqP5PPGCJmkZk73EvpkDxRjOED9w\u003D\u003D(double _param1)
    {
      return _param1;
    }

    internal Decimal \u0023\u003DzysAJjFChL7t5jDsdHzdXQkULmvNn67k19w\u003D\u003D(
      KeyValuePair<double, CandlePriceLevel> _param1)
    {
      CandlePriceLevel candlePriceLevel = _param1.Value;
      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume;
    }
  }

  private sealed class \u0023\u003DzHcysU8drb9mTwzt9H\u0024clWJY\u003D
  {
    public double \u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D;
    public double \u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D;
    public TimeframeSegmentDataSeries \u0023\u003DzPBKNgzqH_\u0024\u0024_;
    public double \u0023\u003DzgSMmZSBP2RDX;

    internal bool \u0023\u003DzaRGXRoZmlum1ArFUPdKhN1HIdaER(double _param1)
    {
      return _param1 > this.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D && _param1 < this.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D;
    }

    internal CandlePriceLevel \u0023\u003DzAo1\u0024ndOCIEmE3q7iVa0gqqG3ZEgh(double _param1)
    {
      return this.\u0023\u003DzPBKNgzqH_\u0024\u0024_.GetVolumeByPrice(_param1, this.\u0023\u003DzgSMmZSBP2RDX);
    }

    internal bool \u0023\u003DzTxCbbIqjlYqMrkZh9\u00249pC9BqtVdY(
      KeyValuePair<double, CandlePriceLevel> _param1)
    {
      if (_param1.Key <= this.\u0023\u003DzgP_jWrrjwvBX7hnEEQ\u003D\u003D || _param1.Key >= this.\u0023\u003DzESuEXae5FE6XM9PRpg\u003D\u003D)
        return false;
      CandlePriceLevel candlePriceLevel = _param1.Value;
      return ((CandlePriceLevel) ref candlePriceLevel).TotalVolume > 0M;
    }
  }
}
