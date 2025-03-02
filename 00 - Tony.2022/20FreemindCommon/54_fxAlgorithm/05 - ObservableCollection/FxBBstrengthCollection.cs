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

#pragma warning disable 414

namespace fx.Algorithm
{
    public class FxBBstrengthBindingList : ThreadSafeObservableCollection< FxBBstrength >
    {
        private bool _doneIndicatorCalculation = false;

        private ThreadSafeDictionary< TimeSpan, FxBBstrength > _bbStrengthToItem = new ThreadSafeDictionary< TimeSpan, FxBBstrength >( );

        ThreadSafeDictionary< TimeSpan, bool > _indicatorResultsReceived = new ThreadSafeDictionary< TimeSpan, bool >( 8 );

        public FxBBstrengthBindingList( IListEx< FxBBstrength > items ) : base( items )
        {
        }

        public void InitializeEvent( TimeSpan period, int barNumber )
        {
            InternalInitializeEvent( period, barNumber );
        }

        public void InternalInitializeEvent( TimeSpan period, int barNumber )
        {
            var percentage = new FxBBstrength( period, barNumber );

            if( _bbStrengthToItem.ContainsKey( period ) )
            {
                _bbStrengthToItem[ period ] = percentage;
            }
            else
            {
                _bbStrengthToItem.Add( period, percentage );
            }

            Add( percentage );
        }

        public void AddBBstrength( TimeSpan period, int strength, int color )
        {
            InternalAddAddBBstrength( period, strength, color );
        }

        private void InternalAddAddBBstrength( TimeSpan period, int strength, int barColor )
        {
            if( period == TimeSpan.FromDays( 30 ) || period == TimeSpan.FromMinutes( 4 ) || period == TimeSpan.FromTicks( 1 ) )
            {
                return;
            }

            var barPercentage = _bbStrengthToItem[ period ];

            barPercentage.Strength = strength;
            barPercentage.BarColor = barColor;
        }
    }
}

