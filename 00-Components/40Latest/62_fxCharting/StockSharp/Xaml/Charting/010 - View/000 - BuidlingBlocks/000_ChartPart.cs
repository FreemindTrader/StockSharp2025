using Ecng.Common;
using Ecng.Serialization;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using StockSharp.Charting;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The base class that describes the part of the chart.
/// </summary>
/// <typeparam name="T">The chart element type.</typeparam>
public abstract class ChartPart<T> : Equatable<T>, INotifyPropertyChanging, INotifyPropertyChanged, IChartPart<T>, IPersistable where T : ChartPart<T>
{

    private Guid _guid;

    /// <summary>
    /// Initialize <see cref="T:StockSharp.Xaml.Charting.ChartPart`1"/>.
    /// </summary>
    protected ChartPart() => Id = Guid.NewGuid();

    /// <summary>
    /// Unique ID.
    /// </summary>
    [Browsable( false )]
    public Guid Id
    {
        get
        {
            return _guid;
        }
        internal set
        {
            _guid = value;
        }
    }

    /// <summary>
    /// Load settings.
    /// </summary>
    /// <param name="storage">Settings storage.</param>
    public virtual void Load( SettingsStorage storage )
    {
        Id = storage.GetValue<Guid>( "Id", new Guid() );
    }

    /// <summary>
    /// Save settings.
    /// </summary>
    /// <param name="storage">Settings storage.</param>
    public virtual void Save( SettingsStorage storage )
    {
        storage.SetValue<Guid>( "Id", Id );
    }

    /// <summary>
    /// Compare <see cref="T:StockSharp.Xaml.Charting.ChartComponent`1"/> on the equivalence.
    /// </summary>
    /// <param name="other">Another value with which to compare.</param>
    /// <returns>
    /// <see langword="true"/>, if the specified object is equal to the current object, otherwise, <see
    /// langword="false"/>.
    /// </returns>
    protected override bool OnEquals( T other ) => other.Id == Id;

    /// <summary>
    /// Get the hash code of the object <see cref="T:StockSharp.Xaml.Charting.ChartComponent`1"/>.
    /// </summary>
    /// <returns>A hash code.</returns>
    public override int GetHashCode() => Id.GetHashCode();

    internal virtual T Clone( T other )
    {
        if ( other == default( T ) )
        {
            throw new ArgumentNullException( "elem" );
        }
        other.Id = Id;
        return other;
    }

    /// <summary>
    /// The chart element properties value changing event.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;

    /// <summary>
    /// To call the event <see cref="E:StockSharp.Xaml.Charting.ChartPart`1.PropertyChanging"/>.
    /// </summary>
    /// <param name="name">Property name.</param>
    protected void RaisePropertyChanging( string name )
    {
        PropertyChangingEventHandler changing = PropertyChanging;

        if ( changing == null )
            return;
        DelegateHelper.Invoke( changing, this, name );
    }

    /// <summary>
    /// The chart element properties value change event.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// To call the event <see cref="E:StockSharp.Xaml.Charting.ChartPart`1.PropertyChanged"/>.
    /// </summary>
    /// <param name="name">Property name.</param>
    protected void RaisePropertyChanged( string name )
    {
        var changed = PropertyChanged;

        if ( changed == null )
            return;
        DelegateHelper.Invoke( changed, this, name );
    }

    /// <summary>
    /// The chart element properties value change start event.
    /// </summary>
    public event Action<object, string, object> PropertyValueChanging;

    /// <summary>
    /// To call the event <see cref="E:StockSharp.Xaml.Charting.ChartPart`1.PropertyValueChanging"/>.
    /// </summary>
    /// <param name="name">Property name.</param>
    /// <param name="value">New value of the property.</param>
    protected void RaisePropertyValueChanging( string name, object value )
    {
        PropertyValueChanging?.Invoke( this, name, value );
    }

    /// <summary>
    /// Update field value and raise <see cref="E:StockSharp.Xaml.Charting.ChartPart`1.PropertyChanged"/> event.
    /// </summary>
    /// <param name="field">Field to update.</param>
    /// <param name="value">New value.</param>
    /// <param name="name">Name of the field to update.</param>
    /// <typeparam name="TField">The field type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the field was updated. <see langword="false"/> otherwise.
    /// </returns>
    protected bool SetField<TField>( ref TField field, TField value, string name )
    {
        if ( EqualityComparer<TField>.Default.Equals( field, value ) )
        {
            return false;
        }
        RaisePropertyChanging( name );
        field = value;
        RaisePropertyChanged( name );
        return true;
    }
}
