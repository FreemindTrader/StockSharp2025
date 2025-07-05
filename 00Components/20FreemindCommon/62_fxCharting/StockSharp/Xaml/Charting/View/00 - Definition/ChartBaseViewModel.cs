using Ecng.Collections;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;

namespace fx.Charting
{
    public class ChartBaseViewModel : NotifiableObject
    {
        private readonly SynchronizedDictionary< INotifyPropertyChanged, PooledDictionary< string, PooledSet< string > > > _propertyChangedMap = new SynchronizedDictionary< INotifyPropertyChanged, PooledDictionary< string, PooledSet< string > > >( );

        public event Action< object, string, object > PropertyValueChanging;

        protected void OnPropertyChanging( string propertyName, object propertyValue )
        {
            PropertyValueChanging?.Invoke( this, propertyName, propertyValue );
        }

        protected void SetField< T >( ref T field, T value, string propertyName )
        {
            if( EqualityComparer< T >.Default.Equals( field, value ) )
            {
                return;
            }

            OnPropertyChanging( propertyName, value );
            field = value;
            NotifyChanged( propertyName );
        }

        protected void MapPropertyChangeNotification( INotifyPropertyChanged source, string nameFrom, params string[ ] namesTo )
        {
            if( namesTo != null && namesTo.Length == 0 )
            {
                namesTo = new string[ 1 ] { nameFrom };
            }

            if( _propertyChangedMap == null )
            {
                throw new ArgumentNullException( nameof( _propertyChangedMap ) );
            }

            PooledDictionary< string, PooledSet< string > > mapping = null;

            if( !_propertyChangedMap.TryGetValue( source, out mapping ) )
            {
                lock( ( ( ISynchronizedCollection )_propertyChangedMap ).SyncRoot )
                {
                    if( !_propertyChangedMap.TryGetValue( source, out mapping ) )
                    {
                        source.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
                        mapping = new PooledDictionary< string, PooledSet< string > >( );

                        _propertyChangedMap.Add( source, mapping );
                    }
                }
            }

            if( mapping == null )
            {
                throw new ArgumentNullException( nameof( mapping ) );
            }

            PooledSet< string > fromToMapping = null;

            if( !mapping.TryGetValue( nameFrom, out fromToMapping ) )
            {
                lock( mapping )
                {
                    if( !mapping.TryGetValue( nameFrom, out fromToMapping ) )
                    {
                        fromToMapping = new PooledSet< string >( );

                        mapping.Add( nameFrom, fromToMapping );
                    }
                }
            }

            fromToMapping.AddRange( namesTo );

            //Action< SynchronizedDictionary< INotifyPropertyChanged, PooledDictionary< string, PooledSet< string > > > > toBeDone = ( d =>
            //{
            //    CollectionHelper.AddRange( d.SafeAdd( source,
            //                                          p =>
            //                                                {
            //                                                    p.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
            //                                                    return new PooledDictionary< string, PooledSet< string > >( );
            //                                                } )
            //                                .SafeAdd( nameFrom, s => new PooledSet< string >( ) ), namesTo );
            //} );

            //_propertyChangedMap.SyncDo( toBeDone );
        }

        protected void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if( _propertyChangedMap == null )
            {
                throw new ArgumentNullException( nameof( _propertyChangedMap ) );
            }

            var source = ( INotifyPropertyChanged )sender;

            PooledDictionary< string, PooledSet< string > > mapping = null;
            PooledSet< string > propertiesSet;

            if( !_propertyChangedMap.TryGetValue( source, out mapping ) )
            {
                return;
            }

            if( !mapping.TryGetValue( e.PropertyName, out propertiesSet ) )
            {
                return;
            }

            foreach( string property in propertiesSet )
            {
                NotifyChanged( property );
            }

           
        }
    }
}
