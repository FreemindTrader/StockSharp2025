using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml
{
    internal sealed class GridScheduledExecutorService<T, H> where H : class, IModelUpdate
    {
        private readonly BaseGridControl _baseGrid;
        private readonly SynchronizedSet<H> _handlers;
        private object _periodicalAction;
        private readonly ConvertibleObservableCollection<T, H> _itemSource;

        public GridScheduledExecutorService( BaseGridControl baseGrid, Func<T, Action<H>, H> converter )
        {
            if ( baseGrid == null )
            {
                throw new ArgumentNullException( "grid" );
            }

            _baseGrid = baseGrid;
            

            _handlers             = new SynchronizedSet<H>( );
            var itemSource        = new ObservableCollectionEx<H>( );
            _itemSource           = new ConvertibleObservableCollection<T, H>( new ThreadSafeObservableCollection<H>( itemSource ), new Func<T, H>( p => converter( p, new Action<H>( ( _handlers ).Add ) ) ) );
            _baseGrid.Loaded     += new RoutedEventHandler( AddPeriodicAction );
            _baseGrid.Unloaded   += new RoutedEventHandler( RemovePeriodicalAction );
            _baseGrid.ItemsSource = itemSource;
        }

        public ConvertibleObservableCollection<T, H> Source
        {
            get
            {
                return _itemSource;
            }
        }

        public int MaxCount
        {
            get
            {
                return Source.MaxCount;
            }

            set
            {
                Source.MaxCount = value;
            }
        }



        private void AddPeriodicAction( object sender, RoutedEventArgs e )
        {
            _periodicalAction = GuiDispatcher.GlobalDispatcher.AddPeriodicalAction( new Action( UpdateModel ) );
        }

        private void RemovePeriodicalAction( object sender, RoutedEventArgs e )
        {
            if ( _periodicalAction == null )
            {
                return;
            }

            GuiDispatcher.GlobalDispatcher.RemovePeriodicalAction( _periodicalAction );
        }

        private void UpdateModel( )
        {
            if ( _handlers.Count == 0 )
            {
                return;
            }

            var changedProperties = _handlers.SyncGet( ss => ss.CopyAndClear<H>( ) );

            _baseGrid.BeginDataUpdate( );

            try
            {
                foreach ( H u in changedProperties )
                {
                    u.UpdateModel( );
                }
            }
            finally
            {
                _baseGrid.EndDataUpdate( );
            }
        }              
    }
}
