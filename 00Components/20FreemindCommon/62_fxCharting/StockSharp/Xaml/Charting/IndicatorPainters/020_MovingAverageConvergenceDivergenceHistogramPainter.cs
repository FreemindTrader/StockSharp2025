// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.MovingAverageConvergenceDivergenceHistogram" />.
/// </summary>
[Indicator(typeof(MovingAverageConvergenceDivergenceHistogram))]
public class MovingAverageConvergenceDivergenceHistogramPainter :
  BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>
{

    private readonly IChartLineElement _macd;

    private readonly IChartLineElement _signal;

    private readonly IChartLineElement _histogram;

    /// <summary>Create instance.</summary>
    public MovingAverageConvergenceDivergenceHistogramPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<MovingAverageConvergenceDivergenceHistogram>.GetColorProvider();
        this._macd = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._signal = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._histogram = (IChartLineElement)new ChartLineElement()
        {
            Style = DrawStyles.Histogram
        };
        this.AddChildElement((IChartElement)this.Macd);
        this.AddChildElement((IChartElement)this.Signal);
        this.AddChildElement((IChartElement)this.Histogram);
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Macd" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "MACD", Description = "SignalMaDesc")]
    public IChartLineElement Macd => this._macd;


    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Signal" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "SignalMa", Description = "SignalMaDesc")]
    public IChartLineElement Signal => this._signal;


    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.MovingAverageConvergenceDivergenceHistogramPainter.Histogram" /> line.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Histogram", Description = "HistogramDesc")]
    public IChartLineElement Histogram => this._histogram;

    protected override bool OnDraw(
      MovingAverageConvergenceDivergenceHistogram indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return ( this.DrawValues(data[(IIndicator)indicator.Macd], (IChartElement)this.Macd)
             | ( this.DrawValues(data[(IIndicator)indicator.SignalMa], (IChartElement)this.Signal) )
             | ( this.DrawValues(data[(IIndicator)indicator.Macd], data[(IIndicator)indicator.SignalMa], (IChartElement)this.Histogram, (d1, d2) => d1 - d2) ) );
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.Macd, storage, "Macd");
        PersistableHelper.Load((IPersistable)this.Signal, storage, "Signal");
        PersistableHelper.Load((IPersistable)this.Histogram, storage, "Histogram");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Macd", PersistableHelper.Save((IPersistable)this.Macd));
        storage.SetValue<SettingsStorage>("Signal", PersistableHelper.Save((IPersistable)this.Signal));
        storage.SetValue<SettingsStorage>("Histogram", PersistableHelper.Save((IPersistable)this.Histogram));
    }
}


//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;
//using Ecng.Drawing;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( MovingAverageConvergenceDivergenceHistogram ) )]
//    public class MovingAverageConvergenceDivergenceHistogramPainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _macd;
//        private readonly ChartLineElement _signal;
//        private readonly ChartLineElement _histogram;

//        public MovingAverageConvergenceDivergenceHistogramPainter( )
//        {
//            _macd = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };

//            _signal = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };

//            _histogram = new ChartLineElement( )
//            {
//                Style = DrawStyles.Histogram,
//                Color = Colors.LightGray
//            };

//            AddChildElement( Macd );
//            AddChildElement( Signal );
//            AddChildElement( Histogram );
//        }

//        [DisplayName( "MACD" )]
//        public ChartLineElement Macd
//        {
//            get
//            {
//                return _macd;
//            }
//        }

//        [Display( Description = "Str805", Name = "Str804", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement Signal
//        {
//            get
//            {
//                return _signal;
//            }
//        }

//        [DisplayName( "MACD (Hist)" )]
//        public ChartLineElement Histogram
//        {
//            get
//            {
//                return _histogram;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            MovingAverageConvergenceDivergenceHistogram indicator = ( MovingAverageConvergenceDivergenceHistogram )Indicator;

//            return ( ( DrawValues( indicator.Macd, indicator.SignalMa, Histogram, ( d1, d2 ) => d1 - d2 ) ) | DrawValues( indicator.Macd, Macd ) | DrawValues( indicator.SignalMa, Signal ) );
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            Macd.Load( storage.GetValue< SettingsStorage >( "Macd", null ) );
//            Signal.Load( storage.GetValue< SettingsStorage >( "Signal", null ) );
//            Histogram.Load( storage.GetValue< SettingsStorage >( "Histogram", null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "Macd", Macd.Save( ) );
//            storage.SetValue( "Signal", Signal.Save( ) );
//            storage.SetValue( "Histogram", Histogram.Save( ) );
//        }        
//    }
//}
