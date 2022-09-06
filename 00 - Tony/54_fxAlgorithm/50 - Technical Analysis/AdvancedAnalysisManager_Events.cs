using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fx.Definitions;
using System.ComponentModel;
using DevExpress.Mvvm;

using fx.Common;
using System.Collections.ObjectModel;
using StockSharp.BusinessEntities;
using fx.Bars;

#pragma warning disable 414

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public sealed partial class AdvancedAnalysisManager
    {
        #region ITechnicalAnalysisSignalProvider 
        public event EventHandler< MacdCrossEventArgs >       MacdCrossEvent;
        public event EventHandler< OscillatorCrossEventArgs > OscillatorCrossEvent;
        public event EventHandler< EmaCrossEventArgs >        EmaCrossEvent;
        public event EventHandler< DivergenceEventArgs >      DivergenceEvent;
        public event EventHandler< TaValueEventArgs >         SmaValueChangedEvent;
        public event EventHandler< MacdValueEventArgs >       MacdValueChangedEvent;
        public event EventHandler< SmaCrossEventArgs >        SmaCrossEvent;
        public event EventHandler< PPChangedEventArgs >       PivotPointChangedEvent;

        #endregion        

        public void AddMacdCrossSignal( MacdCrossEventArgs evts )
        {
            
            var eventList = TradingEventsBindingList;

            eventList.AddMacdCrossSignal( evts.Period, evts.CrossIndex, evts.Direction, evts.BarCount );

            MacdCrossEvent?.Invoke( this, evts );                        
        }

        public void AddOscillatorCrossSignal( OscillatorCrossEventArgs evts )
        {            
            var eventList = TradingEventsBindingList;

            eventList.AddStochasticSignal( evts.Period, evts.CrossIndex, (int) evts.Direction, evts.BarCount );

            OscillatorCrossEvent?.Invoke( this, evts );
        }

        public void AddDivergenceSignal( ref SBar bar, DivergenceEventArgs div )
        {
            var taManager = ( PeriodXTaManager ) GetPeriodXTa( div.Period );
            taManager.AddDivergenceInfo( ref bar, new DivergenceInfo( div.Period, div.Divergence, div.StartIndex, div.StartValue, div.EndIndex, div.EndValue ) );

            DivergenceEvent?.Invoke( this, div );
        }

        public void AddEmaCrossSignal( EmaCrossEventArgs evts )
        {
            var eventList = TradingEventsBindingList;

            eventList.AddEmaCrossSignal( evts.Period, evts.CrossIndex, evts.Direction, evts.BarCount );

            EmaCrossEvent?.Invoke( this, evts );    
        }

        public void AddSmaCrossSignal( SmaCrossEventArgs evts )
        {
            var eventList = TradingEventsBindingList;

            eventList.AddSma55CrossSignal( evts.Period, evts.CrossIndex, evts.Direction );

            SmaCrossEvent?.Invoke( this, evts );            
        }
    }
}
