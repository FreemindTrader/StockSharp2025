// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartBandElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using DevExpress.Dialogs.Core.View;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Drawing;
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
public sealed class ChartBandElement : ChartComponent<ChartBandElement>,
                                          IChartElement,
                                          IChartPart<IChartElement>,
                                          INotifyPropertyChanged,
                                          INotifyPropertyChanging,
                                          IPersistable,
                                          IChartBandElement,
                                          IChartComponent,
                                          IDrawableChartElement
{

    private DrawStyles _drawStyle = DrawStyles.Band;

    private readonly ChartLineElement _lineOne;

    private readonly ChartLineElement _lineTwo;

    private DrawableChartElementBaseViewModel _baseViewModel;

    /// <summary>Create instance.</summary>
    public ChartBandElement()
    {
        this._lineOne = new ChartLineElement()
        {
            Color = Colors.DarkGreen,
            AdditionalColor = Colors.DarkGreen.ToTransparent( ( byte ) 50 )
        };
        this._lineTwo = new ChartLineElement()
        {
            Color = Colors.DarkGreen,
            AdditionalColor = Colors.DarkGreen.ToTransparent( ( byte ) 50 )
        };
        this.Line1.PropertyChanged += new PropertyChangedEventHandler( this.OnLineOnePropertyChanged );
        this.AddChildElement( ( IChartElement ) this.Line1, true );
        this.AddChildElement( ( IChartElement ) this.Line2, true );
    }

    Color IDrawableChartElement.Color
    {
        get
        {
            return this.Line1.AdditionalColor;
        }    
    }

    /// <summary>
    /// The band drawing style. The default is <see cref="F:Ecng.Drawing.DrawStyles.Band" />. Can also be <see cref="F:Ecng.Drawing.DrawStyles.BandOneValue" />.
    /// </summary>
    [Browsable( false )]
    public DrawStyles Style
    {
        get => this._drawStyle;
        set
        {
            if ( this._drawStyle == value )
                return;
            if ( value != DrawStyles.Band && value != DrawStyles.BandOneValue )
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.UnsupportedType, new object[ 1 ]
                {
          (object) value
                } ) );
            this.RaisePropertyValueChanging( nameof( Style ), ( object ) value );
            this._drawStyle = value;
            this.RaisePropertyChanged( nameof( Style ) );
        }
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line1" />.
    /// </summary>
    public ChartLineElement Line1 => this._lineOne;

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.ChartBandElement.Line2" />.
    /// </summary>
    public ChartLineElement Line2 => this._lineTwo;

    IChartLineElement IChartBandElement.Line1 => ( IChartLineElement ) this.Line1;

    IChartLineElement IChartBandElement.Line2 => ( IChartLineElement ) this.Line2;

    public override bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType )
    {
        return !yType.HasValue || yType.GetValueOrDefault() == ChartAxisType.Numeric;
    }

    DrawableChartElementBaseViewModel IDrawableChartElement.CreateViewModel(
      ScichartSurfaceMVVM _param1 )
    {
        this._baseViewModel = _baseViewModel.Area.XAxisType == ChartAxisType.Numeric ? new ChartBandElementVM<double>( this ) : ( DrawableChartElementBaseViewModel ) new ChartBandElementVM<DateTime>( this );
        return _baseViewModel;
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

    protected override bool OnDraw( ChartDrawData data )
    {
        var drawValue = data.GetBandDrawValues( this );
        if ( drawValue != null && !drawValue.IsEmpty() )
        {
            return ( ( IDrawableChartElement ) this ).StartDrawing( drawValue );
        }
        return false;
    }


    /// <summary>Load settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        if ( ( ( SynchronizedDictionary<string, object> ) storage ).ContainsKey( "Line1" ) )
        {
            this.RemoveChildElement( ( IChartElement ) this.Line1 );
            PersistableHelper.Load( ( IPersistable ) this.Line1, storage, "Line1" );
            this.AddChildElement( ( IChartElement ) this.Line1, true );
        }
        if ( !( ( SynchronizedDictionary<string, object> ) storage ).ContainsKey( "Line2" ) )
            return;
        this.RemoveChildElement( ( IChartElement ) this.Line2 );
        PersistableHelper.Load( ( IPersistable ) this.Line2, storage, "Line2" );
        this.AddChildElement( ( IChartElement ) this.Line2, true );
    }

    /// <summary>Save settings.</summary>
    /// <param name="storage">Settings storage.</param>
    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Line1", PersistableHelper.Save( ( IPersistable ) this.Line1 ) );
        storage.SetValue<SettingsStorage>( "Line2", PersistableHelper.Save( ( IPersistable ) this.Line2 ) );
    }

    internal override ChartBandElement Clone( ChartBandElement _param1 )
    {
        _param1 = base.Clone( _param1 );
        this.Line1.Clone( _param1.Line1 );
        this.Line2.Clone( _param1.Line2 );
        return _param1;
    }

    private void OnLineOnePropertyChanged(
#nullable enable
      object? _param1,
      PropertyChangedEventArgs _param2 )
    {
        if ( !( _param2.PropertyName == "AdditionalColor" ) )
            return;
        this.RaisePropertyChanged( "Color" );
    }
}
