using Ecng.Serialization;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using Ecng.Drawing;

namespace StockSharp.Xaml.Charting.IndicatorPainters
{
    [Indicator( typeof( VolumeIndicator ) )]
    public class VolumePainter : BaseChartIndicatorPainter
    {
        private readonly ChartLineElement _upVolume;
        private readonly ChartLineElement _downVolume;

        public VolumePainter( )
        {
            _upVolume = new ChartLineElement( )
            {
                Color = Colors.Green
            };

            _downVolume = new ChartLineElement( )
            {
                Color = Colors.Red
            };

            DownVolume.Style = DrawStyles.Histogram;
            UpVolume.Style = DrawStyles.Histogram;

            AddChildElement( UpVolume );
            AddChildElement( DownVolume );
        }

        [Display( Description = "Str2049", Name = "Str2035", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement UpVolume
        {
            get
            {
                return _upVolume;
            }
        }

        [Display( Description = "Str2050", Name = "Str2037", ResourceType = typeof( LocalizedStrings ) )]
        public ChartLineElement DownVolume
        {
            get
            {
                return _downVolume;
            }
        }

        protected override bool OnDraw( )
        {
            VolumeIndicator indicator = ( VolumeIndicator )Indicator;
            return ( DrawValues( indicator, UpVolume, i => {
                                                                if ( !IsUpOrDown( i ) )
                                                                {
                                                                    return double.NaN;
                                                                }

                                                                return Decimal.ToDouble( i.GetValue<Decimal>( ) );
                                                            }   
                                ) |
                    DrawValues( indicator, DownVolume, i => {
                                                                if ( !IsUpOrDown( i ) )
                                                                {
                                                                    return Decimal.ToDouble( i.GetValue<Decimal>( ) );
                                                                }

                                                                return double.NaN;
                                                            }
                              )
                  );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            UpVolume.Load( storage.GetValue<SettingsStorage>( "UpVolume", null ) );
            DownVolume.Load( storage.GetValue<SettingsStorage>( "DownVolume", null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "UpVolume", PersistableHelper.Save( UpVolume ) );
            storage.SetValue( "DownVolume", PersistableHelper.Save( DownVolume ) );
        }

        internal static bool IsUpOrDown( IIndicatorValue indicatorValue )
        {
            if ( indicatorValue == null )
            {
                return false;
            }

            var candle = indicatorValue.GetValue<Candle>( );
            bool? bwCandle = candle.OpenPrice < candle.ClosePrice;

            return bwCandle.GetValueOrDefault( ) & bwCandle.HasValue;
        }       
    }
}
