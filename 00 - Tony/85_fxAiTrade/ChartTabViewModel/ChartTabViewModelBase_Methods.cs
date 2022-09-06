using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using fx.Charting;
using fx.Algorithm;
using fx.Indicators;
using fx.Definitions;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace FreemindAITrade.ViewModels
{
    public partial class ChartTabViewModelBase 
    {
        public TimeSpan ResponsibleTF { get; set; }

        public void Refresh( )
        {
            _chartVM.Refresh( );
        }

        
        public void OnChartTabSelected( )
        {
            if ( NeedToCenterOnBar )
            {
                NeedToCenterOnBar = false;

                var centerIndex = CenterViewOnThisBarTime;
                DateTime selectedBarTime = SelectedCandleBarTime.FromLinuxTime( );


                ChartVM.CenterViewOnTime( selectedBarTime );
            }
        }


        public void CenterViewOnTime( DateTime selectedBarTime )
        {
            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar != SBar.EmptySBar )
            {
                bar.IsSelected = true;

                _chartVM.CenterViewOnTime( selectedBarTime );
            }
        }

        public void StartChartDrawingTimerThread( )
        {
            if ( _drawTimer != null )
            {
                _drawTimer.Activate( );
            }
        }

        public void StopTimerThread( )
        {
            if ( _drawTimer != null )
            {
                _drawTimer.Cancel( );

                _drawTimer = null;
            }
        }

        public void ShowDivergence( bool show )
        {
            _chartVM.ShowDivergence( show );

        }

        

        public void ShowGannPriceTime( bool show )
        {
            _chartVM.ShowGannPriceTime( show );

        }

        public void ShowElliottWave( bool show )
        {
            _chartVM.ShowElliottWave( show );
        }

        public void ShowMonoWave( bool show )
        {
            _chartVM.ShowMonoWave( show );
        }

        public void ShowHewDetection( bool show )
        {
            _chartVM.ShowHewDetection( show );
        }



        public void ShowFreemindIndicators( bool show )
        {
            _chartVM.ShowFreemindIndicators( show );
        }

        public void YAxisAutoRange( bool show )
        {
            _chartVM.YAxisAutoRange( show );
        }

        public void ShowTradingTime( bool show )
        {
            _chartVM.ShowTradingTime( show );
        }

        

        public void ShowCandlePattern( bool show )
        {
            _chartVM.ShowCandlePattern( show );
        }

        public void CenterViewOnIndexNow( DateTime selectedBarTime )
        {
            ref SBar bar = ref _bars.GetBarByTime( selectedBarTime );

            if ( bar != SBar.EmptySBar )
            {
                bar.IsSelected = true;

                _chartVM.CenterViewOnIndexNow( selectedBarTime );
            }            
        }

        string _caption;
        public string Caption
        {
            get { return _caption; }
            set { SetValue( ref _caption, value ); }
        }

        object _glyph;
        public object Glyph
        {
            get { return _glyph; }
            set { SetValue( ref _glyph, value ); }
        }

        string _text;
        public string Text
        {
            get { return _text; }
            set { SetValue( ref _text, value ); }
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Tony: I need to change This Chart Tab to be responsible for a new TimeFrame so that it will get new candles to show
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- */
        public void SwitchToTimeFrameX( TimeSpan reponsible )
        {
            Caption = reponsible.ToReadable( );
            Text = String.Format( "Document text ({0})", Caption );
            ResponsibleTF = reponsible;

            _name = GetType( ).GetDisplayName( );
            _bars = SymbolsMgr.Instance.CreateOrGetDatabarRepo( SelectedSecurity, ResponsibleTF );

            var aa = SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( SelectedSecurity );

            if ( aa != null )
            {
                _hews = ( HewManager ) aa.HewManager;
            }
        }

        protected Portfolio _portfolio;

        public Portfolio SelectedPortfolio
        {
            get
            {
                return _portfolio;
            }
            set
            {
                _portfolio = value;
            }

        }

        protected Security _security;

        public Security SelectedSecurity
        {
            get
            {
                return _security;
            }
            set
            {
                _security = value;
            }

        }

        public void RaiseCandleLoadedEvent()
        {
            CandlesLoadedEvent?.Invoke( this );
        }

        public void RaiseTabeActivatedEvent()
        {
            TabActivated?.Invoke( this );
        }

        public void RaiseDoneDownloadBarsEvent( )
        {
            DoneDownloadBarsEvent?.Invoke( this );
        }

        Security _quickOrderSecurity = null;

        public Security QuickOrderSecurity
        {
            get
            {
                return _quickOrderSecurity;
            }

            set
            {
                SetValue(ref _quickOrderSecurity, value);
            }
        }

        public void SetQuickOrderPanel(Security security)
        {
            QuickOrderSecurity = security;
        }

        bool _isPaneLoaded = false;

        [Command]
        public void SetupChart()
        {
            if (!_isPaneLoaded)
            {
                RaiseTabeActivatedEvent();

                Step01_ExecuteAddChartArea();
                _isPaneLoaded = true;
            }
        }

        public virtual void Step01_ExecuteAddChartArea()
        {

        }

        public virtual void Step01_ExecuteAddIndicatorArea()
        {

        }


        public virtual void Step9_OnCandleCommand( CandleCommand cmd, bool endOfBatch )
        {

        }

        public virtual void Step9_OnCandleStruct( ref CandleStruct cmd, bool endOfBatch )
        {

        }

        public virtual void Step3_LoadCandlesFromLocalStorage_NonVisual()
        {

        }

    }
}
