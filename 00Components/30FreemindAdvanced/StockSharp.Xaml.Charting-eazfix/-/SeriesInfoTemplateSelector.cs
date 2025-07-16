// Decompiled with JetBrains decompiler
// Type: -.SeriesInfoTemplateSelector
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System.Diagnostics;
using System.Windows;

#nullable disable
namespace StockSharp.Charting;

public sealed class SeriesInfoTemplateSelector : 
  DataTemplateSelector
{
  
  public static readonly DependencyProperty \u0023\u003Dzwb\u0024PK7dpToEG8k4U8sMMGck\u003D = DependencyProperty.Register(nameof (HeatmapSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz65Bzyn0al\u0024hsexCOwQ\u003D\u003D = DependencyProperty.Register(nameof (BandSeries1Template), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzgPAPsf8N0nMyp5a7nA\u003D\u003D = DependencyProperty.Register(nameof (BandSeries2Template), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dz9AZw7OdUa6oJtZAFRw\u003D\u003D = DependencyProperty.Register(nameof (BoxPlotSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzC3AaW5JpRV47dzI1lA\u003D\u003D = DependencyProperty.Register(nameof (OhlcSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzd4jBbeTPaY\u0024mJnLskQ\u003D\u003D = DependencyProperty.Register(nameof (HlcSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dzfe\u0024ko3ZL7vw86G01kRQRLzirSZJh = DependencyProperty.Register(nameof (OneHundredPercentStackedSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003Dza9CG7f7oerF2rgcRKe1gfOztGAAb = DependencyProperty.Register(nameof (TimeframeSegmentSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));
  
  public static readonly DependencyProperty \u0023\u003DzsrZg_F3foQsz = DependencyProperty.Register(nameof (TransactionSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.\u0023\u003DziqhLe0ar\u0024pN3qpBgGFcDfrI\u003D)));

  public SeriesInfoTemplateSelector()
  {
    this.DefaultStyleKey = (object) typeof (SeriesInfoTemplateSelector);
  }

  public DataTemplate HeatmapSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dzwb\u0024PK7dpToEG8k4U8sMMGck\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dzwb\u0024PK7dpToEG8k4U8sMMGck\u003D, (object) value);
    }
  }

  public DataTemplate BandSeries1Template
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dz65Bzyn0al\u0024hsexCOwQ\u003D\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dz65Bzyn0al\u0024hsexCOwQ\u003D\u003D, (object) value);
    }
  }

  public DataTemplate BandSeries2Template
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003DzgPAPsf8N0nMyp5a7nA\u003D\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003DzgPAPsf8N0nMyp5a7nA\u003D\u003D, (object) value);
    }
  }

  public DataTemplate BoxPlotSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dz9AZw7OdUa6oJtZAFRw\u003D\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dz9AZw7OdUa6oJtZAFRw\u003D\u003D, (object) value);
    }
  }

  public DataTemplate OhlcSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003DzC3AaW5JpRV47dzI1lA\u003D\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003DzC3AaW5JpRV47dzI1lA\u003D\u003D, (object) value);
    }
  }

  public DataTemplate HlcSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dzd4jBbeTPaY\u0024mJnLskQ\u003D\u003D);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dzd4jBbeTPaY\u0024mJnLskQ\u003D\u003D, (object) value);
    }
  }

  public DataTemplate OneHundredPercentStackedSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dzfe\u0024ko3ZL7vw86G01kRQRLzirSZJh);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dzfe\u0024ko3ZL7vw86G01kRQRLzirSZJh, (object) value);
    }
  }

  public DataTemplate TimeframeSegmentSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003Dza9CG7f7oerF2rgcRKe1gfOztGAAb);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003Dza9CG7f7oerF2rgcRKe1gfOztGAAb, (object) value);
    }
  }

  public DataTemplate TransactionSeriesTemplate
  {
    get
    {
      return (DataTemplate) this.GetValue(SeriesInfoTemplateSelector.\u0023\u003DzsrZg_F3foQsz);
    }
    set
    {
      this.SetValue(SeriesInfoTemplateSelector.\u0023\u003DzsrZg_F3foQsz, (object) value);
    }
  }

  public override DataTemplate \u0023\u003Dzmy_tWbS7jzNB(object _param1, DependencyObject _param2)
  {
    switch (_param1)
    {
      case \u0023\u003Dz3RRntx4pzkd854dIVpLK6S_EKy\u0024AtkpA9s\u0024N3N0\u003D _:
        return this.BoxPlotSeriesTemplate;
      case \u0023\u003Dz3HkNAtjftY7KLZeVO1e0c8c41pWQbDKntdB13Yg\u003D _:
        return this.OhlcSeriesTemplate;
      case \u0023\u003DzdU\u0024qxkSrwVqvrc8JS00VEdnlPeUGAD5h8w\u003D\u003D _:
        return this.HlcSeriesTemplate;
      case BandSeriesInfo k3zbmiw1OaRAdtq7psDwa:
        return !k3zbmiw1OaRAdtq7psDwa.IsFirstSeries ? this.BandSeries2Template : this.BandSeries1Template;
      case \u0023\u003DzCp5d2Zte2oCosmmx2S7no7oM806RFMQA4oT0jRI\u003D _:
        return this.HeatmapSeriesTemplate;
      case \u0023\u003DznUYKC7Ax8Zwair3Ru5V4H3DyRXy\u0024crDQ0zcN7c_LKq7HenVQrw\u003D\u003D _:
        return this.OneHundredPercentStackedSeriesTemplate;
      case \u0023\u003DzGULZ_B3lGVEDiq9xPbVQjsPdCs3fSNYVEdhm_bS76Lhc _:
        return this.TimeframeSegmentSeriesTemplate;
      case TransactionSeriesRolloverLabel _:
        return this.TransactionSeriesTemplate;
      default:
        return base.\u0023\u003Dzmy_tWbS7jzNB(_param1, _param2);
    }
  }
}
