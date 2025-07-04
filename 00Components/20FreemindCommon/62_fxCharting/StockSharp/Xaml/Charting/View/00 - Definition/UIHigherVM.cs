using Ecng.Common;
using Ecng.Xaml;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Localization;
using fx.Charting;
using System;
using System.Collections.Generic; using fx.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Ecng.Xaml.Converters;

#pragma warning disable CA1416

internal abstract class UIHigherVM< T > : UIBaseVM where T : ChartPart< T >, IDrawableChartElement
{
    private readonly PooledSet< IElementWithXYAxes > _rootElementSet = new PooledSet< IElementWithXYAxes >( );
    private readonly T _chartElement;

    protected UIHigherVM( T element )
    {
        if( element == null )
        {
            throw new ArgumentNullException( "elem" );
        }

        _chartElement = element;
        AddPropertyEvents( ChartElement );
    }

    public override IDrawableChartElement Element
    {
        get
        {
            return ChartElement;
        }
    }

    protected T ChartElement
    {
        get
        {
            return _chartElement;
        }
    }

    public override sealed void GuiUpdateAndClear( )
    {
        foreach( IElementWithXYAxes elementXY in _rootElementSet.ToArray( ) )
        {
            RemovePropertyEvents( elementXY );
        }
        base.GuiUpdateAndClear( );
    }

    protected void AddPropertyEvents( IElementWithXYAxes root )
    {
        if( !_rootElementSet.Add( root ) )
        {
            return;
        }

        root.PropertyValueChanging += OnPropertyChanging;
        root.PropertyChanged       += OnRootPropertyChanged;
    }

    private void RemovePropertyEvents( IElementWithXYAxes root )
    {
        if( !_rootElementSet.Remove( root ) )
        {
            return;
        }

        root.PropertyValueChanging -= OnPropertyChanging;
        root.PropertyChanged       -= OnRootPropertyChanged;
    }

    private void OnPropertyChanging( object elementXY, string string_0, object object_1 )
    {
        RootElementPropertyChanging( ( IElementWithXYAxes )elementXY, string_0, object_1 );
    }

    private void OnRootPropertyChanged( object sender, PropertyChangedEventArgs e )
    {
        RootElementPropertyChanged( ( IElementWithXYAxes )sender, e.PropertyName );
    }

    protected void AddStylePropertyChanging< U >( IElementWithXYAxes elementXY, string style, U[ ] drawStyles )
    {
        elementXY.PropertyValueChanging += ( x, s, y ) =>
        {
            if( s != style )
            {
                return;
            }
            if( !drawStyles.Contains( ( U )y ) )
            {
                //throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.Str2063Params, y ) );
            }
        };
    }

    

    protected U CreateRenderableSeries< U >( ChildVM[ ] childViewModel ) where U : BaseRenderableSeries, new()
    {
        if( childViewModel != null && childViewModel.Any( vm => vm == null ) )
        {
            throw new InvalidOperationException( "value is null during creation of " + typeof( U ).Name );
        }

        
        U instance = new U( );

        if( ChartElement.ElementWithXYAxes == ChartElement )
        {
            instance.SetBindings( BaseRenderableSeries.IsVisibleProperty, ChartElement, "IsVisible", BindingMode.TwoWay, null, null );
        }
        else
        {
            var isVisibleProperty = BaseRenderableSeries.IsVisibleProperty;
            var converter         = new BoolAllConverter( );
            converter.Value       = true;

            Binding[ ] bindingArray = new Binding[ 2 ]
            {
                new Binding( "IsVisible" )
                {
                    Source = ChartElement
                },
                new Binding( "IsVisible" )
                {
                    Source = ChartElement.ElementWithXYAxes
                }
            };

            instance.SetMultiBinding( isVisibleProperty, converter, bindingArray );
        }
        instance.SetBindings( BaseRenderableSeries.XAxisIdProperty, RootElem, "XAxisId", BindingMode.TwoWay, null, null );
        instance.SetBindings( BaseRenderableSeries.YAxisIdProperty, RootElem, "YAxisId", BindingMode.TwoWay, null, null );

        if( !( ChartElement is BandsUI ) )
        {
            instance.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartElement, "StrokeThickness", BindingMode.TwoWay, null, null );
            instance.SetBindings( BaseRenderableSeries.AntiAliasingProperty, ChartElement, "AntiAliasing", BindingMode.TwoWay, null, null );
        }

        instance.Tag = childViewModel == null || childViewModel.Length == 0 ? null : Tuple.Create< UIBaseVM, ChildVM[ ] >( this, childViewModel );

        ChartViewModel.ClearChildViewModels( );

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
