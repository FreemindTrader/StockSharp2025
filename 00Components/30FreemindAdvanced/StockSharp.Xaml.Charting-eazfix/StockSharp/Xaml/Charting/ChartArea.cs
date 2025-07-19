using DevExpress.Xpf.Charts;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable enable
namespace StockSharp.Xaml.Charting;

[Display( ResourceType = typeof( LocalizedStrings ), Name = "ChartArea" )]
public class ChartArea : ChartPart<ChartArea>, IChartArea, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable, IChartPart<IChartArea>
{
    private ScichartSurfaceMVVM _chartSurfaceVM = null;
    private class PropertiesNotifyList<T> : BaseList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {

        private int _index;

        private T _property;


        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected override bool OnRemove( T _param1 )
        {
            this._index = base.IndexOf( _param1 );
            return base.OnRemove( _param1 );
        }

        protected override void OnRemoved( T _param1 )
        {
            if ( this._index >= 0 )
                this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, _param1, this._index );
            base.OnRemoved( _param1 );
        }

        protected override void OnInserted( int _param1, T isX )
        {
            this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, isX, _param1 );
            base.OnInserted( _param1, isX );
        }

        protected override void OnAdded( T _param1 )
        {
            this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Add, _param1, base.Count - 1 );
            base.OnAdded( _param1 );
        }

        protected override void OnRemoveAt( int _param1 )
        {
            this._property = base[ _param1 ];
            base.OnRemoveAt( _param1 );
        }

        protected override void OnRemovedAt( int _param1 )
        {
            if ( _property != null )
                this.RaiseCollectionChangedEvent( NotifyCollectionChangedAction.Remove, this._property, _param1 );
            base.OnRemovedAt( _param1 );
        }

        protected override void OnCleared()
        {
            this.ResetCollectionChangedEvent();
            base.OnCleared();
        }

        private void RaiseCollectionChangedEvent( NotifyCollectionChangedAction action, T changedItem, int index )
        {
            this.RaisePropertyChangedEvent( "Count" );
            this.RaisePropertyChangedEvent( "Item[]" );

            CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( action, changedItem, index ) );
        }

        private void ResetCollectionChangedEvent()
        {
            this.RaisePropertyChangedEvent( "Count" );
            this.RaisePropertyChangedEvent( "Item[]" );

            CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Reset ) );
        }

        private void RaisePropertyChangedEvent( string _param1 )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( _param1 ) );
        }
    }

    public IChart Chart
    {
        get => this._chart;

        set
        {
            if ( this._chart == value )
                return;

            if ( value == null )
                this.ViewModel.Release();

            this._chart = value;

            if ( value == null )
                return;

            this.ViewModel.InitPropertiesEventHandlers();

            Elements.ForEach( x =>
            {
                if ( !( x is IChartComponent elem ) )
                    return;
                elem.ResetUI();
            } );
        }
    }

    public INotifyList<IChartElement> Elements => this._chartElementNotifyList;


    private IChart _chart;

    private ChartAxisType _xAxisType = ChartAxisType.CategoryDateTime;

    private string _title;

    private double _height;

    private readonly INotifyList<IChartElement> _chartElementNotifyList;

    private readonly INotifyList<IChartAxis> _xAxisNotifyList;

    private readonly INotifyList<IChartAxis> _yAxisNotifyList;

    public INotifyList<IChartAxis> XAxises => this._xAxisNotifyList;

    public INotifyList<IChartAxis> YAxises => this._yAxisNotifyList;

    public ScichartSurfaceMVVM ViewModel
    {
        get
        {
            return _chartSurfaceVM;
        }

        set
        {
            _chartSurfaceVM = value;
        }

    }

    private sealed class DictionaryStruct03894
    {
        public ChartAxisType _someChartAxisType;
        public ChartArea _variableSome3535;

        public bool SomeLinqFunction3596(
          IChartComponent _param1 )
        {
            return !_param1.CheckAxesCompatible( new ChartAxisType?( this._someChartAxisType ), new ChartAxisType?() );
        }

        public bool AnotherSomeLinqFunction3596( IChartArea a )
        {
            return a != this._variableSome3535 && a.XAxisType != this._someChartAxisType;
        }
    }

    [Browsable( false )]
    public ChartAxisType XAxisType
    {
        get => this._xAxisType;

        set
        {
            if ( _xAxisType == value )
            {
                return;
            }

            if ( Chart != null )
            {
                Chart.EnsureUIThread();
            }

            if ( Elements.Cast<IChartComponent>().Any( i => !i.CheckAxesCompatible( new ChartAxisType?( value ), new ChartAxisType?() ) ) )
            {
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.ElementDontSupportAxisTypeParams, value ) );
            }

            ChartArea.DictionaryStruct03894 dkfA7SK9Zsjh7b7evY = new ChartArea.DictionaryStruct03894();
            dkfA7SK9Zsjh7b7evY._someChartAxisType = value;
            dkfA7SK9Zsjh7b7evY._variableSome3535 = this;


            if ( this.Chart != null && this.Chart.Areas.Any( a => a != this && a.XAxisType != value ) )
            {
                throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
            }
                
            this._xAxisType = dkfA7SK9Zsjh7b7evY._someChartAxisType;
            List<ChartAxis> chartAxisList = new List<ChartAxis>();
            foreach ( IChartAxis xaxise in ( IEnumerable<IChartAxis> ) this.XAxises )
            {
                ChartAxis chartAxis = new ChartAxis()
                {
                    Id = xaxise.Id,
                    AutoRange = xaxise.AutoRange,
                    AxisType = this._xAxisType
                };
                chartAxisList.Add( chartAxis );
            }
            IChart chart2 = this.Chart;
            this.Chart = ( IChart ) null;
            ICollection<IChartAxis> xaxises1 = (ICollection<IChartAxis>) this.XAxises;
            INotifyList<IChartAxis> xaxises2 = this.XAxises;
            int index = 0;
            IChartAxis[] chartAxisArray = new IChartAxis[((ICollection<IChartAxis>) xaxises2).Count];
            foreach ( IChartAxis chartAxis in ( IEnumerable<IChartAxis> ) xaxises2 )
            {
                chartAxisArray[ index ] = chartAxis;
                ++index;
            }
            CollectionHelper.RemoveRange<IChartAxis>( xaxises1, ( IEnumerable<IChartAxis> ) new \u0023\u003DzFxYNKQ1M2eiqODEcXA\u003D\u003D< IChartAxis > ( chartAxisArray ) );
            CollectionHelper.AddRange<IChartAxis>( ( ICollection<IChartAxis> ) this.XAxises, ( IEnumerable<IChartAxis> ) chartAxisList );
            this.Chart = chart2;
        }
    }

    private sealed class AxisNotifyList( ChartArea _param1, bool isX ) : ChartArea.PropertiesNotifyList<IChartAxis>
    {
        private sealed class SomeClass34343
        {
            public IChartAxis _someChartElement;

            public bool SomeClass34343Method01( IChartAxis _param1 )
            {
                return _param1.Id == this._someChartElement.Id;
            }
        }

        private static int _xAxisCount;

        private static int _yAxisCount;

        private readonly bool _isX = isX;

        private readonly ChartArea _chartArea = _param1 ?? throw new ArgumentNullException("area");

        private bool GetIsX()
        {
            return this._isX;
        }

        protected override bool OnAdding( IChartAxis axis )
        {
            ChartArea.AxisNotifyList.SomeClass34343 zPKCmcad6Nxc5A8A = new ChartArea.AxisNotifyList.SomeClass34343();
            zPKCmcad6Nxc5A8A._someChartElement = axis;


            string str = this.GetIsX() ? "X" : "Y";

            int yCount;

            if ( !this.GetIsX() )
                yCount = ++_yAxisCount;
            else
            {
                yCount = _xAxisCount + 1;
                _xAxisCount = yCount;
            }

            if ( StringHelper.IsEmpty( axis.Id ) )
            {
                axis.Id = $"{str}({Guid.NewGuid()})";
            }

            if ( this.Any( a => a.Id == axis.Id ) )
            {
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.AxisAlreadyAdded, axis.Id ) );
            }



            if ( this == _chartArea.XAxises && axis.AxisType != _chartArea.XAxisType )
            {
                axis.AxisType = _chartArea.XAxisType;

                //throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
            }

            foreach ( IChartComponent elem in ( ( IEnumerable ) this._chartArea.Elements ).Cast<IChartComponent>() )
            {
                if ( elem.TryGetXAxis() == null && this == this._area.XAxises && zPKCmcad6Nxc5A8A._someChartElement.Id == elem.XAxisId && !elem.CheckAxesCompatible( new ChartAxisType?( zPKCmcad6Nxc5A8A._someChartElement.AxisType ), new ChartAxisType?() ) )
                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
                if ( elem.TryGetYAxis() == null && this == this._area.YAxises && zPKCmcad6Nxc5A8A._someChartElement.Id == elem.YAxisId && !elem.CheckAxesCompatible( new ChartAxisType?(), new ChartAxisType?( zPKCmcad6Nxc5A8A._someChartElement.AxisType ) ) )
                    throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
            }


            if ( this.GetIsX() && StringHelper.IsEmpty( zPKCmcad6Nxc5A8A._someChartElement.Group ) )
                zPKCmcad6Nxc5A8A._someChartElement.Group = zPKCmcad6Nxc5A8A._someChartElement.AxisType.ToString() + zPKCmcad6Nxc5A8A._someChartElement.Id;
            if ( StringHelper.IsEmpty( zPKCmcad6Nxc5A8A._someChartElement.Title ) )
                zPKCmcad6Nxc5A8A._someChartElement.Title = str + num2.ToString();
            ( ( ChartAxis ) zPKCmcad6Nxc5A8A._someChartElement ).ChartArea = ( IChartArea ) this._area;
            return ( ( BaseCollection<IChartAxis, IList<IChartAxis>> ) this ).OnAdding( zPKCmcad6Nxc5A8A._someChartElement );
        }

        protected virtual bool OnRemoving( IChartAxis _param1 )
        {
            ChartAxis chartAxis = (ChartAxis) _param1;
            bool flag = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).Contains((IChartAxis) chartAxis);
            if ( flag && this._area.Chart != null && CompareHelper.IsDefault<ChartAxis>( chartAxis ) )
                throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
            int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnRemoving((IChartAxis) chartAxis) ? 1 : 0;
            if ( ( num & ( flag ? 1 : 0 ) ) == 0 )
                return num != 0;
            chartAxis.ChartArea = ( IChartArea ) null;
            return num != 0;
        }

        protected virtual bool OnRemovingAt( int _param1 )
        {
            ChartAxis chartAxis = (ChartAxis) ((BaseCollection<IChartAxis, IList<IChartAxis>>) this)[_param1];
            if ( CompareHelper.IsDefault<ChartAxis>( chartAxis ) && this._area.Chart != null )
                throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
            int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnRemovingAt(_param1) ? 1 : 0;
            if ( num == 0 )
                return num != 0;
            chartAxis.ChartArea = ( IChartArea ) null;
            return num != 0;
        }

        protected virtual bool OnClearing()
        {
            if ( this._area.Chart != null )
                throw new InvalidOperationException( LocalizedStrings.ErrorRemovingDefaultAxis );
            IChartAxis[] array = ((IEnumerable<IChartAxis>) this).ToArray<IChartAxis>();
            int num = ((BaseCollection<IChartAxis, IList<IChartAxis>>) this).OnClearing() ? 1 : 0;
            if ( num == 0 )
                return num != 0;
            CollectionHelper.ForEach<IChartAxis>( ( IEnumerable<IChartAxis> ) array, ChartArea.AxisNotifyList.SomeShittyClass33434.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D ?? ( ChartArea.AxisNotifyList.SomeShittyClass33434.\u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D = new Action<IChartAxis>( ChartArea.AxisNotifyList.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D) ));
            return num != 0;
        }

        [Serializable]
        private sealed class SomeShittyClass33434
        {
            public static readonly ChartArea.AxisNotifyList.SomeShittyClass33434 _someMemberOfShittyClass = new ChartArea.AxisNotifyList.SomeShittyClass33434();
            public static Action<IChartAxis> \u0023\u003DzaObIckm5bO9Zm0ifDA\u003D\u003D;

      public void \u0023\u003DzE70qt2sPjBBv095jMVMFSaY\u003D(IChartAxis _param1)
      {
        ((ChartAxis) _param1).ChartArea = (IChartArea) null;
      }
    }


}


private sealed class ChartElementNotifyList( ChartArea area ) : ChartArea.PropertiesNotifyList<IChartElement>
{

    private readonly ChartArea _area = area ?? throw new ArgumentNullException("area");

    protected override bool OnAdding( IChartElement element )
    {
        if ( element.Chart != null )
        {
            throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
        }

        if ( this.Any( i => i.Id == element.Id ) )
        {
            throw new InvalidOperationException( LocalizedStrings.ElementAlreadyAttached );
        }

        var elementXY = element as IElementWithXYAxes;

        Maybe.Do( elementXY, a =>
        {
            var axis = _area.YAxises.FirstOrDefault( i => i.Id == a.YAxisId );
            if ( !a.CheckAxesCompatible( new ChartAxisType?( _area.XAxisType ), axis?.AxisType ) )
            {
                throw new InvalidOperationException( StringHelper.Put( LocalizedStrings.AxesTypesNotSupportedParams, new object[ ]
                {
                                                       a.GetType( ).Name,
                                                      _area.XAxisType,
                                                      axis?.AxisType
                } ) );
            }
        } );

        return base.OnAdding( element );
    }        
  }
    
  

  public ChartArea()
    {
        this._chartElementNotifyList; = ( INotifyList<IChartElement> ) new ChartArea.ChartElementNotifyList( this );
        this._xAxisNotifyList = ( INotifyList<IChartAxis> ) new ChartArea.AxisNotifyList( this, true );
        this._yAxisNotifyList = ( INotifyList<IChartAxis> ) new ChartArea.AxisNotifyList( this, false );
        this.InitAxises();
        this._chartSurfaceVM = new ScichartSurfaceMVVM( this );
        this.Height = 100.0;
        this.ViewModel.PropertyChanged += new PropertyChangedEventHandler( this.\u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D);
    }

    public ScichartSurfaceMVVM ViewModel
    {
    return this._chartSurfaceVM;
    }

    private void InitAxises()
    {
        if ( !( ( IEnumerable<IChartAxis> ) this.XAxises ).Any<IChartAxis>( ChartArea.SomeShittyClass33434.method01 ?? ( ChartArea.SomeShittyClass33434.method01 = new Func<IChartAxis, bool>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003DzfnNqP9jz3szEAuvQ\u0024gr5C7U\u003D) ) ) )
            ( ( ICollection<IChartAxis> ) this.XAxises ).Add( ( IChartAxis ) new ChartAxis()
            {
                Id = "X",
                AutoRange = false,
                AxisType = ChartAxisType.CategoryDateTime
            } );
        if ( ( ( IEnumerable<IChartAxis> ) this.YAxises ).Any<IChartAxis>( ChartArea.SomeShittyClass33434.method02 ?? ( ChartArea.SomeShittyClass33434.method02 = new Func<IChartAxis, bool>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D) ) ) )
            return;
        ( ( ICollection<IChartAxis> ) this.YAxises ).Add( ( IChartAxis ) new ChartAxis()
        {
            Id = "Y",
            AxisType = ChartAxisType.Numeric
        } );
    }

    [Browsable( false )]




    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Name", Description = "ChartAreaName", GroupName = "Common", Order = 0 )]
    public string Title
    {
        get => this._title;
        set
        {
            this._title = value;
            this.RaisePropertyChanged( nameof( Title ) );
        }
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "GroupId", Description = "ChartPaneGroupDescription", GroupName = "Common", Order = 1 )]
    public string GroupId
    {
        get => this._groupId;
        set => this._groupId = value;
    }

    [Browsable( false )]
    public double Height
    {
        get => this._height;
        set
        {
            if ( Math.Abs( this._height - value ) < double.Epsilon )
                return;
            this._height = value;
            this.RaisePropertyChanged( nameof( Height ) );
        }
    }





    public override void Load( SettingsStorage storage )
    {
        ( ( ICollection<IChartElement> ) this.Elements ).Clear();
        base.Load( storage );
        this.Title = storage.GetValue<string>( "Title", ( string ) null );
        this.Height = storage.GetValue<double>( "Height", 0.0 );
        this.XAxisType = storage.GetValue<ChartAxisType>( "XAxisType", this.XAxisType );
        this.GroupId = storage.GetValue<string>( "GroupId", this.GroupId );
        ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, "XAxises", ( ICollection<IChartAxis> ) this.XAxises);
        ChartArea.\u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(storage, "YAxises", ( ICollection<IChartAxis> ) this.YAxises);
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<string>( "Title", this.Title );
        storage.SetValue<double>( "Height", this.Height );
        storage.SetValue<ChartAxisType>( "XAxisType", this.XAxisType );
        storage.SetValue<string>( "GroupId", this.GroupId );
        storage.SetValue<SettingsStorage[ ]>( "XAxises", ( ( IEnumerable<IChartAxis> ) this.XAxises ).Select<IChartAxis, SettingsStorage>( ChartArea.SomeShittyClass33434.Method04 ?? ( ChartArea.SomeShittyClass33434.Method04 = new Func<IChartAxis, SettingsStorage>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003Dzxv2ll83UBK_RmlktVQ\u003D\u003D) ) ).ToArray<SettingsStorage>() );
        storage.SetValue<SettingsStorage[ ]>( "YAxises", ( ( IEnumerable<IChartAxis> ) this.YAxises ).Select<IChartAxis, SettingsStorage>( ChartArea.SomeShittyClass33434.Method05 ?? ( ChartArea.SomeShittyClass33434.Method05 = new Func<IChartAxis, SettingsStorage>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003Dzx2FJ4suusAb7GDyK1w\u003D\u003D) ) ).ToArray<SettingsStorage>() );
    }

  private static void \u0023\u003Dz4w\u0024DGYrkGMNXjRkcgg\u003D\u003D(
    SettingsStorage _param0,
    string _param1,
    ICollection<IChartAxis> isX)
  {
    IEnumerable<SettingsStorage> source = _param0.GetValue<IEnumerable<SettingsStorage>>(_param1, (IEnumerable<SettingsStorage>) null);
    if (source == null)
      return;
    isX.Clear();
    CollectionHelper.AddRange<IChartAxis>(isX, (IEnumerable<IChartAxis>) source.Select<SettingsStorage, ChartAxis>(ChartArea.SomeShittyClass33434.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D ?? (ChartArea.SomeShittyClass33434.\u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D = new Func<SettingsStorage, ChartAxis>(ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003Dzr2ViHsE5u5Iy0l8GPqzpBAb_0ZNf))));
  }

  public virtual ChartArea Clone()
{
    ChartArea chartArea = this.Clone(new ChartArea()
    {
        Title = this.Title,
        Height = this.Height,
        XAxisType = this.XAxisType
    });
    CollectionHelper.AddRange<IChartElement>( ( ICollection<IChartElement> ) chartArea.Elements, ( ( IEnumerable<IChartElement> ) this.Elements ).Select<IChartElement, IChartElement>( ChartArea.SomeShittyClass33434.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_ ?? ( ChartArea.SomeShittyClass33434.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_ = new Func<IChartElement, IChartElement>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003DzLhPQ\u0024JfQhEkyu0vUWg\u003D\u003D) ) ));
    ( ( ICollection<IChartAxis> ) chartArea.XAxises ).Clear();
    CollectionHelper.AddRange<IChartAxis>( ( ICollection<IChartAxis> ) chartArea.XAxises, ( ( IEnumerable<IChartAxis> ) this.XAxises ).Select<IChartAxis, IChartAxis>( ChartArea.SomeShittyClass33434.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_ ?? ( ChartArea.SomeShittyClass33434.public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_ = new Func<IChartAxis, IChartAxis>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003DzGeF_1AAuPyWwDbL_iA\u003D\u003D) ) ));
    ( ( ICollection<IChartAxis> ) chartArea.YAxises ).Clear();
    CollectionHelper.AddRange<IChartAxis>( ( ICollection<IChartAxis> ) chartArea.YAxises, ( ( IEnumerable<IChartAxis> ) this.YAxises ).Select<IChartAxis, IChartAxis>( ChartArea.SomeShittyClass33434.Func_IDrawableChartElement_bool_098 ?? ( ChartArea.SomeShittyClass33434.Func_IDrawableChartElement_bool_098 = new Func<IChartAxis, IChartAxis>( ChartArea.SomeShittyClass33434._someMemberOfShittyClass.\u0023\u003Dzf4yIUkrz2a0As47tiA\u003D\u003D) ) ));
    return chartArea;
}

public virtual string ToString() => this.Title;

public void Dispose()
{
    this.ViewModel.Dispose();
    GC.SuppressFinalize( ( object ) false );
}

private void \u0023\u003Dzg7PFOA2RIl9h1rTv9w\u003D\u003D(
    #nullable enable
    object? _param1,
    PropertyChangedEventArgs isX)
  {
    if (!(isX.PropertyName == "PaneGroupSuffix"))
      return;
this.RaisePropertyChanged( "GroupId" );
  }

  [Serializable]
private sealed class SomeShittyClass33434
{
    public static readonly 
    #nullable disable
    ChartArea.SomeShittyClass33434 _someMemberOfShittyClass = new ChartArea.SomeShittyClass33434();
    public static Func<IChartAxis, bool> method01;
    public static Func<IChartAxis, bool> method02;
    public static Action<IChartElement> method03;
    public static Func<IChartAxis, SettingsStorage> Method04;
    public static Func<IChartAxis, SettingsStorage> Method05;
    public static Func<SettingsStorage, ChartAxis> \u0023\u003DzXRCsJbKV5qeZQYBJDA\u003D\u003D;
    public static Func<IChartElement, IChartElement> public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_bool_;
    public static Func<IChartAxis, IChartAxis> public_static_Func_KeyValuePair_IChartComponent_ChartComponentViewModel_IChartComponent_;
    public static Func<IChartAxis, IChartAxis> Func_IDrawableChartElement_bool_098;

    public bool \u0023\u003DzfnNqP9jz3szEAuvQ\u0024gr5C7U\u003D(IChartAxis _param1)
    {
      return _param1.Id == "X";
    }

public bool \u0023\u003Dz\u0024PoY\u0024FfmSWryZZmtwAl\u0024D38\u003D(IChartAxis _param1)
    {
      return _param1.Id == "Y";
    }

    public void \u0023\u003DzM30dyEF9Fb2bzYLjmLgjtiE\u003D(IChartElement _param1)
    {
      if (!(_param1 is IChartComponent ddznyiGmdRlAevOq))
        return;
      ddznyiGmdRlAevOq.ResetUI();
    }

    public SettingsStorage \u0023\u003Dzxv2ll83UBK_RmlktVQ\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }

    public SettingsStorage \u0023\u003Dzx2FJ4suusAb7GDyK1w\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Save((IPersistable) _param1);
    }

    public ChartAxis \u0023\u003Dzr2ViHsE5u5Iy0l8GPqzpBAb_0ZNf(SettingsStorage _param1)
    {
      return PersistableHelper.Load<ChartAxis>(_param1);
    }

    public IChartElement \u0023\u003DzLhPQ\u0024JfQhEkyu0vUWg\u003D\u003D(IChartElement _param1)
    {
      return PersistableHelper.Clone<IChartElement>(_param1);
    }

    public IChartAxis \u0023\u003DzGeF_1AAuPyWwDbL_iA\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Clone<IChartAxis>(_param1);
    }

    public IChartAxis \u0023\u003Dzf4yIUkrz2a0As47tiA\u003D\u003D(IChartAxis _param1)
    {
      return PersistableHelper.Clone<IChartAxis>(_param1);
    }
  }

  

  

  

  
}
