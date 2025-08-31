using Ecng.Collections;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Base class for chart related view models. It mainly contains methods for the properties including Setting property value and the event that get triggered
/// when the property is changed.
/// </summary>
public class ChartBaseViewModel : NotifiableObject
{

    private readonly SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> _propertyChangedMap = new SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>();

    /// <summary>
    /// Raised before property value is changed.
    /// </summary>
    public event Action<object, string, object> PropertyValueChanging;

    private void OnPropertyChanging( string propertyName, object propertyValue )
    {
        PropertyValueChanging?.Invoke( this, propertyName, propertyValue );
    }

    /// <summary>
    /// Set property value and raise events.
    /// </summary>
    /// <typeparam name="T">Value type.</typeparam>
    /// <param name="field">Property backing field.</param>
    /// <param name="value">New value.</param>
    /// <param name="propertyName">Name of the property.</param>
    protected bool SetField<T>( ref T field, T value, string propertyName )
    {
        if ( EqualityComparer<T>.Default.Equals( field, value ) )
            return false;
        OnPropertyChanging( propertyName, ( object ) value );
        field = value;
        NotifyChanged( propertyName );
        return true;
    }

    /// <summary>
    /// Helper method to raise property change notifications on this object if the event was raised on another object
    /// <paramref name="source"/>.
    /// </summary>
    /// <param name="source">
    ///
    /// </param>
    /// <param name="nameFrom">
    ///
    /// </param>
    /// <param name="namesTo">
    ///
    /// </param>
    protected void MapPropertyChangeNotification( INotifyPropertyChanged source, string nameFrom, params string[ ] namesTo )
    {
        if ( namesTo != null && namesTo.Length == 0 )
        {
            namesTo = new string[ 1 ] { nameFrom };
        }        

        CollectionHelper.SyncDo( _propertyChangedMap,            
                                                        p =>
                                                        {
                                                            CollectionHelper.AddRange( CollectionHelper.SafeAdd( CollectionHelper.SafeAdd( p, source, pd => {
                                                                                                                                                                pd.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
                                                                                                                                                                return new Dictionary<string, HashSet<string>>();
                                                                                                                                                            } ),
                                                                                                                                                            nameFrom,
                                                                                                                                                            p => new HashSet<string>() ),
                                                                                                                                                            namesTo );
                                                        } );
    }

    private void OnPropertyChanged( object _param1, PropertyChangedEventArgs e )
    {
        CollectionHelper.SyncDo( _propertyChangedMap,            
                                                        p =>
                                                        {                                                                                                                        
                                                            if ( !p.TryGetValue( ( INotifyPropertyChanged ) p, out var propertiesSet ) || !propertiesSet.TryGetValue( e.PropertyName, out var properties ) )
                                                                return;

                                                            foreach ( string property in properties )
                                                            {
                                                                NotifyChanged( property );
                                                            }
                                                        } );
    }
}