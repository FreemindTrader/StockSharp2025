using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

/// <summary>
/// Annotation View Model. It defines
///     1) Color
///     2) ChartAnnotationTypes
///     3) Pass all the drawing activity to the ChartAnnotationUiDomain - The business logic code
/// </summary>
public class ChartAnnotation : ChartComponentViewModel<ChartAnnotation>,
                                  IChartElement,
                                  IChartPart<IChartElement>,
                                  INotifyPropertyChanged,
                                  INotifyPropertyChanging,
                                  IPersistable,
                                  IChartAnnotationElement,
                                  IChartComponent,
                                  IChartElementUiDomain
{
    
    private ChartAnnotationTypes _chartAnnotationTypes;
    
    private ChartAnnotationUiDomain _uiBusinessLogic;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartAnnotation" />.
    /// </summary>
    public ChartAnnotation() => IsLegend = false;

    

    public Color Color
    {
        get
        {
            return Colors.Transparent;
        }    
    }

    public ChartAnnotationTypes Type
    {
        get => _chartAnnotationTypes;
        set
        {
            if ( _chartAnnotationTypes == value )
                return;
            _chartAnnotationTypes = _chartAnnotationTypes == ChartAnnotationTypes.None ? value : throw new InvalidOperationException( LocalizedStrings.AnnotationTypeCantBeChanged );
        }
    }  

    public ChartElementUiDomain CreateViewModel( IDrawingSurfaceVM viewModel )
    {
        if ( Type == ChartAnnotationTypes.None )
        {
            throw new InvalidOperationException( "annotation type is not set" );
        }
            
        return ( ChartElementUiDomain ) ( _uiBusinessLogic = new ChartAnnotationUiDomain( this ) );
    }

    //public DrawableChartComponentBaseViewModel CreateViewModel( ScichartSurfaceMVVM _param1 );

    bool IChartElementUiDomain.StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> drawData )
    {
        return _uiBusinessLogic.Draw( drawData );
    }

    void IChartElementUiDomain.StartDrawing()
    {
        _uiBusinessLogic.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>(), 0 ) );
    }

    bool IChartComponent.CheckAxesCompatible( ChartAxisType? axisTypeOne, ChartAxisType? axisTypeTwo )
    {
        return !axisTypeTwo.HasValue || axisTypeTwo.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        var annData = data.GetAnnotation( this );

        if ( annData == null )
            return false;

        return ( ( IChartElementUiDomain ) this ).StartDrawing( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( new ChartDrawData.IDrawValue[ 1 ]{ annData }, 1 ) );
    }

    internal override ChartAnnotation Clone( ChartAnnotation to )
    {
        base.Clone( to );
        to.Type = this.Type;
        return to;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        Type = storage.GetValue<ChartAnnotationTypes>( "Type", Type );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<ChartAnnotationTypes>( "Type", Type );
    }    
}
