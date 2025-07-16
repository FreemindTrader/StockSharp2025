using Ecng.Collections;
using Ecng.Xaml;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.RenderableSeries;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using Ecng.Xaml.Converters;

namespace StockSharp.Xaml.Charting;
#nullable disable
public abstract class DrawableChartElementBaseViewModel : ChartBaseViewModel
{
	
	private readonly Dictionary<IRenderableSeries, AxisMarkerAnnotation> _renderseries2AxisMarker = new Dictionary<IRenderableSeries, AxisMarkerAnnotation>();
	
	private ChartCompentViewModel _parentChartViewModel;

	protected ChartCompentViewModel ChartViewModel
	{
		get
		{
			return _parentChartViewModel;
		}

		set
		{
			_parentChartViewModel = value;
		}
	}

	public abstract IDrawableChartElement Element
	{
		get;
	}

	public IChartComponent RootElem
	{
		get => this.ChartViewModel.ChartComponent;
	}

	public IScichartSurfaceVM ScichartSurfaceMVVM
	{
		get
		{
			return this.ChartViewModel.Pane;
		}
	}



	protected bool IsDisposed() => this.ChartViewModel.IsDisposed;

	private static Dispatcher GetDispatcher()
	{
		return GuiDispatcher.GlobalDispatcher.Dispatcher;
	}

	protected static bool IsUiThread()
	{
		return GetDispatcher().CheckAccess();
	}

	protected abstract void UpdateUi();

	protected virtual void Init()
	{
	}

	protected abstract void Clear();

	public abstract bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> _param1 );

	public void Reset()
	{
		this.UpdateUi();
		this.PerformUiAction( new Action( this.ResetY1Annotation ), true );
	}

	public virtual void GuiUpdateAndClear()
	{
		this.PerformUiAction( new Action( this.UpdateAndClear ), true );
	}

	protected virtual void RootElementPropertyChanging(
		IChartComponent _param1,
		string _param2,
		object _param3 )
	{
	}

	protected virtual void RootElementPropertyChanged(
	  IChartComponent _param1,
	  string _param2 )
	{
	}

	public void Init(
		ChartCompentViewModel _param1 )
	{
		if ( this.ChartViewModel != null )
			throw new InvalidOperationException( "parent was already added" );
		this.ChartViewModel = _param1;
		this.Init();
		this.Reset();
	}

	public virtual void UpdateYAxisMarker()
	{
		foreach ( KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> p in _renderseries2AxisMarker )
		{
			p.Value.Y1 = p.Key.DataSeries?.LatestYValue;
		}
	}

	public virtual void PerformPeriodicalAction()
	{
	}

	protected void ClearAll()
	{
		foreach ( KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> keyValuePair in this._renderseries2AxisMarker )
		{
			keyValuePair.Value.IsHidden = true;
			BindingOperations.ClearAllBindings( ( DependencyObject ) keyValuePair.Value );
		}
		this.ScichartSurfaceMVVM.RemoveAnnotation(this.RootElem, ( object ) null);
		this._renderseries2AxisMarker.Clear();
	}

	protected void SetupAxisMarkerAndBinding(
	  IRenderableSeries _param1,
	  IChartComponent _param2,
	  string _param3,
	  string _param4 )
	{
		AxisMarkerAnnotation markerAnnotation1 = new AxisMarkerAnnotation();
		markerAnnotation1.FontSize = 11.0;
		markerAnnotation1.Foreground = ( Brush ) Brushes.White;
		AxisMarkerAnnotation markerAnnotation2 = markerAnnotation1;
		this._renderseries2AxisMarker[ _param1 ] = markerAnnotation2;
		this.ScichartSurfaceMVVM.AddAxisMakerAnnotation(this.RootElem, (IAnnotation) markerAnnotation2, ( object ) markerAnnotation2);
		markerAnnotation2.SetBindings( AnnotationBase.XAxisIdProperty, ( object ) _param2, "XAxisId" );
		markerAnnotation2.SetBindings( AnnotationBase.YAxisIdProperty, ( object ) _param2, "YAxisId" );
		AxisMarkerAnnotation markerAnnotation3 = markerAnnotation2;
		DependencyProperty isHiddenProperty = AnnotationBase.IsHiddenProperty;
		BoolAnyConverter conv = new BoolAnyConverter();
		conv.Value = false;
		Binding[] bindingArray = new Binding[2]
	{
	  new Binding(_param3) { Source = (object) _param2 },
	  new Binding()
	  {
		Source = (object) _param1,
		Path = new PropertyPath((object) BaseRenderableSeries.IsVisibleProperty)
	  }
	};
		markerAnnotation3.SetMultiBinding( isHiddenProperty, ( IMultiValueConverter ) conv, bindingArray );
		if ( _param4 != null )
		{
			markerAnnotation2.SetBindings( Control.BackgroundProperty, ( object ) _param2, _param4, converter: ( IValueConverter ) new ColorToBrushConverter() );
			markerAnnotation2.SetBindings( Control.BorderBrushProperty, ( object ) _param2, _param4, converter: ( IValueConverter ) new ColorToBrushConverter() );
		}
		else
			markerAnnotation2.Background = markerAnnotation2.BorderBrush = ( Brush ) Brushes.Gray;
	}

	protected void \u0023\u003DzY_lPK_VP\u0024B7_( Action _param1, bool _param2 )
	{
		this.\u0023\u003DztwYEX\u0024c\u003D(new Action<Action>( ( ( XamlHelper ) DrawableChartElementBaseViewModel.GetDispatcher() ).GuiAsync ), _param1, _param2);
	}

	protected void PerformUiAction( Action _param1, bool _param2 )
	{
		this.\u0023\u003DztwYEX\u0024c\u003D(new Action<Action>( ( ( XamlHelper ) DrawableChartElementBaseViewModel.GetDispatcher() ).GuiSync ), _param1, _param2);
	}

	private void \u0023\u003DztwYEX\u0024c\u003D(
		Action<Action> _param1,
		Action _param2,
		bool _param3)
  {
    DrawableChartElementBaseViewModel.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D yeuvfbi2ga1Q3dva4g = new DrawableChartElementBaseViewModel.\u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D();
yeuvfbi2ga1Q3dva4g._variableSome3535 = this;
yeuvfbi2ga1Q3dva4g.\u0023\u003Dz07PQx44\u003D = _param2;
Action action = _param3 ? new Action(yeuvfbi2ga1Q3dva4g.\u0023\u003Dz60l\u0024Ihha9C_kL2icwg\u003D\u003D) : yeuvfbi2ga1Q3dva4g.\u0023\u003Dz07PQx44\u003D;
if (DrawableChartElementBaseViewModel.IsUiThread() )
	action();
else
	_param1( action );
}

private void ResetY1Annotation()
{
	CollectionHelper.ForEach<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>>( ( IEnumerable<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>> ) this._renderseries2AxisMarker, DrawableChartElementBaseViewModel.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D ?? ( DrawableChartElementBaseViewModel.SomeClass34343383.\u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D = new Action<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>>( DrawableChartElementBaseViewModel.SomeClass34343383.SomeMethond0343.\u0023\u003DzrUT4mYySZlfIfzql4Q\u003D\u003D) ));
}

private void UpdateAndClear()
{
	this.Reset();
	this.Clear();
}

[Serializable]
private new sealed class SomeClass34343383
  {

	public static readonly DrawableChartElementBaseViewModel.SomeClass34343383 SomeMethond0343 = new DrawableChartElementBaseViewModel.SomeClass34343383();
public static Action<KeyValuePair<IRenderableSeries, AxisMarkerAnnotation>> \u0023\u003DzyKR7f5A3QPvTwxZODA\u003D\u003D;

public void \u0023\u003DzrUT4mYySZlfIfzql4Q\u003D\u003D(
      KeyValuePair<IRenderableSeries, AxisMarkerAnnotation> _param1)
    {
      _param1.Value.Y1 = (IComparable) null;
    }
  }

  private sealed class \u0023\u003DzlcqYEuvfbi2ga1Q3dva__4g\u003D
  {

	public DrawableChartElementBaseViewModel _variableSome3535;
public Action \u0023\u003Dz07PQx44\u003D;

public void \u0023\u003Dz60l\u0024Ihha9C_kL2icwg\u003D\u003D()
    {
      if (this._variableSome3535.IsDisposed())
        return;
this.\u0023\u003Dz07PQx44\u003D();
    }
  }
}
