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

/// <summary>The chart element representing the indicator.</summary>
[Display(ResourceType = typeof(LocalizedStrings), Name = "IndicatorSettings")]
public sealed class ChartIndicatorElement : ChartComponentViewModel<ChartIndicatorElement>, IChartElement, IChartPart<IChartElement>, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable, IChartIndicatorElement
{
    private DefaultPainter _defaultPainter;

    private IChartIndicatorPainter _chartIndicatorPainter;

    private bool _autoAssignYAxis;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartIndicatorElement" />.
    /// </summary>
    public ChartIndicatorElement()
    {
        _defaultPainter = new DefaultPainter();
        _defaultPainter.OnAttached((IChartIndicatorElement)this);
    }


    /// <summary>
    /// TonyAdded: 
    /// This constructor is used to setup the First in First Out capabilities for the Indicator.
    /// 
    /// I guess I meant to do a scrolling of the indicator values, so that the oldest values are removed when new values are added.
    /// </summary>
    /// <param name="fifoCapacity"></param>
    public ChartIndicatorElement(int fifoCapacity)
    {
        _defaultPainter = new DefaultPainter(fifoCapacity);
        FifoCapacity    = fifoCapacity;

        _defaultPainter.OnAttached(this);
    }

    public override string ToString() => FullTitle;

    public bool AutoAssignYAxis
    {
        get => _autoAssignYAxis;
        set => _autoAssignYAxis = value;
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
            ChartArea chartArea = (ChartArea)ChartArea;
            chartArea?.ViewModel.OnChartAreaElementsRemoving(IndicatorPainter.Element);
            IndicatorPainter.OnDetached();

            if ( value?.GetType() != typeof(DefaultPainter) )
            {
                _chartIndicatorPainter = value;
            }
            else
            {
                _chartIndicatorPainter = null;
                _defaultPainter = (DefaultPainter)value;
            }

            IndicatorPainter.OnAttached(this);
            chartArea?.ViewModel.OnChartAreaElementsAdded(this);
            RaisePropertyChanged(nameof(IndicatorPainter));
        }
    }

    /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.Color" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
    [Browsable(false)]
    public System.Windows.Media.Color Color
    {
        get => _defaultPainter.Line.Color.ToWpf();
        set => _defaultPainter.Line.Color = value.FromWpf();
    }

    /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.AdditionalColor" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
    [Browsable(false)]
    public System.Windows.Media.Color AdditionalColor
    {
        get => _defaultPainter.Line.AdditionalColor.ToWpf();
        set => _defaultPainter.Line.AdditionalColor = value.FromWpf();
    }

    [Browsable(false)]
    public int StrokeThickness
    {
        get => _defaultPainter.Line.StrokeThickness;
        set => _defaultPainter.Line.StrokeThickness = value;
    }

    [Browsable(false)]
    public bool AntiAliasing
    {
        get => _defaultPainter.Line.AntiAliasing;
        set => _defaultPainter.Line.AntiAliasing = value;
    }

    [Browsable(false)]
    public DrawStyles DrawStyle
    {
        get => _defaultPainter.Line.Style;
        set => _defaultPainter.Line.Style = value;
    }

    [Browsable(false)]
    public bool ShowAxisMarker
    {
        get => _defaultPainter.Line.ShowAxisMarker;
        set => _defaultPainter.Line.ShowAxisMarker = value;
    }

    /// <summary>Compatibility property for <see cref="P:StockSharp.Xaml.Charting.ChartLineElement.DrawTemplate" /> for <see cref="T:StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter" />.</summary>
    [Browsable(false)]
    public ControlTemplate DrawTemplate
    {
        get => ( (ChartLineElement)_defaultPainter.Line ).DrawTemplate;
        set
        {
            ( (ChartLineElement)_defaultPainter.Line ).DrawTemplate = value;
        }
    }

    System.Drawing.Color IChartIndicatorElement.Color
    {
        get => Color.FromWpf();
        set => Color = value.ToWpf();
    }

    System.Drawing.Color IChartIndicatorElement.AdditionalColor
    {
        get => AdditionalColor.FromWpf();
        set => AdditionalColor = value.ToWpf();
    }

    protected override bool OnDraw(ChartDrawData data)
    {
        return IndicatorPainter.Draw((IChartDrawData)data);
    }

    protected override void OnReset()
    {
        base.OnReset();
        IndicatorPainter?.Reset();
    }

    internal void CreateIndicatorPainter(IList<IndicatorType> indicatorTypeList, IIndicator indicator)
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
            myPainter = (IChartIndicatorPainter)indicatorType.CreatePainter();
        }

        if ( !( myPainter?.GetType() != typeof(DefaultPainter) ) )
        {
            return;
        }

        IndicatorPainter = myPainter;
    }


    protected override ChartIndicatorElement CreateClone()
    {
        ChartIndicatorElement clone = base.CreateClone();
        clone.IndicatorPainter = PersistableHelper.Clone<IChartIndicatorPainter>(IndicatorPainter);
        return clone;
    }

    protected override string GetGeneratedTitle()
    {
        return ChartHelper2025.TryGetIndicator( this )?.ToString();
    }
    

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>("IndicatorPainter", (SettingsStorage)null);
        if ( settingsStorage1 == null )
            return;
        Type type = Type.GetType(settingsStorage1.GetValue<string>("type", (string)null), false);
        if ( type == (Type)null )
            return;
        IndicatorPainter.OnDetached();
        IChartIndicatorPainter instance = TypeHelper.CreateInstance<IChartIndicatorPainter>(type, Array.Empty<object>());
        if ( instance.GetType() == typeof(DefaultPainter) )
        {
            _defaultPainter = (DefaultPainter)instance;
            _chartIndicatorPainter = (IChartIndicatorPainter)null;
        }
        else
            _chartIndicatorPainter = instance;
        SettingsStorage settingsStorage2 = settingsStorage1.GetValue<SettingsStorage>(
            "settings",
            (SettingsStorage)null);
        try
        {
            ( (IPersistable)instance ).Load(settingsStorage2);
        }
        catch
        {
        }
        IndicatorPainter.OnAttached((IChartIndicatorElement)this);
    }

    public override void Save(SettingsStorage storage)
    {
        storage.SetValue<SettingsStorage>(
            "IndicatorPainter",
            PersistableHelper.SaveEntire((IPersistable)IndicatorPainter, false));
        base.Save(storage);
    }

    protected override int Priority => 1;
}


//using Ecng.Common;
//using Ecng.Serialization;
//using StockSharp.Algo.Indicators;
//using Ecng.Drawing;
//using StockSharp.Xaml.Charting.IndicatorPainters;
//using System;
//using System.Collections.Generic; 
//using fx.Collections;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Controls;
//using System.Windows.Media;
//using StockSharp.Charting;

//namespace StockSharp.Xaml.Charting
//{

//    public sealed class ChartIndicatorElement : ChartComponentView< ChartIndicatorElement >, IChartIndicatorElement
//    {


//        private DefaultPainter _defaultPainter;
//        private IChartIndicatorPainter _chartIndicatorPainter;

//        public ChartIndicatorElement( )
//        {
//            _defaultPainter = new DefaultPainter( );

//            _defaultPainter.OnAttached( this );

//        }

//        public ChartIndicatorElement( int fifoCapacity )
//        {
//            _defaultPainter = new DefaultPainter( fifoCapacity );
//            FifoCapacity    = fifoCapacity;

//            _defaultPainter.OnAttached( this );
//        }

//        public override string ToString( )
//        {
//            return FullTitle;
//        }

//        [Browsable( false )]
//        [Obsolete( "Use FullTitle property instead." )]
//        public string Title
//        {
//            get
//            {
//                return FullTitle;
//            }
//            set
//            {
//                FullTitle = value;
//            }
//        }

//        public IChartIndicatorPainter IndicatorPainter
//        {
//            get
//            {
//                return _chartIndicatorPainter ?? _defaultPainter;
//            }
//            set
//            {
//                if( _chartIndicatorPainter == value )
//                {
//                    return;
//                }
//                ChartArea chartArea = (ChartArea) ChartArea;
//                chartArea?.ViewModel.OnChartAreaElementsRemoving( IndicatorPainter.Element );
//                IndicatorPainter.OnDetached( );

//                if( value?.GetType( ) != typeof( DefaultPainter ) )
//                {
//                    _chartIndicatorPainter = value;
//                }
//                else
//                {
//                    _chartIndicatorPainter = null;
//                    _defaultPainter = ( DefaultPainter )value;
//                }

//                IndicatorPainter.OnAttached( this );
//                chartArea?.ViewModel.OnChartAreaElementsAdded( this );
//                RaisePropertyChanged( nameof( IndicatorPainter ) );
//            }
//        }

//        [Browsable( false )]
//        public Color Color
//        {
//            get
//            {
//                return _defaultPainter.Line.Color;
//            }
//            set
//            {
//                _defaultPainter.Line.Color = value;
//            }
//        }

//        [Browsable( false )]
//        public Color AdditionalColor
//        {
//            get
//            {
//                return _defaultPainter.Line.AdditionalColor;
//            }
//            set
//            {
//                _defaultPainter.Line.AdditionalColor = value;
//            }
//        }

//        [Browsable( false )]
//        public int StrokeThickness
//        {
//            get
//            {
//                return _defaultPainter.Line.StrokeThickness;
//            }
//            set
//            {
//                _defaultPainter.Line.StrokeThickness = value;
//            }
//        }

//        [Browsable( false )]
//        public bool AntiAliasing
//        {
//            get
//            {
//                return _defaultPainter.Line.AntiAliasing;
//            }
//            set
//            {
//                _defaultPainter.Line.AntiAliasing = value;
//            }
//        }

//        [Browsable( false )]
//        public DrawStyles DrawStyle
//        {
//            get
//            {
//                return _defaultPainter.Line.Style;
//            }
//            set
//            {
//                _defaultPainter.Line.Style = value;
//            }
//        }

//        [Browsable( false )]
//        public bool ShowAxisMarker
//        {
//            get
//            {
//                return _defaultPainter.Line.ShowAxisMarker;
//            }
//            set
//            {
//                _defaultPainter.Line.ShowAxisMarker = value;
//            }
//        }

//        [Browsable( false )]
//        public ControlTemplate DrawTemplate
//        {
//            get
//            {
//                return _defaultPainter.Line.DrawTemplate;
//            }
//            set
//            {
//                _defaultPainter.Line.DrawTemplate = value;
//            }
//        }

//        protected override bool OnDraw( ChartDrawData data )
//        {
//            return IndicatorPainter.Draw( data );
//        }

//        protected override void OnReset( )
//        {
//            base.OnReset( );
//            IndicatorPainter?.Reset( );
//        }

//        internal void CreateIndicatorPainter( IList< IndicatorType > indicatorTypeList, IIndicator indicator )
//        {
//            if( _chartIndicatorPainter != null || indicatorTypeList == null || indicatorTypeList.Count <= 0 )
//            {
//                return;
//            }

//            IndicatorType indicatorType = indicatorTypeList.FirstOrDefault( t => t.Indicator == indicator.GetType( ) );

//            IChartIndicatorPainter myPainter;
//            if( indicatorType == null )
//            {
//                myPainter = null;
//            }
//            else
//            {
//                myPainter = (StockSharp.Xaml.Charting.IChartIndicatorPainter ) indicatorType.CreatePainter();

//            }

//            if( !( myPainter?.GetType( ) != typeof( DefaultPainter ) ) )
//            {
//                return;
//            }

//            IndicatorPainter = myPainter;
//        }

//        protected override ChartIndicatorElement CreateClone( )
//        {
//            ChartIndicatorElement clone = base.CreateClone( );
//            clone.IndicatorPainter = ( IChartIndicatorPainter )IndicatorPainter.Clone( );
//            return clone;
//        }

//        public override void Load( SettingsStorage storage )
//        {
//            base.Load( storage );
//            SettingsStorage settingsStorage = storage.GetValue( "IndicatorPainter", ( SettingsStorage )null );
//            if( settingsStorage == null )
//            {
//                return;
//            }
//            Type type = Type.GetType( settingsStorage.GetValue( "type", ( string )null ), false );
//            if( type == null )
//            {
//                return;
//            }
//            IndicatorPainter.OnDetached( );
//            IChartIndicatorPainter instance = type.CreateInstance< IChartIndicatorPainter >( );
//            if( instance.GetType( ) == typeof( DefaultPainter ) )
//            {
//                _defaultPainter = ( DefaultPainter )instance;
//                _chartIndicatorPainter = null;
//            }
//            else
//            {
//                _chartIndicatorPainter = instance;
//            }
//            IndicatorPainter.OnAttached( this );
//            SettingsStorage storage1 = settingsStorage.GetValue( "settings", ( SettingsStorage )null );
//            try
//            {
//                instance.Load( storage1 );
//            }
//            catch
//            {
//            }
//        }

//        public override void Save( SettingsStorage storage )
//        {
//            storage.SetValue( "IndicatorPainter", IndicatorPainter.SaveEntire( false ) );
//            base.Save( storage );
//        }
//    }
//}
