// Decompiled with JetBrains decompiler
// Type: #=zBQI7r_wiRKcCdd7zOgb7xAm_Ca5jQWV7qTLlcbbCsg9V98qcX3BV8d1LwxB$
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using Ecng.Common;
using Ecng.Xaml;
using Ecng.Xaml.Converters;
using SciChart.Charting.ChartModifiers;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting;

public abstract class ChartCompentWpfBaseViewModel<T> : DrawableChartComponentBaseViewModel where T : ChartPart<T>, IDrawableChartElement
{

    private readonly HashSet<IChartComponent> _componentUIMap = new HashSet<IChartComponent>();

    private readonly T _drawableChartElement;

    protected ChartCompentWpfBaseViewModel( T _param1 )
    {
        _drawableChartElement = _param1 ?? throw new ArgumentNullException( "elem" );
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

    public sealed override void GuiUpdateAndClear()
    {
        foreach ( IChartComponent ui in _componentUIMap.ToArray() )
            RemovePropertyEvents( ui );
        base.GuiUpdateAndClear();
    }

    protected void AddPropertyEvents( IChartComponent component )
    {
        if ( !_componentUIMap.Add( component ) )
            return;

        component.PropertyValueChanging += new Action<object, string, object>( OnPropertyChanging );
        component.PropertyChanged += new PropertyChangedEventHandler( OnRootPropertyChanged );
    }

    private void RemovePropertyEvents( IChartComponent component )
    {
        if ( !_componentUIMap.Remove( component ) )
            return;
        component.PropertyValueChanging -= new Action<object, string, object>( OnPropertyChanging );
        component.PropertyChanged -= new PropertyChangedEventHandler( OnPropertyChanged );
    }

    private void OnPropertyChanging( object _param1, string _param2, object _param3 )
    {
        RootElementPropertyChanging( ( IChartComponent ) _param1, _param2, _param3 );
    }

    private void OnRootPropertyChanged( object _param1, PropertyChangedEventArgs _param2 )
    {
        RootElementPropertyChanged( ( IChartComponent ) _param1, _param2.PropertyName );
    }

    protected static void AddStylePropertyChanging<T>( IChartComponent _param0, string _param1, T[ ] _param2 )
    {
        _param0.PropertyValueChanging += new Action<object, string, object>( new ChartCompentWpfBaseViewModel<T>.SomeInternalSealClass0836<T>()
        {
            _someString08353 = _param1,
            _someTArray = _param2
        }.SomeInternalMethod08342 );
    }

    protected T CreateRenderableSeries<T>( ChartElementViewModel[ ] childViewModel ) where T : BaseRenderableSeries, new()
    {
        if ( childviewmodel != null && childviewmodel.any( vm => vm == null ) )
        {
            throw new invalidoperationexception( "value is null during creation of " + typeof( u ).name );
        }

        T instance = new T();
        if ( ChartComponentView.RootElement == ChartComponentView )
        {
            instance.SetBindings( BaseRenderableSeries.IsVisibleProperty, ChartComponentView, "IsVisible" );
        }
        else
        {
            // ISSUE: variable of a boxed type
            var local = instance;
            DependencyProperty z8b6MqaiE8Uzn = BaseRenderableSeries.IsVisibleProperty;
            BoolAllConverter conv = new BoolAllConverter();
            conv.Value = true;
            Binding[] bindingArray = new Binding[2]
      {
        new Binding("IsVisible")
        {
          Source = (object) ChartComponentView
        },
        new Binding("IsVisible")
        {
          Source = (object) ChartComponentView.RootElement
        }
      };
            local.SetMultiBinding( z8b6MqaiE8Uzn, ( IMultiValueConverter ) conv, bindingArray );
        }
        instance.SetBindings( BaseRenderableSeries.XAxisIdProperty, RootElem, "XAxisId" );
        instance.SetBindings( BaseRenderableSeries.YAxisIdProperty, RootElem, "YAxisId" );
        T drawableElem = ChartComponentView;
        if ( !( drawableElem is ChartBandElement ) && !( drawableElem is IChartTransactionElement ) )
        {
            instance.SetBindings( BaseRenderableSeries.StrokeThicknessProperty, ChartComponentView, "StrokeThickness" );
            instance.SetBindings( BaseRenderableSeries.AntiAliasingProperty, ChartComponentView, "AntiAliasing" );
        }
        instance.Tag = childViewModel == null || childViewModel.Length == 0 ? ( Tuple<DrawableChartComponentBaseViewModel, ChartElementViewModel[ ]> ) null : Tuple.Create<DrawableChartComponentBaseViewModel, ChartElementViewModel[ ]>( ( DrawableChartComponentBaseViewModel ) this, childViewModel );
        ChartViewModel.ClearChildViewModels();
        return instance;
    }

    protected static void SetIncludeSeries( IRenderableSeries series, bool shouldIncludeSeries )
    {
        BaseRenderableSeries quoteRSeries = ( BaseRenderableSeries )series;


        RolloverModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        CursorModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        TooltipModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
        VerticalSliceModifier.SetIncludeSeries( quoteRSeries, shouldIncludeSeries );
    }

    [Serializable]
    private sealed class SomeInternalSealClass0835<T> where T : BaseRenderableSeries, new()
    {
        public static readonly ChartCompentWpfBaseViewModel<T>.SomeInternalSealClass0835<T> SomeMethond0343 = new ChartCompentWpfBaseViewModel<T>.SomeInternalSealClass0835<T>();
        public static Func<ChartElementViewModel, bool> SomeIntenalMethod003D;

        public bool SomeStuff034803(
              ChartElementViewModel _param1 )
        {
            return _param1 == null;
        }
    }

    
}
