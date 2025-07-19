using Ecng.Common;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

#nullable disable
namespace StockSharp.Xaml.Charting;

[Display( ResourceType = typeof( LocalizedStrings ), Name = "IndicatorSettings" )]
public sealed class ChartIndicatorElement : ChartComponent<ChartIndicatorElement>, IChartElement, IChartPart<IChartElement>, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable, IChartIndicatorElement
{
    private DefaultPainter _defaultPainter;

    private IChartIndicatorPainter _chartIndicatorPainter;

    private bool _autoAssignYAxis;

    public ChartIndicatorElement()
    {
        this._defaultPainter = new DefaultPainter();
        this._defaultPainter.OnAttached( ( IChartIndicatorElement ) this );
    }

    public override string ToString() => this.FullTitle;

    public bool AutoAssignYAxis
    {
        get => this._autoAssignYAxis;
        set => this._autoAssignYAxis = value;
    }

    public IChartIndicatorPainter IndicatorPainter
    {
        get
        {
            return _chartIndicatorPainter ?? _defaultPainter;
        }
        set
        {
            if ( _chartIndicatorPainter == value )
            {
                return;
            }
            ChartArea chartArea = (ChartArea) ChartArea;
            chartArea?.ViewModel.OnChartAreaElementsRemoving( IndicatorPainter.Element );
            IndicatorPainter.OnDetached();

            if ( value?.GetType() != typeof( DefaultPainter ) )
            {
                _chartIndicatorPainter = value;
            }
            else
            {
                _chartIndicatorPainter = null;
                _defaultPainter = ( DefaultPainter ) value;
            }

            IndicatorPainter.OnAttached( this );
            chartArea?.ViewModel.OnChartAreaElementsAdded( this );
            RaisePropertyChanged( nameof( IndicatorPainter ) );
        }
    }

    [Browsable( false )]
    public System.Windows.Media.Color Color
    {
        get => this._defaultPainter.Line.Color.ToWpf();
        set => this._defaultPainter.Line.Color = value.FromWpf();
    }

    [Browsable( false )]
    public System.Windows.Media.Color AdditionalColor
    {
        get => this._defaultPainter.Line.AdditionalColor.ToWpf();
        set => this._defaultPainter.Line.AdditionalColor = value.FromWpf();
    }

    [Browsable( false )]
    public int StrokeThickness
    {
        get => this._defaultPainter.Line.StrokeThickness;
        set => this._defaultPainter.Line.StrokeThickness = value;
    }

    [Browsable( false )]
    public bool AntiAliasing
    {
        get => this._defaultPainter.Line.AntiAliasing;
        set => this._defaultPainter.Line.AntiAliasing = value;
    }

    [Browsable( false )]
    public DrawStyles DrawStyle
    {
        get => this._defaultPainter.Line.Style;
        set => this._defaultPainter.Line.Style = value;
    }

    [Browsable( false )]
    public bool ShowAxisMarker
    {
        get => this._defaultPainter.Line.ShowAxisMarker;
        set => this._defaultPainter.Line.ShowAxisMarker = value;
    }

    [Browsable( false )]
    public ControlTemplate DrawTemplate
    {
        get => ( ( ChartLineElement ) this._defaultPainter.Line ).DrawTemplate;
        set
        {
            ( ( ChartLineElement ) this._defaultPainter.Line ).DrawTemplate = value;
        }
    }

    System.Drawing.Color IChartIndicatorElement.Color
    {
        get => this.Color.FromWpf();
        set => this.Color = value.ToWpf();
    }

    System.Drawing.Color IChartIndicatorElement.AdditionalColor
    {
        get => this.AdditionalColor.FromWpf();
        set => this.AdditionalColor = value.ToWpf();
    }

    protected override bool OnDraw( ChartDrawData data )
    {
        return this.IndicatorPainter.Draw( ( IChartDrawData ) data );
    }

    protected override void OnReset()
    {
        base.OnReset();
        this.IndicatorPainter?.Reset();
    }

    internal void CreateIndicatorPainter( IList<IndicatorType> indicatorTypeList, IIndicator indicator )
    {
        if ( _chartIndicatorPainter != null || indicatorTypeList == null || indicatorTypeList.Count <= 0 )
        {
            return;
        }

        IndicatorType indicatorType = indicatorTypeList.FirstOrDefault(t => t.Indicator == indicator.GetType());

        IChartIndicatorPainter myPainter;
        if ( indicatorType == null )
        {
            myPainter = null;
        }
        else
        {
            myPainter = ( IChartIndicatorPainter ) indicatorType.CreatePainter();
        }

        if ( !( myPainter?.GetType() != typeof( DefaultPainter ) ) )
        {
            return;
        }

        IndicatorPainter = myPainter;
    }


    protected override ChartIndicatorElement CreateClone()
    {
        ChartIndicatorElement clone = base.CreateClone();
        clone.IndicatorPainter = PersistableHelper.Clone<IChartIndicatorPainter>( this.IndicatorPainter );
        return clone;
    }

    protected override string GetGeneratedTitle() => this.TryGetIndicator()?.ToString();

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>("IndicatorPainter", (SettingsStorage) null);
        if ( settingsStorage1 == null )
            return;
        Type type = Type.GetType(settingsStorage1.GetValue<string>("type", (string) null), false);
        if ( type == ( Type ) null )
            return;
        this.IndicatorPainter.OnDetached();
        IChartIndicatorPainter instance = TypeHelper.CreateInstance<IChartIndicatorPainter>(type, Array.Empty<object>());
        if ( instance.GetType() == typeof( DefaultPainter ) )
        {
            this._defaultPainter = ( DefaultPainter ) instance;
            this._chartIndicatorPainter = ( IChartIndicatorPainter ) null;
        }
        else
            this._chartIndicatorPainter = instance;
        SettingsStorage settingsStorage2 = settingsStorage1.GetValue<SettingsStorage>(
            "settings",
            (SettingsStorage) null);
        try
        {
            ( ( IPersistable ) instance ).Load( settingsStorage2 );
        }
        catch
        {
        }
        this.IndicatorPainter.OnAttached( ( IChartIndicatorElement ) this );
    }

    public override void Save( SettingsStorage storage )
    {
        storage.SetValue<SettingsStorage>(
            "IndicatorPainter",
            PersistableHelper.SaveEntire( ( IPersistable ) this.IndicatorPainter, false ) );
        base.Save( storage );
    }

    protected override int Priority => 1;
}
