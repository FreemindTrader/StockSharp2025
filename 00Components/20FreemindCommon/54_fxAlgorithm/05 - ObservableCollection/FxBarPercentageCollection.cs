using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Ecng.Collections;
using Ecng.Xaml;
using fx.Collections;
using fx.Definitions;

namespace fx.Algorithm
{
    public class FxBarPercentageBindingList : ThreadSafeObservableCollection< FxBarPercentage >
    {
        public FxBarPercentageBindingList( IListEx< FxBarPercentage > items ) : base( items )
        {
        }        

        private ThreadSafeDictionary< int, FxBarPercentage > _barNumberToItem          = new ThreadSafeDictionary< int, FxBarPercentage >( );

        private ThreadSafeDictionary< TimeSpan, bool >       _indicatorResultsReceived = new ThreadSafeDictionary< TimeSpan, bool >( 8 );

        public void InitializeEvent( int barNumber )
        {
            InternalInitializeEvent( barNumber );
        }

        public void InternalInitializeEvent( int barNumber )
        {
            var percentage = new FxBarPercentage( barNumber );

            if( _barNumberToItem.ContainsKey( barNumber ) )
            {
                _barNumberToItem[ barNumber ] = percentage;
            }
            else
            {
                _barNumberToItem.Add( barNumber, percentage );
            }

            Add( percentage );
        }

        public void AddBarPercentage( int barNumber, double percentage, int color )
        {
            InternalAddBarPercentage( barNumber, percentage, color );
        }

        private void InternalAddBarPercentage( int barNumber, double percentage, int barColor )
        {
            var barPercentage        = _barNumberToItem[ barNumber ];

            barPercentage.BarNumber  = barNumber;
            barPercentage.BarPercent = percentage;
            barPercentage.BarColor   = barColor;
        }
    }
}

