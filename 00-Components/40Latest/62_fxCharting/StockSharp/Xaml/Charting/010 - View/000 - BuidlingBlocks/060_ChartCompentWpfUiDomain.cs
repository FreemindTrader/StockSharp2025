using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Ecng.Xaml.Converters;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using StockSharp.Charting;

#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting;

/// <summary>
/// The following class will setup the WFP binding for the following properties
/// 
///     1) IsVisible
///     2) XAxisId
///     3) YAxisId
///     4) StrokeThickness (if it is not band and not IChartTransactionElement )
///     5) AntiAliasing    (if it is not band and not IChartTransactionElement )
///     
/// and event property handler when it is changing or has been changed.
/// 
/// All the ChartCandleElementViewModel, indicatorUI, quoteUI will inherit from this class
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ChartCompentWpfUiDomain< T > : ChartElementUiDomain where T : ChartPart< T >, IChartElementUiDomain
{
    private readonly PooledSet< IChartComponent > _componentUIMap = new PooledSet< IChartComponent >( );
    private readonly T _chartComponentUI;

    /// <summary>
    /// This constructor is called when we create the top most ChartComponent, for example, ChartCandleElementUiDomain will be called from here
    /// 
    /// public ChartElementUiDomain CreateViewModel( IDrawingSurfaceVM viewModel )
    /// {        
    ///     return _uiBusinessLogic = new ChartCandleElementUiDomain( this );
    /// }
    /// 
    /// And ChartCandleElementUIDomain is declared as follows:
    /// 
    /// public sealed class ChartCandleElementUiDomain( ChartCandleElement candle ) : ChartCompentWpfUiDomain<ChartCandleElement>( candle ), IPaletteProvider, IPaletteProviderSS
    /// 
    /// </summary>
    /// <param name="component"></param>
    /// <exception cref="ArgumentNullException"></exception>
    protected ChartCompentWpfUiDomain( T component )
    {
        _chartComponentUI = component ?? throw new ArgumentNullException( "elem" );
        AddPropertyEvents( ChartComponentView );
    }

    /// <summary>
    /// This will return the ChartComponentView, for example, ChartCandleElementUiDomain will return ChartCandleElement interface IChartElementUiDomain
    /// </summary>
    public override IChartElementUiDomain Element
    {
        get
        {
            return ChartComponentView;
        }
    }

    /// <summary>
    /// This will return the ChartComponentView, for example, ChartCandleElementUiDomain will return ChartCandleElement
    /// </summary>
    protected T ChartComponentView
    {
        get
        {
            return _chartComponentUI;
        }
    }

    public sealed override void GuiUpdateAndClear( )
    {
        foreach( IChartComponent component in _componentUIMap.ToArray( ) )
        {
            RemovePropertyEvents( component );
        }
        base.GuiUpdateAndClear( );
    }

    protected void AddPropertyEvents( IChartComponent component )
    {
        if( !_componentUIMap.Add( component ) )
        {
            return;
        }

        component.PropertyValueChanging += OnPropertyChanging;
        component.PropertyChanged       += OnRootPropertyChanged;
    }

    private void RemovePropertyEvents( IChartComponent root )
    {
        if( !_componentUIMap.Remove( root ) )
        {
            return;
        }

        root.PropertyValueChanging -= OnPropertyChanging;
        root.PropertyChanged       -= OnRootPropertyChanged;
    }

    private void OnPropertyChanging( object elementXY, string popstring, object object_1 )
    {
        RootElementPropertyChanging( ( IChartComponent )elementXY, popstring, object_1 );
    }

    private void OnRootPropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        RootElementPropertyChanged( ( IChartComponent )sender, e.PropertyName );
    }

    protected void AddDrawStylePropertyChanging< U >( IChartComponent com, string style, U[ ] drawStyles )
    {
        com.PropertyValueChanging += ( x, s, y ) =>
        {
            if( s != style )
            {
                return;
            }
            if( !drawStyles.Contains<U>( ( U )y ) )
            {
                throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.UnsupportedType, y));
            }
        };
    }


    /// <summary>
    /// Create the renderable series and setup the binding for the properties
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="childviewmodel"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected U CreateRenderableSeries< U >( ChartElementViewModel[ ] childviewmodel ) where U : BaseRenderableSeries, new()
    {        
        if ( childviewmodel != null && childviewmodel.Any( vm => vm == null ) )
        {
            throw new InvalidOperationException( "value is null during creation of " + typeof( U ).Name );
        }

        // Create an instance of the renderable series
        U instance = new U( );


        // If this is the toppest ChartComponent, then we just need to bind to its IsVisible property
        if ( ChartComponentView.RootElement == ChartComponentView )
        {
            instance.SetBindings( BaseRenderableSeries.IsVisibleProperty, ChartComponentView, "IsVisible" );
        }
        else
        {
            // if this is not the toppest ChartComponent, then we need to bind to both its IsVisible property and its RootElement's IsVisible property
            var isVisibleProperty = BaseRenderableSeries.IsVisibleProperty;
            var converter         = new BoolAllConverter( );
            converter.Value = true;

            Binding[ ] bindingArray = new Binding[ 2 ]
            {
                new Binding( "IsVisible" )
                {
                    Source = ChartComponentView
                },
                new Binding( "IsVisible" )
                {
                    Source = ChartComponentView.RootElement
                }
            };

            instance.SetMultiBinding( isVisibleProperty, converter, bindingArray );
        }

        // Set the binding for XAxisId and YAxisId
        instance.SetBindings( BaseRenderableSeries.XAxisIdProperty, RootElem, "XAxisId" );
        instance.SetBindings( BaseRenderableSeries.YAxisIdProperty, RootElem, "YAxisId" );

        if ( !( ChartComponentView is ChartBandElement ) && !( ChartComponentView is IChartTransactionElement ) )
        {
            // If this is not a band or not TransactionElement, then we need to bind to its StrokeThickness and AntiAliasing property
            instance.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartComponentView, "StrokeThickness" );
            instance.SetBindings( BaseRenderableSeries.AntiAliasingProperty,    ChartComponentView, "AntiAliasing" );
        }

        // Set the Tag property, so that we can get to its children's ViewModel from the renderable series
        // For example, CandleStick will have OHLC children view model.
        instance.Tag = ( childviewmodel == null || childviewmodel.Length == 0 ) ? null : Tuple.Create( this, childviewmodel );
        ChartViewModel.ClearChildViewModels();
        
        return instance;
    }

    protected void SetIncludeSeries( IRenderableSeries series, bool shouldIncludeSeries )
    {
        BaseRenderableSeries quoteRSeries = ( BaseRenderableSeries )series;


        RolloverModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        CursorModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        TooltipModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        VerticalSliceModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );        
    }
}
