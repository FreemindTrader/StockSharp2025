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

    private ChartElementUiDomain? _uiBusinessLogic = null;

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

        Line1.PropertyChanged += new PropertyChangedEventHandler( OneLineOneAdditionalColorChanged );
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
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnsupportedType, value ) );

            RaisePropertyValueChanging( nameof( Style ), value );
            _drawStyle = value;
            RaisePropertyChanged( nameof( Style ) );
        }
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

    ChartElementUiDomain IChartElementUiDomain.CreateViewModel( IDrawingSurfaceVM iSurface )
    {
        var drawingSurface = (ScichartSurfaceMVVM) iSurface;
        _uiBusinessLogic = drawingSurface.Area.XAxisType == ChartAxisType.Numeric ? new ChartBandElementUiDomain<double>( this ) : ( ChartElementUiDomain ) new ChartBandElementUiDomain<DateTime>( this );
        return _uiBusinessLogic;
    }

    bool IChartElementUiDomain.StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> drawData )
    {        
        return _uiBusinessLogic.Draw( drawData );
    }

    void IChartElementUiDomain.StartDrawing()
    {        
        _uiBusinessLogic.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>(), 0 ) );
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        IEnumerableEx<ChartDrawData.IDrawValue> drawValue = data.GetCandleRelatedData( this );
        return drawValue != null && !CollectionHelper.IsEmpty( drawValue ) && ( ( IChartElementUiDomain ) this ).StartDrawing( drawValue );        
    }


    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );

        if (  storage.ContainsKey( "Line1" ) )
        {
            RemoveChildElement( Line1 );
            PersistableHelper.Load( Line1, storage, "Line1" );
            AddChildElement( Line1, true );
        }
        
        if (  storage.ContainsKey( "Line2" ) )
        {
            RemoveChildElement( Line2 );
            PersistableHelper.Load( Line2, storage, "Line2" );
            AddChildElement( Line2, true );
        }                           
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue( "Line1", PersistableHelper.Save( Line1 ) );
        storage.SetValue( "Line2", PersistableHelper.Save( Line2 ) );
    }

    internal override ChartBandElement Clone( ChartBandElement other )
    {
        other = base.Clone( other );
        Line1.Clone( other.Line1 );
        Line2.Clone( other.Line2 );
        
        return other;
    }

    private void OneLineOneAdditionalColorChanged( object? _param1, PropertyChangedEventArgs e )
    {
        if ( ( e.PropertyName != "AdditionalColor" ) )
            return;

        RaisePropertyChanged( "Color" );
    }
}
