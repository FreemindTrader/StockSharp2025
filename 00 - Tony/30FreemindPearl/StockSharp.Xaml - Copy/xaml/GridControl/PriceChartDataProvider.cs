

using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace StockSharp.Xaml.GridControl
{
    public class PriceChartDataProvider : Disposable, IDisposable, IPriceChartDataProvider
    {
        private readonly CachedSynchronizedDictionary<Tuple<Security, Level1Fields>, Level1FieldInfo> _dataCache = new CachedSynchronizedDictionary<Tuple<Security, Level1Fields>, Level1FieldInfo>();
        private readonly SyncObject          _lock = new SyncObject();
        private readonly IMarketDataProvider _provider;
        private Timer                        _timer;
        private bool                         _isBusy;
        private TimeSpan                     _refreshPeriod;
        private Level1Fields[]               _level1Fields;

        public PriceChartDataProvider( IMarketDataProvider provider )
        {
            
            IMarketDataProvider imarketDataProvider = provider;
            if ( imarketDataProvider == null )
            {
                throw new ArgumentNullException( nameof( provider ) );
            }

            _provider                = imarketDataProvider;
            _provider.ValuesChanged += OnValueChange;
        }

        public TimeSpan RefreshPeriod
        {
            get
            {
                return _refreshPeriod;
            }
            set
            {
                _refreshPeriod = value;
            }
        }

        public Level1Fields[ ] Fields
        {
            get
            {
                return _level1Fields;
            }
            set
            {
                _level1Fields = value;
            }
        }

        protected override void DisposeManaged( )
        {
            _provider.ValuesChanged -= OnValueChange;
            base.DisposeManaged();
        }

        public ICollection<Tuple<DateTime, Decimal>> Get( Security security, Level1Fields field )
        {
            if ( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            var tuple  = Tuple.Create<Security, Level1Fields>( security, field );
            var output = _dataCache.SyncGet( x => _dataCache.TryGetValue( tuple ));

            if ( output == null )
            {
                return ArrayHelper.Empty<Tuple<DateTime, Decimal>>();
            }

            return output.Values;
        }

        private void OnValueChange( Security security, IEnumerable<KeyValuePair<Level1Fields, object>> changes, DateTimeOffset from, DateTimeOffset to )
        {
            foreach ( KeyValuePair<Level1Fields, object> level1Fields in changes )
            {
                var isDecimal = level1Fields.Value is decimal;

                if ( isDecimal )
                {
                    var newTuple    = Tuple.Create( security, level1Fields.Key );

                    var level1Info  = _dataCache.SafeAdd( newTuple );

                    var addedResult = level1Info.Add( ( Decimal )level1Fields.Value );

                    if ( Fields != null && Fields.Contains( level1Fields.Key ) && addedResult )
                    {
                        lock ( _lock )
                        {
                            if ( !_isBusy )
                            {
                                if ( _timer == null )
                                {
                                    _timer = ThreadingHelper.Interval( ThreadingHelper.Timer( new Action( UpdateCache ) ), RefreshPeriod );
                                }
                            }
                        }
                    }
                }                
            }            
        }

        private void UpdateCache( )
        {
            try
            {                
                lock ( _lock )
                {
                    if ( _isBusy )
                    {
                        return;
                    }

                    _isBusy = true;
                }
                var dirtySymbols = _dataCache.CachedValues.Where( x => x.IsDirty ).ToArray();

                Action toBeDone = delegate()
                {
                    foreach( Level1FieldInfo level1FieldInfo in dirtySymbols )
                    {
                        level1FieldInfo.Update( );
                    }
                };

                GuiDispatcher.GlobalDispatcher.AddSyncAction( toBeDone );

                if ( dirtySymbols.Length != 0 || _timer == null )
                {
                    return;
                }

                _timer.Dispose();
                _timer = null;
            }
            catch ( Exception ex )
            {               
                LoggingHelper.LogError( ex );
            }
            finally
            {
                lock ( _lock )
                {
                    _isBusy = false;
                }
            }
        }        

        private sealed class Level1FieldInfo
        {            
            private readonly List<Tuple<DateTime, Decimal>> _tempList = new List<Tuple<DateTime, Decimal>>();
            private readonly CachedSynchronizedList<Tuple<DateTime, Decimal>> _finalList = new CachedSynchronizedList<Tuple<DateTime, Decimal>>();
            private readonly SyncObject _lock = new SyncObject();
            private Decimal _lastValue;
            private bool _isDirty;

            public ICollection<Tuple<DateTime, Decimal>> Values
            {
                get
                {
                    return _finalList.Cache;
                }
            }

            
            public bool IsDirty
            {
                get { return _isDirty; }
                set
                {
                    _isDirty = value;
                }
            }
                        

            public bool Add( Decimal newItem )
            {
                lock ( _lock )
                {
                    if ( _lastValue == newItem )
                    {
                        return false;
                    }

                    _lastValue = newItem;
                    _tempList.Add( Tuple.Create<DateTime, Decimal>( DateTime.Now, newItem ) );
                    _isDirty =  true;
                }
                return true;
            }

            public void Update( )
            {
                lock ( _lock )
                {
                    _finalList.AddRange( _tempList.CopyAndClear(  ) );
                    _isDirty = false;
                }
            }
        }

        
    }
}
