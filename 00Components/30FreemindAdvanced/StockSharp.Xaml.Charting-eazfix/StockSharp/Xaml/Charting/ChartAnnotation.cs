// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

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

// ChartElement<ChartAnnotation>,
//
// ICloneable<ChartAnnotation>, ICloneable, IEquatable<ChartAnnotation>, IComparable<ChartAnnotation>, IComparable, IChartPart<ChartAnnotation>, , , , , 

public class ChartAnnotation :
  ChartElement<ChartAnnotation>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartAnnotationElement,
  IChartComponent,
  IDrawableChartElement
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private ChartAnnotationTypes _chartAnnotationTypes;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private ChartAnnotationVM _baseViewModel;

    public ChartAnnotation() => this.IsLegend = false;

    [Browsable( false )]
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

    Color IDrawableChartElement.Color
    {
    return Colors.Transparent;
    }

    UIChartBaseViewModel IDrawableChartElement.CreateViewModel(
      ScichartSurfaceMVVM _param1 )
    {
        if ( this.Type == ChartAnnotationTypes.None )
            throw new InvalidOperationException( "annotation type is not set" );
        return ( UIChartBaseViewModel ) ( this._baseViewModel = new ChartAnnotationVM( this ) );
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
      ChartAxisType? _param2)
    {
        return !_param2.HasValue || _param2.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        ChartDrawData.AnnotationData annotationData = data.GetAnnotation( ( IChartAnnotationElement ) this );
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
