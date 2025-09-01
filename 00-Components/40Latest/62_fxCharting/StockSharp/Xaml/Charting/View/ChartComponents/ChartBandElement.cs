using Ecng.Drawing;
using DevExpress.Dialogs.Core.View;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>The chart element representing a band.</summary>
public sealed class ChartBandElement : ChartComponentViewModel<ChartBandElement>,
                                          IChartElement,
                                          IChartPart<IChartElement>,
                                          INotifyPropertyChanged,
                                          INotifyPropertyChanging,
                                          IPersistable,
                                          IChartBandElement,
                                          IChartComponent,
                                          IChartElementUiDomain
{

    private DrawStyles _drawStyle = DrawStyles.Band;

    private readonly ChartLineElement _lineOne;

    private readonly ChartLineElement _lineTwo;

    private ChartElementUiDomain _baseViewModel;

    /// <summary>Create instance.</summary>
    public ChartBandElement()
    {
        _lineOne = new ChartLineElement()
        {
            Color = Colors.DarkGreen,
            AdditionalColor = Colors.DarkGreen.ToTransparent( ( byte ) 50 )
        };
        _lineTwo = new ChartLineElement()
        {
            Color = Colors.DarkGreen,
            AdditionalColor = Colors.DarkGreen.ToTransparent( ( byte ) 50 )
        };
        Line1.PropertyChanged += new PropertyChangedEventHandler( OnLineOnePropertyChanged );
        AddChildElement( ( IChartElement ) Line1, true );
        AddChildElement( ( IChartElement ) Line2, true );
    }

    Color IChartElementUiDomain.Color
    {
        get
        {
            return Line1.AdditionalColor;
        }
    }

    /// <summary>
    /// The band drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Band" />. Can also be <see cref="F:Ecng.Drawing.DrawStyles.BandOneValue" />.
    /// </summary>
    [Browsable( false )]
    public DrawStyles Style
    {
        get => _drawStyle;
        set
        {
            if ( _drawStyle == value )
                return;
            if ( value != DrawStyles.Band && value != DrawStyles.BandOneValue )
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnsupportedType, new object[ 1 ]
                {
          (object) value
                } ) );
            RaisePropertyValueChanging( nameof( Style ), ( object ) value );
            _drawStyle = value;
            RaisePropertyChanged( nameof( Style ) );
        }
    }

    Ecng.Drawing.DrawStyles IChartBandElement.Style
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line1" />.
    /// </summary>
    public ChartLineElement Line1 => _lineOne;

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line2" />.
    /// </summary>
    public ChartLineElement Line2 => _lineTwo;

    IChartLineElement IChartBandElement.Line1 => ( IChartLineElement ) Line1;

    IChartLineElement IChartBandElement.Line2 => ( IChartLineElement ) Line2;

    

    public override bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
    {
        return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM _param1 )
    {
        // BUG: need to Decode ScichartSurfaceMVVM.cs first

        throw new NotImplementedException();
        
        //_baseViewModel = _baseViewModel.Area.XAxisType == ChartAxisType.Numeric ? new ChartBandElementVM<double>( this ) : ( DrawableChartComponentBaseViewModel ) new ChartBandElementVM<DateTime>( this );
        //return _baseViewModel;
    }

    bool IChartElementUiDomain.StartDrawing(
      IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        return _baseViewModel.Draw( _param1 );
    }

    void IChartElementUiDomain.StartDrawing()
    {
        _baseViewModel.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>(), 0 ) );
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        throw new NotImplementedException();

        //var drawValue = data.GetBandDrawValues( this );
        //if ( drawValue != null && !drawValue.IsEmpty() )
        //{
        //    return ( ( IChartElementUiDomain ) this ).StartDrawing( drawValue );
        //}
        //return false;
    }


    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        if ( ( ( SynchronizedDictionary<string, object> ) storage ).ContainsKey( "Line1" ) )
        {
            RemoveChildElement( ( IChartElement ) Line1 );
            PersistableHelper.Load( ( IPersistable ) Line1, storage, "Line1" );
            AddChildElement( ( IChartElement ) Line1, true );
        }
        if ( !( ( SynchronizedDictionary<string, object> ) storage ).ContainsKey( "Line2" ) )
            return;
        RemoveChildElement( ( IChartElement ) Line2 );
        PersistableHelper.Load( ( IPersistable ) Line2, storage, "Line2" );
        AddChildElement( ( IChartElement ) Line2, true );
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Line1", PersistableHelper.Save( ( IPersistable ) Line1 ) );
        storage.SetValue<SettingsStorage>( "Line2", PersistableHelper.Save( ( IPersistable ) Line2 ) );
    }

    internal override ChartBandElement Clone( ChartBandElement _param1 )
    {
        _param1 = base.Clone( _param1 );
        Line1.Clone( _param1.Line1 );
        Line2.Clone( _param1.Line2 );
        return _param1;
    }

    private void OnLineOnePropertyChanged(
#nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "AdditionalColor" ) )
            return;
        RaisePropertyChanged( "Color" );
    }
}
