using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DevExpress.Mvvm;
using Ecng.Collections;
using Ecng.Xaml;
using fx.Collections;
using fx.Definitions;

namespace fx.Algorithm
{
    public class FxTradingEventsMsgBindingList : ThreadSafeObservableCollection< FxTradingEventsMsg >
    {
        //private bool _doneIndicatorCalculation = false;

        private ThreadSafeDictionary< TimeSpan, FxTradingEventsMsg > _periodToItem = new ThreadSafeDictionary< TimeSpan, FxTradingEventsMsg >( );

        ThreadSafeDictionary< TimeSpan, bool > _indicatorResultsReceived = new ThreadSafeDictionary< TimeSpan, bool >( 8 );

        public FxTradingEventsMsgBindingList(IListEx< FxTradingEventsMsg > items ) : base( items )
        {
        }

        public int FindIndexByPeriod( TimeSpan myPeriod )
        {
            if( Items.Count > 0 )
            {
                var index = IndexOf( this.Where( X => X.Period == myPeriod ).FirstOrDefault( ) );

                return index;
            }

            return -1;
        }

        public void InitializeEvent( TimeSpan timeFrame )
        {
            InternalInitializeEvent( timeFrame );
        }

        public void InternalInitializeEvent( TimeSpan timeFrame )
        {
            var tradingEvent = new FxTradingEventsMsg( timeFrame );

            if( timeFrame != TimeSpan.FromMinutes( 4 ) && timeFrame != TimeSpan.FromTicks( 1 ) )
            {
                _indicatorResultsReceived.Add( timeFrame, true );
            }

            if( _periodToItem.ContainsKey( timeFrame ) )
            {
                _periodToItem[ timeFrame ] = tradingEvent;
            }
            else
            {
                _periodToItem.Add( timeFrame, tradingEvent );
            }

            Add( tradingEvent );
        }

        public void AddMessage( TimeSpan timeFrame, fxMsgType msgtype, string message )
        {
            InternalAddMessage( timeFrame, msgtype, message );
        }

        private void InternalAddMessage( TimeSpan timeFrame,
                                         fxMsgType msgtype,
                                        string message )
        {
            var tradingEvent = _periodToItem[ timeFrame ];

            tradingEvent.LastMessage = message;
            tradingEvent.LastMsgType = msgtype;
        }
    }
}

