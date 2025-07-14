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

/// <summary>Annotation.</summary>
public class ChartAnnotation : ChartComponent<ChartAnnotation>,
                                  IChartElement,
                                  IChartPart<IChartElement>,
                                  INotifyPropertyChanged,
                                  INotifyPropertyChanging,
                                  IPersistable,
                                  IChartAnnotationElement,
                                  IChartComponent,
                                  IDrawableChartElement
{
    
    private ChartAnnotationTypes _chartAnnotationTypes;
    
    private ChartAnnotationVM _baseViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartAnnotation" />.
    /// </summary>
    public ChartAnnotation() => this.IsLegend = false;

    

    public Color Color
    {
        get
        {
            return Colors.Transparent;
        }    
    }

    public ChartAnnotationTypes Type
    {
        get => this._chartAnnotationTypes;
        set
        {
            if ( this._chartAnnotationTypes == value )
                return;
            this._chartAnnotationTypes = this._chartAnnotationTypes == ChartAnnotationTypes.None ? value : throw new InvalidOperationException( LocalizedStrings.AnnotationTypeCantBeChanged );
        }
    }

    //Func<IComparable, System.Drawing.Color?> StockSharp.Charting.IChartElement.Colorer
    //{
    //    get => throw new NotImplementedException();
    //    set => throw new NotImplementedException();
    //}
        

    public DrawableChartComponentBaseViewModel CreateViewModel( IScichartSurfaceVM viewModel )
    {
        if ( this.Type == ChartAnnotationTypes.None )
            throw new InvalidOperationException( "annotation type is not set" );
        return ( DrawableChartComponentBaseViewModel ) ( this._baseViewModel = new ChartAnnotationVM( this ) );
    }

    bool IDrawableChartElement.StartDrawing(
      IEnumerableEx<ChartDrawData.IDrawValue> _param1 )
    {
        return this._baseViewModel.Draw( _param1 );
    }

    void IDrawableChartElement.StartDrawing()
    {
        this._baseViewModel.Draw( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( Enumerable.Empty<ChartDrawData.IDrawValue>(), 0 ) );
    }

    bool IChartComponent.CheckAxesCompatible(
      ChartAxisType? _param1,
      ChartAxisType? _param2 )
    {
        return !_param2.HasValue || _param2.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        var annotationData = data.GetAnnotation( this );
        if ( annotationData == null )
            return false;
        return ( ( IDrawableChartElement ) this ).StartDrawing( CollectionHelper.ToEx<ChartDrawData.IDrawValue>( ( IEnumerable<ChartDrawData.IDrawValue> ) new ChartDrawData.IDrawValue[ 1 ]
        {
      (ChartDrawData.IDrawValue) annotationData
        }, 1 ) );
    }

    internal override ChartAnnotation Clone( ChartAnnotation _param1 )
    {
        base.Clone( _param1 );
        _param1.Type = _param1.Type;
        return _param1;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        this.Type = storage.GetValue<ChartAnnotationTypes>( "Type", this.Type );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<ChartAnnotationTypes>( "Type", this.Type );
    }

    
}
