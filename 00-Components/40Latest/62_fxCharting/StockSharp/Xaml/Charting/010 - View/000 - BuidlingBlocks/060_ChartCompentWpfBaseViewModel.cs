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
public abstract class ChartCompentWpfBaseViewModel< T > : DrawableChartComponentBaseViewModel where T : ChartPart< T >, IDrawableChartElement
{
    private readonly PooledSet< IChartComponent > _componentUIMap = new PooledSet< IChartComponent >( );
    private readonly T _drawableChartElement;

    protected ChartCompentWpfBaseViewModel( T component )
    {
        _drawableChartElement = component ?? throw new ArgumentNullException( "elem" );
        AddPropertyEvents( ChartComponentView );
    }

    public override IDrawableChartElement Element
    {
        get
        {
            return ChartComponentView;
        }
    }

    protected T ChartComponentView
    {
        get
        {
            return _drawableChartElement;
        }
    }

    public sealed override void GuiUpdateAndClear( )
    {
        foreach( IChartComponent elementXY in _componentUIMap.ToArray( ) )
        {
            RemovePropertyEvents( elementXY );
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

    protected void AddStylePropertyChanging< U >( IChartComponent elementXY, string style, U[ ] drawStyles )
    {
        elementXY.PropertyValueChanging += ( x, s, y ) =>
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

    

    protected U CreateRenderableSeries< U >( ChartElementViewModel[ ] childviewmodel ) where U : BaseRenderableSeries, new()
    {        
        if ( childviewmodel != null && childviewmodel.Any( vm => vm == null ) )
        {
            throw new InvalidOperationException( "value is null during creation of " + typeof( U ).Name );
        }


        U instance = new U( );

        if ( ChartComponentView.RootElement == ChartComponentView )
        {
            instance.SetBindings( BaseRenderableSeries.IsVisibleProperty, ChartComponentView, "IsVisible" );
        }
        else
        {
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
        instance.SetBindings( BaseRenderableSeries.XAxisIdProperty, RootElem, "XAxisId" );
        instance.SetBindings( BaseRenderableSeries.YAxisIdProperty, RootElem, "YAxisId" );

        if ( !( ChartComponentView is ChartBandElement ) && !( ChartComponentView is IChartTransactionElement ) )
        {
            instance.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartComponentView, "StrokeThickness" );
            instance.SetBindings( BaseRenderableSeries.AntiAliasingProperty,    ChartComponentView, "AntiAliasing" );
        }

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
