using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using fx.Collections;
using DevExpress.Mvvm;
using Ecng.Collections;
using Ecng.Common;

using fx.Common;
using fx.Definitions;
using System.Collections.ObjectModel;
using Ecng.Xaml;

namespace fx.Algorithm
{
    public class WaveTargetsTsoCollection : ThreadSafeObservableCollection<FibLevelInfo>, INotifyPropertyChanged
    {
        private readonly SyncObject _lock = new SyncObject( );

        private ThreadSafeDictionary< Tuple< SR1stType, SR2ndType, SR3rdType >, FibLevelInfo > _supResisMap = new ThreadSafeDictionary< Tuple< SR1stType, SR2ndType, SR3rdType >, FibLevelInfo >( );

        SortedList< double, FibLevelInfo > _sortedList = new SortedList<double, FibLevelInfo>();
        PooledList< double > _sortedListKey = new PooledList< double >( );

        ThreadSafeDictionary< TimeSpan, bool > _indicatorResultsReceived = new ThreadSafeDictionary< TimeSpan, bool >( 8 );

        private PropertyChangedEventHandler _propertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        public WaveTargetsTsoCollection( IListEx<FibLevelInfo> items ) : base( items )
        {
        }

        //public void AddWaveTarget( FibLevelInfo fibLvl )
        //{
        //    InternalAddWaveTarget( fibLvl );
        //}

        ////public void AddSRline( FibLevelInfo sRlevel, decimal priceStep )
        ////{
        ////    InternalAddSRline( sRlevel, priceStep );
        ////}

        //private void InternalAddWaveTarget( FibLevelInfo sRlevel )
        //{
        //    //var key = sRlevel.UniqueKey( );

        //    //sRlevel.PriceStep = priceStep;

        //    //bool addNew = false;

        //    //if ( _supResisMap.ContainsKey( key ) )
        //    //{
        //    //    var srdetail = _supResisMap[ key ];

        //    //    srdetail.CopyChangable( sRlevel );

        //    //    _supResisMap[ key ] = sRlevel;
        //    //}
        //    //else
        //    //{
        //    //    _supResisMap.Add( key, sRlevel );

        //    //    addNew = true;
        //    //}

        //    //if ( addNew )
        //    //{
        //    //    this.Add( sRlevel );
        //    //}

        //    ////if ()
        //    ////_sortedList.Add( sRlevel.SRvalue, sRlevel );

        //    //_sortedListKey = _sortedList.Keys.ToPooledList();

        //    //_sortedList = this.Items.Where( x => x != null ).OrderBy( x => x.SRvalue ).Select( x => x.SRvalue ).ToPooledList();


        //}

        //private void InternalAddSRlines( PooledList<FibLevelInfo> sRlevels, decimal priceStep )
        //{

        //    //lock ( _lock )
        //    //{
        //    //    PooledList< FibLevelInfo > newSRLevels = null;
        //    //    newSRLevels = new PooledList<FibLevelInfo>( this.Items );
        //    //    foreach ( FibLevelInfo sRlevel in sRlevels )
        //    //    {
        //    //        sRlevel.PriceStep = priceStep;

        //    //        var key = sRlevel.UniqueKey( );

        //    //        if ( _supResisMap.ContainsKey( key ) )
        //    //        {
        //    //            var srdetail = _supResisMap[ key ];

        //    //            srdetail.CopyChangable( sRlevel );
        //    //            _supResisMap[ key ] = sRlevel;
        //    //        }
        //    //        else
        //    //        {
        //    //            _supResisMap.Add( key, sRlevel );
        //    //            newSRLevels.Add( sRlevel );
        //    //        }
        //    //    }

        //    //    var a = newSRLevels.OrderBy( i => i.SRvalue ).ToPooledList();


        //    //    this.Items.Clear();
        //    //    this.Items.AddRange( a );

        //    //    _sortedList.Clear();

        //    //    foreach ( FibLevelInfo sRlevel in sRlevels )
        //    //    {
        //    //        _sortedList.Add( sRlevel.SRvalue, sRlevel );
        //    //    }

        //    //    _sortedListKey = _sortedList.Keys.ToPooledList();
        //}
    }

    public class ReverseComparer<T> : IComparer<T>
    {
        public int Compare( T x, T y )
        {
            return Comparer<T>.Default.Compare( y, x );
        }
    }

    //public int GetClosestSrLine( double price )
    //{
    //    if ( _sortedListKey == null )
    //    {
    //        return -1;
    //    }

    //    int index = _sortedListKey.BinarySearch( price );

    //    if ( 0 <= index )
    //    {
    //        return index;
    //    }
    //    else
    //    {
    //        index = ~index;

    //        if ( 0 < index )
    //        {
    //            return index - 1;
    //        }
    //    }

    //    return -1;
    //}
}



