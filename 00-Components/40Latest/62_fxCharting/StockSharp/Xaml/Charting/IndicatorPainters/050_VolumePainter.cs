using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.VolumeIndicator" />.
/// </summary>

[Indicator(typeof(VolumeIndicator))]
public class VolumePainter : BaseChartIndicatorPainter<VolumeIndicator>
{

    private readonly IChartLineElement _upVolume;

    private readonly IChartLineElement _downVolume;

    /// <summary>Create instance.</summary>
    public VolumePainter()
    {
        _upVolume = (IChartLineElement)new ChartLineElement()
        {
            Color = Colors.Green
        };

        _downVolume = (IChartLineElement)new ChartLineElement()
        {
            Color = Colors.Red
        };

        UpVolume.Style = DownVolume.Style = DrawStyles.Histogram;
        AddChildElement((IChartElement)UpVolume);
        AddChildElement((IChartElement)DownVolume);
    }

    /// <summary>Up line color.</summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "UpColor", Description = "UpCandleColor")]
    public IChartLineElement UpVolume => _upVolume;

    /// <summary>Down line color.</summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "DownColor", Description = "DownCandleColor")]
    public IChartLineElement DownVolume => _downVolume;

    protected override bool OnDraw(VolumeIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return ( ( DrawValues(data[(IIndicator)indicator], (IChartElement)UpVolume, p => !VolumePainter.IsUpOrDown(p.Value) ? double.NaN : (double)p.Value.ToDecimal()) )
                   | ( DrawValues(data[(IIndicator)indicator], (IChartElement)DownVolume, p => !VolumePainter.IsUpOrDown(p.Value) ? (double)p.Value.ToDecimal() : double.NaN) ) );
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)UpVolume, storage, "UpVolume");
        PersistableHelper.Load((IPersistable)DownVolume, storage, "DownVolume");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("UpVolume", PersistableHelper.Save((IPersistable)UpVolume));
        storage.SetValue<SettingsStorage>("DownVolume", PersistableHelper.Save((IPersistable)DownVolume));
    }

    public static bool IsUpOrDown(IIndicatorValue indicatorValue)
    {
        if ( indicatorValue == null )
            return false;
        ICandleMessage icandleMessage = indicatorValue.GetValue<ICandleMessage>();
        return icandleMessage == null || icandleMessage.OpenPrice<icandleMessage.ClosePrice;
    }
}


//using Ecng.Serialization;
//using StockSharp.Algo.Candles;
//using StockSharp.Algo.Indicators;
//using StockSharp.Localization;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Media;
//using Ecng.Drawing;

//namespace StockSharp.Xaml.Charting.IndicatorPainters
//{
//    [Indicator( typeof( VolumeIndicator ) )]
//    public class VolumePainter : BaseChartIndicatorPainter
//    {
//        private readonly ChartLineElement _upVolume;
//        private readonly ChartLineElement _downVolume;

//        public VolumePainter( )
//        {
//            _upVolume = new ChartLineElement( )
//            {
//                Color = Colors.Green
//            };

//            _downVolume = new ChartLineElement( )
//            {
//                Color = Colors.Red
//            };

//            DownVolume.Style = DrawStyles.Histogram;
//            UpVolume.Style = DrawStyles.Histogram;

//            AddChildElement( UpVolume );
//            AddChildElement( DownVolume );
//        }

//        [Display( Description = "Str2049", Name = "Str2035", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement UpVolume
//        {
//            get
//            {
//                return _upVolume;
//            }
//        }

//        [Display( Description = "Str2050", Name = "Str2037", ResourceType = typeof( LocalizedStrings ) )]
//        public ChartLineElement DownVolume
//        {
//            get
//            {
//                return _downVolume;
//            }
//        }

//        protected override bool OnDraw( )
//        {
//            VolumeIndicator indicator = ( VolumeIndicator )Indicator;
//            return ( DrawValues( indicator, UpVolume, i => {
//                                                                if ( !IsUpOrDown( i ) )
//                                                                {
//                                                                    return double.NaN;
//                                                                }

//                                                                return Decimal.ToDouble( i.GetValue<Decimal>( ) );
//                                                            }   
//                                ) |
//                    DrawValues( indicator, DownVolume, i => {
//                                                                if ( !IsUpOrDown( i ) )
//                                                                {
//                                                                    return Decimal.ToDouble( i.GetValue<Decimal>( ) );
//                                                                }

//                                                                return double.NaN;
//                                                            }
//                              )
//                  );
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            UpVolume.Load( storage.GetValue<SettingsStorage>( "UpVolume", null ) );
//            DownVolume.Load( storage.GetValue<SettingsStorage>( "DownVolume", null ) );
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            base.Save( storage );
//            storage.SetValue( "UpVolume", PersistableHelper.Save( UpVolume ) );
//            storage.SetValue( "DownVolume", PersistableHelper.Save( DownVolume ) );
//        }

//        internal static bool IsUpOrDown( IIndicatorValue indicatorValue )
//        {
//            if ( indicatorValue == null )
//            {
//                return false;
//            }

//            var candle = indicatorValue.GetValue<Candle>( );
//            bool? bwCandle = candle.OpenPrice < candle.ClosePrice;

//            return bwCandle.GetValueOrDefault( ) & bwCandle.HasValue;
//        }       
//    }
//}
