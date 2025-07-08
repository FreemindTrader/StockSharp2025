
using DevExpress.Mvvm.Native;
using Ecng.Collections;
using Ecng.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class ChartBaseViewModel : NotifiableObject
{    
    private readonly SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>> _propertyChangedMap = new SynchronizedDictionary<INotifyPropertyChanged, Dictionary<string, HashSet<string>>>();

    public event Action<object, string, object> PropertyValueChanging;

    protected void OnPropertyChanging(string propertyName, object propertyValue)
    {
        PropertyValueChanging?.Invoke(this, propertyName, propertyValue);
    }

    protected void SetField<T>(ref T field, T value, string propertyName)
    {
        if(EqualityComparer<T>.Default.Equals(field, value))
        {
            return;
        }

        OnPropertyChanging(propertyName, value);
        field = value;
        NotifyChanged(propertyName);
    }

    protected void MapPropertyChangeNotification(
        INotifyPropertyChanged source,
        string nameFrom,
        params string[ ] namesTo)
    {
        if(namesTo != null && namesTo.Length == 0)
        {
            namesTo = new string[ 1 ] { nameFrom };
        }

        if(_propertyChangedMap == null)
        {
            throw new ArgumentNullException(nameof(_propertyChangedMap));
        }

        Dictionary<string, HashSet<string>> mapping = null;

        if(!_propertyChangedMap.TryGetValue(source, out mapping))
        {
            lock(((ISynchronizedCollection) _propertyChangedMap).SyncRoot)
            {
                if(!_propertyChangedMap.TryGetValue(source, out mapping))
                {
                    source.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
                    mapping = new Dictionary<string, HashSet<string>>();

                    _propertyChangedMap.Add(source, mapping);
                }
            }
        }

        if(mapping == null)
        {
            throw new ArgumentNullException(nameof(mapping));
        }

        HashSet<string> fromToMapping = null;

        if(!mapping.TryGetValue(nameFrom, out fromToMapping))
        {
            lock(mapping)
            {
                if(!mapping.TryGetValue(nameFrom, out fromToMapping))
                {
                    fromToMapping = new HashSet<string>();

                    mapping.Add(nameFrom, fromToMapping);
                }
            }
        }

        fromToMapping.AddRange(namesTo);

        //Action< SynchronizedDictionary< INotifyPropertyChanged, Dictionary< string, PooledSet< string > > > > toBeDone = ( d =>
        //{
        //    CollectionHelper.AddRange( d.SafeAdd( source,
        //                                          p =>
        //                                                {
        //                                                    p.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
        //                                                    return new Dictionary< string, PooledSet< string > >( );
        //                                                } )
        //                                .SafeAdd( nameFrom, s => new PooledSet< string >( ) ), namesTo );
        //} );

        //_propertyChangedMap.SyncDo( toBeDone );
    }

    protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if(_propertyChangedMap == null)
        {
            throw new ArgumentNullException(nameof(_propertyChangedMap));
        }

        var source = (INotifyPropertyChanged)sender;

        Dictionary<string, HashSet<string>> mapping = null;
        HashSet<string> propertiesSet;

        if(!_propertyChangedMap.TryGetValue(source, out mapping))
        {
            return;
        }

        if(!mapping.TryGetValue(e.PropertyName, out propertiesSet))
        {
            return;
        }

        foreach(string property in propertiesSet)
        {
            NotifyChanged(property);
        }
    }
}