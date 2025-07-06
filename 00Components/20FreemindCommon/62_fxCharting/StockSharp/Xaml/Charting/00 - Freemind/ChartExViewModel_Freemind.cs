using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Serialization;
using MoreLinq;
using SciChart.Charting.Visuals.TradeChart;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm.UI;
using StockSharp.Xaml.Charting.Definitions;
using System.Windows;
using DevExpress.Xpf.Grid;
using SciChart.Charting.Visuals;
using fx.Common;
using StockSharp.Xaml.Charting.HewFibonacci;
using fx.Definitions;
using SciChart.Core.Extensions;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Data.Model;
using fx.Algorithm;
using fx.Bars;


namespace StockSharp.Xaml.Charting
{
    public partial class ChartExViewModel : DevExpress.Mvvm.ViewModelBase, IChart, IPersistable, IThemeableChart
    {
        private PivotPointLevelsAnnotation _monthly;
        private PivotPointLevelsAnnotation _weekly;
        private PivotPointLevelsAnnotation _daily;

        TradingEventSmallView _smallTradingEventView = null;

        private PooledList< double > _soundAlertLevels = new PooledList< double >( );               

        public void CenterViewOnTime(  DateTime selectedBarTime )
        {
            _candleStickUI.CenterViewOnTime( selectedBarTime );           
        }

        public PooledList<double> GetSelectedLinesForSoundAlert()
        {
            _soundAlertLevels.Clear( );

            var anno = _drawSurface.Annotations;

            foreach ( var a in anno )
            {
                if ( a is IfxImportantLevel )
                {
                    var myAnno = ( IfxImportantLevel ) a;

                    _soundAlertLevels.AddRange( myAnno.GetSelectedLines( ) );

                }

                //if ( a is PivotPointLevelsAnnotation )
                //{
                //    var myAnno = ( PivotPointLevelsAnnotation ) a;

                //    _soundAlertLevels.AddRange( myAnno.GetSelectedLines( ) );
                //}
            }

            return _soundAlertLevels;
        }


        public void CenterViewOnIndexNow(  DateTime selectedBarTime )
        {            
            var xAxis       = _drawSurface.XAxises.FirstOrDefault( );
            xAxis.AutoRange = SciChart.Charting.Visuals.Axes.AutoRange.Never;
            
            var calc        = xAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;

            if ( calc != null )
            {
                int x    = ( calc.TransformDataToIndex( selectedBarTime ) );
                int minX = Math.Max( x - _drawSurface.MinimumRange / 2, 0 );
                int maxX = x + _drawSurface.MinimumRange / 2;

                xAxis.VisibleRange.SetMinMax( minX, maxX );                

                return;
            }


        }



        

        public void AddMonthlyPivots( Security selectedSecurity, IList<SRlevel> newLevels, ref SBar lastBar )
        {
            _monthly            = new PivotPointLevelsAnnotation( newLevels, _gripStyle, SelectedSecurity, TimeSpan.FromDays( 30 ) );
            _monthly.XAxisId    = "X";
            _monthly.YAxisId    = "Y";
            _monthly.Tag        = "M1";

            var first           = newLevels[ 0 ];
            var last            = newLevels[ newLevels.Count - 1 ];
            
            _monthly.X1             = first.SRtimeBlock.Start;
            _monthly.Y1             = last.SRvalue; 
            _monthly.X2             = first.SRtimeBlock.End;
            _monthly.Y2             = first.SRvalue;
            _monthly.IsEditable     = true;
            _monthly.IsLocked       = true;

            _monthly.UpdateLastX( ref lastBar );

            _drawSurface.Annotations.Add( _monthly );
        }

        public void ClearMonthlyPivots( )
        {
            _drawSurface.Annotations.Remove( _monthly );
        }

        public void ClearDailyPivots( )
        {
            _drawSurface.Annotations.Remove( _daily );
        }

        public void AddDailyPivots( Security selectedSecurity, IList<SRlevel> newLevels, ref SBar lastBar )
        {
            _daily            = new PivotPointLevelsAnnotation( newLevels, _gripStyle, SelectedSecurity, TimeSpan.FromDays( 1 ) );
            _daily.XAxisId    = "X";
            _daily.YAxisId    = "Y";
            _daily.Tag        = "D1";

            var first         = newLevels[ 0 ];
            var last          = newLevels[ newLevels.Count - 1 ];

            _daily.X1         = first.SRtimeBlock.Start;
            _daily.Y1         = last.SRvalue;
            _daily.X2         = first.SRtimeBlock.End;
            _daily.Y2         = first.SRvalue;
            _daily.IsEditable = true;
            _daily.IsLocked   = true;

            _daily.UpdateLastX( ref lastBar );

            _drawSurface.Annotations.Add( _daily );
        }

        public void ClearWeeklyPivots( )
        {
            _drawSurface.Annotations.Remove( _weekly );
        }

        public void AddWeeklyPivots( Security selectedSecurity, IList<SRlevel> newLevels, ref SBar lastBar )
        {
            _weekly            = new PivotPointLevelsAnnotation( newLevels, _gripStyle, SelectedSecurity, TimeSpan.FromDays( 7 ) );
            _weekly.XAxisId    = "X";
            _weekly.YAxisId    = "Y";
            _weekly.Tag        = "W1";

            var first          = newLevels[ 0 ];
            var last           = newLevels[ newLevels.Count - 1 ];

            _weekly.X1         = first.SRtimeBlock.Start;
            _weekly.Y1         = last.SRvalue;
            _weekly.X2         = first.SRtimeBlock.End;
            _weekly.Y2         = first.SRvalue;
            _weekly.IsEditable = true;
            _weekly.IsLocked = true;

            _weekly.UpdateLastX( ref lastBar );

            _drawSurface.Annotations.Add( _weekly );
        }

        #region Tony
        
        

        public bool CanExecuteAddProgramIndicatorsCommand( Tuple<ChartArea, CandleSeries> tuple )
        {
            return IsProgrammable;
        }

        public bool CanExecuteAddCandlesProgramatically( )
        {
            return IsProgrammable;
        }

        public void ExecuteAddQuotes( ChartArea chartArea, Tuple<double, double> quote )
        {
            AddQuotesEvent?.Invoke( chartArea, quote );
        }

        public bool CanExecuteAddQuotes( )
        {
            return IsProgrammable;
        }

        public bool CanUpdateQuotes( )
        {
            return IsProgrammable;
        }

        public void ShowDivergence( bool show )
        {
            _candleStickUI.ShowDivergence = show;
        }

        public void ShowSmallTradingEvent( bool show )
        {            
            if (show )
            {
                if ( _smallTradingEventView == null )
                {
                    _smallTradingEventView                  = new TradingEventSmallView( );
                    _smallTradingEventView.CoordinateMode   = SciChart.Charting.Visuals.Annotations.AnnotationCoordinateMode.Relative;
                    _smallTradingEventView.AnnotationCanvas = SciChart.Charting.Visuals.Annotations.AnnotationCanvas.BelowChart;
                    _smallTradingEventView.X1               = 0.995;
                    _smallTradingEventView.XAxisId          = "X";
                    _smallTradingEventView.Y1               = 0;
                    _smallTradingEventView.YAxisId          = "Y";

                    _smallTradingEventView.StartItemSource( SelectedSecurity.Code );

                    _drawSurface.Annotations.Add( _smallTradingEventView );
                }                
            }
            else
            {
                PooledList< SciChart.Charting.Visuals.Annotations.IAnnotation > tobeRemoved = new PooledList< SciChart.Charting.Visuals.Annotations.IAnnotation >( );
                foreach ( SciChart.Charting.Visuals.Annotations.IAnnotation a in _drawSurface.Annotations )
                {
                    if (a is TradingEventSmallView )
                    {
                        tobeRemoved.Add( a );
                    }
                }

                foreach ( SciChart.Charting.Visuals.Annotations.IAnnotation iAnnotation in tobeRemoved )
                {
                    _drawSurface.Annotations.Remove( iAnnotation );
                }

                _smallTradingEventView = null;
            }

        }

        public void FifoCapacity( int fifoSize )
        {
            _candleStickUI.FifoCapacity = fifoSize;
        }

        public void WaveScenarioNo( int waveScenarioNo )
        {
            _drawSurface.WaveScenarioNo = waveScenarioNo;
            _candleStickUI.WaveScenarioNo = waveScenarioNo;
        }

        public void ShowGannPriceTime( bool show )
        {
            _candleStickUI.ShowPriceTimeSignal = show;
        }

        public void ShowElliottWave( bool show )
        {
            _candleStickUI.ShowElliottWave = show;
        }

        public void IsSimulation(bool show)
        {
            _candleStickUI.IsSimulation = show;
        }

        public void ShowMonoWave( bool show )
        {
            _candleStickUI.ShowMonoWave = show;
        }

        public void ShowHewDetection( bool show )
        {
            _candleStickUI.ShowHewDetection = show;
        }


        public void ShowWaveImportance( int impt )
        {
            _candleStickUI.WaveImportance = impt;
        }

        public void ShowWaveCycle( ElliottWaveCycle cycle )
        {
            _candleStickUI.WaveCycle = cycle;
        }

        public void ShowCandlePattern( bool show )
        {
            _candleStickUI.ShowCandlePattern = show;
        }

        public void ShowFreemindIndicators( bool show )
        {
            _candleStickUI.ShowIndicatorResult = show;
        }

        public void YAxisAutoRange( bool onOff )
        {
            YAxisIsAutoRange = onOff;
        }

        public void ShowTradingTime( bool show )
        {
            _candleStickUI.ShowTradingTime = show;
        }

        public long SelectedCandleBarTime
        {
            get
            {
                return _candleStickUI.SelectedCandleBarTime;
            }

            set
            {
                _candleStickUI.SelectedCandleBarTime = value;
            }
        }

        public bool HasMultipleBarsHighlighted
        {
            get
            {
                return _drawSurface.HasMultipleBarsHighlighted;
            }
        }

        public PooledList<long> HighlightedBarLinuxTime
        {
            get { return _drawSurface.HighlightedBarLinxTime; }
        }

        public bool IsSpecialBar { get; set; }

        public void LockFibLevelsObject( )
        {
            _candleStickUI.LockFibLevelsObject( );
        }
        

        public void DeleteAllLockFibLevels( )
        {
            _candleStickUI.DeleteAllLockFibLevels( );
        }

        public void RemoveAllEWaves()
        {
            _candleStickUI.RemoveAllEWaves();
        }

        #endregion
    }
}
