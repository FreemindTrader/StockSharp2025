// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.SeriesInfoTemplateSelector
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;

namespace fx.Xaml.Charting
{
    public class SeriesInfoTemplateSelector : DataTemplateSelector
    {
        public static readonly DependencyProperty HeatmapSeriesTemplateProperty = DependencyProperty.Register(nameof (HeatmapSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty BandSeries1TemplateProperty = DependencyProperty.Register(nameof (BandSeries1Template), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty BandSeries2TemplateProperty = DependencyProperty.Register(nameof (BandSeries2Template), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty BoxPlotSeriesTemplateProperty = DependencyProperty.Register(nameof (BoxPlotSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty OhlcSeriesTemplateProperty = DependencyProperty.Register(nameof (OhlcSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty HlcSeriesTemplateProperty = DependencyProperty.Register(nameof (HlcSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty OneHundredPercentStackedSeriesTemplateProperty = DependencyProperty.Register(nameof (OneHundredPercentStackedSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));
        public static readonly DependencyProperty TimeframeSegmentSeriesTemplateProperty = DependencyProperty.Register(nameof (TimeframeSegmentSeriesTemplate), typeof (DataTemplate), typeof (SeriesInfoTemplateSelector), new PropertyMetadata(new PropertyChangedCallback(DataTemplateSelector.OnDefautlTemplateDependencyPropertyChanged)));

        public SeriesInfoTemplateSelector()
        {
            this.DefaultStyleKey = ( object ) typeof( SeriesInfoTemplateSelector );
        }

        public DataTemplate HeatmapSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.HeatmapSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.HeatmapSeriesTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate BandSeries1Template
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.BandSeries1TemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.BandSeries1TemplateProperty, ( object ) value );
            }
        }

        public DataTemplate BandSeries2Template
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.BandSeries2TemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.BandSeries2TemplateProperty, ( object ) value );
            }
        }

        public DataTemplate BoxPlotSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.BoxPlotSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.BoxPlotSeriesTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate OhlcSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.OhlcSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.OhlcSeriesTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate HlcSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.HlcSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.HlcSeriesTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate OneHundredPercentStackedSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.OneHundredPercentStackedSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.OneHundredPercentStackedSeriesTemplateProperty, ( object ) value );
            }
        }

        public DataTemplate TimeframeSegmentSeriesTemplate
        {
            get
            {
                return ( DataTemplate ) this.GetValue( SeriesInfoTemplateSelector.TimeframeSegmentSeriesTemplateProperty );
            }
            set
            {
                this.SetValue( SeriesInfoTemplateSelector.TimeframeSegmentSeriesTemplateProperty, ( object ) value );
            }
        }

        public override DataTemplate SelectTemplate( object item, DependencyObject container )
        {
            if ( item is BoxPlotSeriesInfo )
                return this.BoxPlotSeriesTemplate;
            if ( item is OhlcSeriesInfo )
                return this.OhlcSeriesTemplate;
            if ( item is HlcSeriesInfo )
                return this.HlcSeriesTemplate;
            BandSeriesInfo bandSeriesInfo = item as BandSeriesInfo;
            if ( bandSeriesInfo != null )
            {
                if ( !bandSeriesInfo.IsFirstSeries )
                    return this.BandSeries2Template;
                return this.BandSeries1Template;
            }
            if ( item is HeatmapSeriesInfo )
                return this.HeatmapSeriesTemplate;
            if ( item is OneHundredPercentStackedSeriesInfo )
                return this.OneHundredPercentStackedSeriesTemplate;
            if ( item is TimeframeSegmentSeriesInfo )
                return this.TimeframeSegmentSeriesTemplate;
            return base.SelectTemplate( item, container );
        }
    }
}
