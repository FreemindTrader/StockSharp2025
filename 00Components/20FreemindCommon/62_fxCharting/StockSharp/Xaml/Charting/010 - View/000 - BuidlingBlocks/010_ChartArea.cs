using DevExpress.Charts.Model;
using DevExpress.Xpf.Charts;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Xaml.Charting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

#nullable enable
namespace StockSharp.Xaml.Charting;

/// <summary>Chart area.</summary>
[Display(ResourceType = typeof(LocalizedStrings), Name = "ChartArea")]
public class ChartArea : ChartPart<ChartArea>, IChartArea, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable, IChartPart<IChartArea>
{
    private ScichartSurfaceMVVM _chartSurfaceVM = null;
    public INotifyList<IChartElement> Elements => _chartElementNotifyList;

    private IChart _chart;

    private ChartAxisType _xAxisType = ChartAxisType.CategoryDateTime;

    private string _title;

    private double _height;

    private readonly INotifyList<IChartElement> _chartElementNotifyList;

    private readonly INotifyList<IChartAxis> _xAxisNotifyList;

    private readonly INotifyList<IChartAxis> _yAxisNotifyList;

    public INotifyList<IChartAxis> XAxises => _xAxisNotifyList;

    public INotifyList<IChartAxis> YAxises => _yAxisNotifyList;

    /// <summary>
    /// Tony Added:
    /// </summary>
    private int _indicatorCount = 0;

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

    public IChart Chart
    {
        get => _chart;

        set
        {
            if ( _chart == value )
                return;

            if ( value == null )
                ViewModel.Release();

            _chart = value;

            if ( value == null )
                return;

            ViewModel.InitPropertiesEventHandlers();

            Elements.ForEach(x =>
            {
                if ( !( x is IChartComponent elem ) )
                    return;
                elem.ResetUI();
            });
        }
    }



    [Browsable(false)]
    public ChartAxisType XAxisType
    {
        get => _xAxisType;

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

            if ( Elements.Cast<IChartComponent>().Any(i => !i.CheckAxesCompatible(new ChartAxisType?(value), new ChartAxisType?())) )
            {
                throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.ElementDontSupportAxisTypeParams, value));
            }
            

            if ( Chart != null && Chart.Areas.Any(a => a != this && a.XAxisType != value) )
            {
                throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
            }

            _xAxisType = value;
            List<ChartAxis> chartAxisList = new List<ChartAxis>();
            foreach ( IChartAxis xaxise in (IEnumerable<IChartAxis>)XAxises )
            {
                ChartAxis chartAxis = new ChartAxis()
                {
                    Id = xaxise.Id,
                    AutoRange = xaxise.AutoRange,
                    AxisType = _xAxisType
                };
                chartAxisList.Add(chartAxis);
            }

            IChart oldChart = Chart;
            Chart = null;

            int index = 0;
            var chartAxisArray = new IChartAxis[XAxises.Count];

            foreach ( var chartAxis in XAxises )
            {
                chartAxisArray[index] = chartAxis;
                ++index;
            }
            CollectionHelper.RemoveRange(XAxises, new List<IChartAxis>(chartAxisArray));
            CollectionHelper.AddRange(XAxises, (IEnumerable<IChartAxis>)chartAxisList);
            Chart = oldChart;
        }
    }
    internal class PropertiesNotifyList<T> : BaseList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private int _index;
        private T _property;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected override bool OnRemove(T property)
        {
            _index = base.IndexOf(property);
            return base.OnRemove(property);
        }

        protected override void OnRemoved(T property)
        {
            if ( _index >= 0 )
                RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Remove, property, _index);
            base.OnRemoved(property);
        }

        protected override void OnInserted(int _param1, T property)
        {
            RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Add, property, _param1);
            base.OnInserted(_param1, property);
        }

        protected override void OnAdded(T property)
        {
            RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Add, property, Count - 1);
            base.OnAdded(property);
        }

        protected override void OnRemoveAt(int index)
        {
            _property = base[index];
            base.OnRemoveAt(index);
        }

        protected override void OnRemovedAt(int index)
        {
            if ( _property != null )
                RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Remove, _property, index);
            base.OnRemovedAt(index);
        }

        protected override void OnCleared()
        {
            ResetCollectionChangedEvent();
            base.OnCleared();
        }

        private void RaiseCollectionChangedEvent(NotifyCollectionChangedAction action, T changedItem, int index)
        {
            RaisePropertyChangedEvent("Count");
            RaisePropertyChangedEvent("Item[]");

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItem, index));
        }

        private void ResetCollectionChangedEvent()
        {
            RaisePropertyChangedEvent("Count");
            RaisePropertyChangedEvent("Item[]");

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void RaisePropertyChangedEvent(string _param1)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_param1));
        }
    }

    private sealed class AxisNotifyList(ChartArea area, bool isX) : ChartArea.PropertiesNotifyList<IChartAxis>
    {
        private static int _xAxisCount;
        private static int _yAxisCount;
        private readonly bool _isX = isX;
        private readonly ChartArea _chartArea = area ?? throw new ArgumentNullException("area");

        private bool GetIsX()
        {
            return _isX;
        }

        protected override bool OnAdding(IChartAxis axis)
        {
            string axisId = GetIsX() ? "X" : "Y";

            int yCount;

            if ( GetIsX() )
            {
                _xAxisCount = yCount = _xAxisCount + 1;
            }
            else
            {
                yCount = ++_yAxisCount;
            }

            if ( StringHelper.IsEmpty(axis.Id) )
            {
                axis.Id = $"{axisId}({Guid.NewGuid()})";
            }

            if ( this.Any(a => a.Id == axis.Id) )
            {
                throw new InvalidOperationException(StringHelper.Put(LocalizedStrings.AxisAlreadyAdded, axis.Id));
            }


            if ( this == _chartArea.XAxises && axis.AxisType != _chartArea.XAxisType )
            {
                //axis.AxisType = _chartArea.XAxisType;

                throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
            }

            foreach ( var elem in _chartArea.Elements.Cast<IChartComponent>() )
            {
                if ( elem.TryGetXAxis() == null && this == _chartArea.XAxises && axis.Id == elem.XAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(axis.AxisType), new ChartAxisType?()) )
                {
                    throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
                }

                if ( elem.TryGetYAxis() == null && this == _chartArea.YAxises && axis.Id == elem.YAxisId && !elem.CheckAxesCompatible(new ChartAxisType?(), new ChartAxisType?(axis.AxisType)) )
                {
                    throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
                }
            }


            if ( GetIsX() && StringHelper.IsEmpty(axis.Group) )
            {
                axis.Group = axis.AxisType.ToString() + axis.Id;
            }


            if ( StringHelper.IsEmpty(axis.Title) )
            {
                axis.Title = axisId + yCount.ToString();
            }

            ( (ChartAxis)axis ).ChartArea = (IChartArea)_chartArea;

            return base.OnAdding(axis);
        }

        protected override bool OnRemoving(IChartAxis axis)
        {
            ChartAxis chartAxis = (ChartAxis)axis;
            bool hasAxis = Contains(chartAxis);

            if ( hasAxis && _chartArea.Chart != null && CompareHelper.IsDefault<ChartAxis>(chartAxis) )
                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);


            if ( ( base.OnRemoving(chartAxis) &  hasAxis ) == false )
                return false;

            chartAxis.ChartArea = null;

            return true;
        }

        protected override bool OnRemovingAt(int index)
        {
            var chartAxis = (ChartAxis)this[index];

            if ( CompareHelper.IsDefault(chartAxis) && _chartArea.Chart != null )
            {
                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
            }

            if ( base.OnRemovingAt(index) )
            {
                chartAxis.ChartArea = null;

                return true;
            }

            return false;
        }

        protected override bool OnClearing()
        {
            if ( _chartArea.Chart != null )
                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);

            IChartAxis[] axises = this.ToArray<IChartAxis>();

            if ( base.OnClearing() )
            {
                foreach ( ChartAxis axis in axises )
                {
                    axis.ChartArea = null;
                }

                return true;
            }

            return false;
        }
    }



    internal sealed class ChartElementNotifyList(ChartArea area) : ChartArea.PropertiesNotifyList<IChartElement>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ChartArea _area = area ?? throw new ArgumentNullException("area");

        protected override bool OnAdding(IChartElement elem)
        {
            if ( elem.TryGetChart() != null )
            {
                throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
            }

            var result = !this.Any(i => i.Id == elem.Id) ? elem as IChartComponent : throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
            if ( result != null )
            {
                IChartAxis chartAxis = this._area.YAxises.FirstOrDefault(i => i.Id == result.YAxisId);

                if ( !result.CheckAxesCompatible(new ChartAxisType?(this._area.XAxisType), chartAxis?.AxisType) )
                    throw new InvalidOperationException(
                        StringHelper.Put(
                            LocalizedStrings.AxesTypesNotSupportedParams,
                            result.GetType().Name,
                            this._area.XAxisType,
                            chartAxis?.AxisType));

            }
            return base.OnAdding(elem);
        }


    }


    /// <summary>
    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartArea" />.
    /// </summary>
    public ChartArea()
    {
        _chartElementNotifyList = (INotifyList<IChartElement>)new ChartArea.ChartElementNotifyList(this);
        _xAxisNotifyList = (INotifyList<IChartAxis>)new ChartArea.AxisNotifyList(this, true);
        _yAxisNotifyList = (INotifyList<IChartAxis>)new ChartArea.AxisNotifyList(this, false);
        InitAxises();
        _chartSurfaceVM = new ScichartSurfaceMVVM(this);
        Height = 100.0;
        ViewModel.PropertyChanged += new PropertyChangedEventHandler(OnPaneGroupSuffixChanged);
    }

    public ChartArea(int count)
    {
        _indicatorCount = count;
        _chartElementNotifyList = new ChartElementNotifyList(this);
        _xAxisNotifyList = (INotifyList<IChartAxis>)new ChartArea.AxisNotifyList(this, true);
        _yAxisNotifyList = (INotifyList<IChartAxis>)new ChartArea.AxisNotifyList(this, false);
        InitAxises(count);
        Height = 200f;
    }



    private void InitAxises()
    {
        if ( !XAxises.Any(a => a.Id == "X") )
        {
            XAxises.Add(new ChartAxis() { Id = "X", AutoRange = false, AxisType = ChartAxisType.CategoryDateTime });
        }

        if ( !YAxises.Any(a => a.Id == "Y") )
        {
            YAxises.Add(new ChartAxis() { Id = "Y", AxisType = ChartAxisType.Numeric });
        }

    }

    /// <summary>
    /// Tony Added:
    /// </summary>
    /// <param name="count"></param>
    private void InitAxises(int count)
    {
        string newX = "X";

        if ( XAxises.FirstOrDefault(x => x.Id == newX) == null )
        {
            XAxises.Add(new ChartAxis() { Id = newX, AxisType = ChartAxisType.CategoryDateTime });
        }

        string newY = "Y";

        if ( YAxises.FirstOrDefault(y => y.Id == newY) == null )
        {
            YAxises.Add(new ChartAxis() { Id = newY, AxisType = ChartAxisType.Numeric });
        }
    }

    [Browsable(false)]
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Name", Description = "ChartAreaName", GroupName = "Common", Order = 0)]
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            RaisePropertyChanged(nameof(Title));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "GroupId", Description = "ChartPaneGroupDescription", GroupName = "Common", Order = 1)]
    public string GroupId
    {
        get => ViewModel.PaneGroupSuffix;
        set => ViewModel.PaneGroupSuffix = value;
    }

    [Browsable(false)]
    public double Height
    {
        get => _height;
        set
        {
            if ( Math.Abs(_height - value) < double.Epsilon )
                return;
            _height = value;
            RaisePropertyChanged(nameof(Height));
        }
    }



    public override void Load(SettingsStorage storage)
    {
        ( (ICollection<IChartElement>)Elements ).Clear();
        base.Load(storage);
        Title = storage.GetValue<string>( "Title" );
        Height = storage.GetValue<double>("Height", 0.0);
        XAxisType = storage.GetValue<ChartAxisType>("XAxisType", XAxisType);
        GroupId = storage.GetValue<string>("GroupId", GroupId);
        ChartArea.LoadAxises(storage, "XAxises", (ICollection<IChartAxis>)XAxises);
        ChartArea.LoadAxises(storage, "YAxises", (ICollection<IChartAxis>)YAxises);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<string>("Title", Title);
        storage.SetValue<double>("Height", Height);
        storage.SetValue<ChartAxisType>("XAxisType", XAxisType);
        storage.SetValue<string>("GroupId", GroupId);
        storage.SetValue<SettingsStorage[]>("XAxises", XAxises.Select(x => PersistableHelper.Save(x)).ToArray());
        storage.SetValue<SettingsStorage[]>("YAxises", YAxises.Select(y => PersistableHelper.Save(y)).ToArray());
    }

    private static void LoadAxises(SettingsStorage settings, string axisName, ICollection<IChartAxis> Axises)
    {
        IEnumerable<SettingsStorage> source = settings.GetValue<IEnumerable<SettingsStorage>>(axisName, null);
        if ( source == null )
            return;
        Axises.Clear();
        CollectionHelper.AddRange(Axises, source.Select(s => PersistableHelper.Load<ChartAxis>(s)));
    }

    /// <summary>
    /// Create a copy of <see cref="T:StockSharp.Xaml.Charting.ChartArea" />.
    /// </summary>
    /// <returns>Copy.</returns>
    public override ChartArea Clone()
    {
        ChartArea chartArea = Clone(new ChartArea()
        {
            Title = Title,
            Height = Height,
            XAxisType = XAxisType
        });
        CollectionHelper.AddRange(chartArea.Elements, Elements.Select(e => PersistableHelper.Clone(e)));

        chartArea.XAxises.Clear();
        CollectionHelper.AddRange(chartArea.XAxises, XAxises.Select(e => PersistableHelper.Clone(e)));

        chartArea.YAxises.Clear();
        CollectionHelper.AddRange(chartArea.YAxises, YAxises.Select(e => PersistableHelper.Clone(e)));

        return chartArea;
    }

    public override string ToString() => Title;

    public void Dispose()
    {
        ViewModel.Dispose();
        GC.SuppressFinalize((object)false);
    }

    private void OnPaneGroupSuffixChanged(object? _param1, PropertyChangedEventArgs isX)
    {
        if ( !( isX.PropertyName == "PaneGroupSuffix" ) )
            return;
        RaisePropertyChanged("GroupId");
    }
}





//using Ecng.Collections;
//using Ecng.Common;
//using Ecng.Serialization;
//using Ecng.Xaml;
//using SciChart.Charting.Common;
//using StockSharp.Localization;
//using System;
//using System.Collections.Generic; 
//using fx.Collections;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading;
//using StockSharp.Xaml.Charting.ATony;
//using StockSharp.Xaml.Charting.Definitions;
//using SciChart.Charting.ChartModifiers;
//using StockSharp.Charting;

//namespace StockSharp.Xaml.Charting;


///// <summary>Chart area.</summary>

//[Display(ResourceType = typeof (LocalizedStrings), Name = "ChartArea")]
//public class ChartArea : ChartPart<ChartArea>, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable
//{
//    public static readonly string XAxisId = "X";
//    public static readonly string YAxisId = "Y";

//    private readonly SynchronizedList<string>   _stackTrace = new SynchronizedList<string>();
//    private ChartAxisType                         _xAxisType = ChartAxisType.CategoryDateTime;
//    private ScichartSurfaceMVVM                     _chartSurfaceVM;
//    private IChartEx                                _chart;
//    private string                                _title;
//    private float                                 _height;
//    private readonly INotifyList<IChartElement> _chartElementNotifyList;
//    private readonly INotifyList<ChartAxis>        _xAxisNotifyList;
//    private readonly INotifyList<ChartAxis>        _yAxisNotifyList;
//    private int _indicatorCount = 0;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="T:StockSharp.Xaml.Charting.ChartArea" />.
//    /// </summary>
//    public ChartArea()
//    {
//        _chartElementNotifyList = new ChartElementNotifyList(this);
//        _xAxisNotifyList = new AxisNotifyList(this, XAxisId, (a, i) => ((int)a.AxisType).ToString() + a.Id);
//        _yAxisNotifyList = new AxisNotifyList(this, YAxisId, (a, i) => ((int)a.AxisType).ToString() + a.Id + i);
//        InitAxises();
//        Height = 100f;
//    }


//    public ChartArea(int count)
//    {
//        _indicatorCount = count;
//        _chartElementNotifyList = new ChartElementNotifyList(this);
//        _xAxisNotifyList = new AxisNotifyList(this, XAxisId, (a, i) => ((int) a.AxisType).ToString() + a.Id);
//        _yAxisNotifyList = new AxisNotifyList(this, YAxisId, (a, i) => ((int) a.AxisType).ToString() + a.Id + i);
//        InitAxises(count);
//        Height = 200f;
//    }


//    public ScichartSurfaceMVVM ViewModel
//    {
//        get
//        {
//            return _chartSurfaceVM;
//        }

//        set
//        {
//            _chartSurfaceVM = value;
//        }
//    }

//    private void InitAxises()
//    {
//        if(XAxises.FirstOrDefault(x => x.Id == XAxisId) == null)
//        {
//            XAxises.Add(new ChartAxis() { Id = XAxisId, AxisType = ChartAxisType.CategoryDateTime });
//        }

//        if(YAxises.FirstOrDefault(y => y.Id == YAxisId) != null)
//        {
//            return;
//        }

//        var yAxis = new ChartAxis() { Id = YAxisId, AxisType = ChartAxisType.Numeric };

//        YAxises.Add(yAxis);
//    }

//    private void InitAxises(int count)
//    {
//        string newX = XAxisId;// + count.ToString( );

//        if(XAxises.FirstOrDefault(x => x.Id == newX) == null)
//        {
//            XAxises.Add(new ChartAxis() { Id = newX, AxisType = ChartAxisType.CategoryDateTime });
//        }

//        string newY = YAxisId;// + count.ToString( );

//        if(YAxises.FirstOrDefault(y => y.Id == newY) == null)
//        {
//            YAxises.Add(new ChartAxis() { Id = newY, AxisType = ChartAxisType.Numeric });
//        }
//    }

//    internal SynchronizedList<string> GetStackTrace()
//    {
//        return _stackTrace;
//    }

//    [Browsable(false)]
//    public IChartEx Chart
//    {
//        get
//        {
//            return _chart;
//        }
//        internal set
//        {
//            if(_chart == value)
//            {
//                return;
//            }

//            GetStackTrace()
//                .Add(string.Format("(tid={0})\n", Thread.CurrentThread.ManagedThreadId) + Environment.StackTrace);

//            if(value == null)
//            {
//                ViewModel.Release();
//            }

//            _chart = value;

//            if(value == null)
//            {
//                return;
//            }

//            int count = 0;

//            if(value is ChartExViewModel)
//            {
//                count = ((ChartExViewModel) value).ChartCount;
//            }


//            if(count > 0)
//            {
//                InitAxises(count);
//            } else
//            {
//                InitAxises();
//            }


//            ViewModel.InitPropertiesEventHandlers();
//        }
//    }


//    [Browsable(false)]
//    public ChartAxisType XAxisType
//    {
//        get
//        {
//            return _xAxisType;
//        }
//        set
//        {
//            if(_xAxisType == value)
//            {
//                return;
//            }
//            if(Chart != null)
//            {
//                Chart.EnsureUIThread();
//            }

//            if(Elements.Cast<IChartComponent>()
//                .Any(i => !i.CheckAxesCompatible(new ChartAxisType?(value), new ChartAxisType?())))
//            {
//                throw new InvalidOperationException(
//                    StringHelper.Put(LocalizedStrings.ElementDontSupportAxisTypeParams, value));
//            }

//            if(Chart != null && Chart.XAxisType != value)
//            {
//                throw new InvalidOperationException(LocalizedStrings.InvalidAxisType);
//            }

//            _xAxisType = value;

//            PooledList<ChartAxis> chartAxisList = new PooledList<ChartAxis>();

//            foreach(ChartAxis xAxis in XAxises)
//            {
//                ChartAxis chartAxis = new ChartAxis()
//                {
//                    Id = xAxis.Id,
//                    AutoRange = xAxis.AutoRange,
//                    AxisType = _xAxisType
//                };
//                chartAxisList.Add(chartAxis);
//            }

//            IChartEx resetChart = Chart;
//            Chart = null;

//            XAxises.RemoveRange(XAxises.ToArray());
//            XAxises.AddRange(chartAxisList);
//            Chart = resetChart;
//        }
//    }

//    [Display(
//        Description = "Str1905",
//        GroupName = "Common",
//        Name = "Name",
//        Order = 0,
//        ResourceType = typeof(LocalizedStrings))]
//    public string Title
//    {
//        get
//        {
//            return _title;
//        }
//        set
//        {
//            _title = value;
//            RaisePropertyChanged(nameof(Title));
//        }
//    }

//    [Browsable(false)]
//    public float Height
//    {
//        get
//        {
//            return _height;
//        }
//        set
//        {
//            _height = value;
//        }
//    }

//    [Browsable(false)]
//    public INotifyList<IChartElement> Elements
//    {
//        get
//        {
//            return _chartElementNotifyList;
//        }
//    }

//    public INotifyList<ChartAxis> XAxises
//    {
//        get
//        {
//            return _xAxisNotifyList;
//        }
//    }

//    public INotifyList<ChartAxis> YAxises
//    {
//        get
//        {
//            return _yAxisNotifyList;
//        }
//    }

//    public override void Load(SettingsStorage storage)
//    {
//        Elements.Clear();
//        base.Load(storage);
//        Title = storage.GetValue<string>("Title", null);
//        Height = storage.GetValue<float>("Height", 0);
//        XAxisType = ChartAxisType.CategoryDateTime;
//        //XAxisType = storage.GetValue<ChartAxisType>( "XAxisType", ChartAxisType.CategoryDateTime );
//        LoadAxises(storage, "XAxises", XAxises);
//        LoadAxises(storage, "YAxises", YAxises);
//        LoadChartElements<ChartCandleElement>(storage, "Candles");
//        LoadChartElements<ChartIndicatorElement>(storage, "Indicators");
//        LoadChartElements<TradesUI>(storage, "Trades");
//        LoadChartElements<OrdersUI>(storage, "Orders");
//    }

//    public override void Save(SettingsStorage storage)
//    {
//        base.Save(storage);
//        storage.SetValue("Title", Title);
//        storage.SetValue("Height", Height);
//        storage.SetValue("XAxisType", XAxisType);
//        storage.SetValue("XAxises", XAxises.Select(x => x.Save()).ToArray());
//        storage.SetValue("YAxises", YAxises.Select(y => y.Save()).ToArray());
//        SaveChartElments<ChartCandleElement>(storage, "Candles");
//        SaveChartElments<ChartIndicatorElement>(storage, "Indicators");
//        SaveChartElments<TradesUI>(storage, "Trades");
//        SaveChartElments<OrdersUI>(storage, "Orders");
//    }

//    private void LoadChartElements<T>(SettingsStorage settings, string name) where T : ChartComponentView<T>, new()
//    {
//        Elements.AddRange(settings.GetValue<IEnumerable<SettingsStorage>>(name, null).Select(s => s.Load<T>()));
//    }

//    private void SaveChartElments<T>(SettingsStorage settings, string name) where T : ChartComponentView<T>
//    {
//        settings.SetValue(name, Elements.OfType<T>().Select(s => s.Save()).ToArray());
//    }

//    private static void LoadAxises(SettingsStorage settings, string axisName, ICollection<ChartAxis> Axises)
//    {
//        var source = settings.GetValue<IEnumerable<SettingsStorage>>(axisName, null);

//        if(source == null)
//        {
//            return;
//        }
//        Axises.Clear();

//        Axises.AddRange(source.Select(s => s.Load<ChartAxis>()));
//    }

//    public override ChartArea Clone()
//    {
//        ChartArea chartArea = Clone(new ChartArea() { Title = Title, Height = Height, XAxisType = XAxisType });

//        chartArea.Elements.AddRange(Elements.Select(e => e.Clone()));

//        chartArea.XAxises.Clear();
//        chartArea.XAxises.AddRange(XAxises.Select(x => x.Clone()));

//        chartArea.YAxises.Clear();
//        chartArea.YAxises.AddRange(YAxises.Select(y => y.Clone()));
//        return chartArea;
//    }

//    public override string ToString()
//    {
//        return Title;
//    }

//    public void Dispose()
//    {
//        ViewModel.Dispose();
//    }

//    private sealed class AxisNotifyList : PropertiesNotifyList<ChartAxis>
//    {
//        private static int _xAxisCount;
//        private static int _yAxisCount;
//        private readonly string _defaultId;
//        private readonly ChartArea _chartArea;
//        private readonly Func<ChartAxis, int, string> _getDefaultGroupFunc;

//        public AxisNotifyList(ChartArea chartArea_1, string string_1, Func<ChartAxis, int, string> func_1)
//        {
//            ChartArea chartArea = chartArea_1;
//            if(chartArea == null)
//            {
//                throw new ArgumentNullException("area");
//            }
//            _chartArea = chartArea;
//            string str = string_1;
//            if(str == null)
//            {
//                throw new ArgumentNullException("defaultId");
//            }
//            _defaultId = str;
//            Func<ChartAxis, int, string> func = func_1;
//            if(func == null)
//            {
//                throw new ArgumentNullException("getDefaultGroup");
//            }
//            _getDefaultGroupFunc = func;
//        }

//        private static int GetAxisCount(string string_1)
//        {
//            if(!(string_1 == XAxisId))
//            {
//                return ++_yAxisCount;
//            }
//            return ++_xAxisCount;
//        }

//        protected override bool OnAdding(ChartAxis axis)
//        {
//            // FIXME:


//            throw new NotImplementedException();
//            //int num = GetAxisCount( _defaultId );

//            //if( StringHelper.IsEmpty( axis.Id ) )
//            //{
//            //    axis.Id = string.Format( "{0}({1})", _defaultId, Guid.NewGuid( ) );
//            //}

//            //if( Any( a => a.Id == axis.Id ) )
//            //{
//            //    throw new InvalidOperationException( "StringHelper.Put( LocalizedStrings.Str1904Params, axis.Id )" );
//            //}

//            //if( this == _chartArea.XAxises && axis.AxisType != _chartArea.XAxisType )
//            //{
//            //    axis.AxisType = _chartArea.XAxisType;

//            //    //throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
//            //}

//            //foreach( IChartComponent axisElement in _chartArea.Elements.Cast< IChartComponent >( ) )
//            //{
//            //    if( axisElement.XAxis == null && this == _chartArea.XAxises && axis.Id == axisElement.XAxisId && !axisElement.CheckAxesCompatible( new ChartAxisType?( axis.AxisType ), new ChartAxisType?( ) ) )
//            //    {
//            //        throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
//            //    }

//            //    if( axisElement.YAxis == null && this == _chartArea.YAxises && axis.Id == axisElement.YAxisId && !axisElement.CheckAxesCompatible( new ChartAxisType?( ), new ChartAxisType?( axis.AxisType ) ) )
//            //    {
//            //        throw new InvalidOperationException( LocalizedStrings.InvalidAxisType );
//            //    }
//            //}

//            //if( axis.Group.IsEmpty( ) )
//            //{
//            //    axis.Group = _getDefaultGroupFunc( axis, num );
//            //}

//            //if( axis.Title.IsEmpty( ) )
//            //{
//            //    axis.Title = _defaultId + num;
//            //}

//            //axis.ChartArea = _chartArea;
//            //return base.OnAdding( axis );
//        }

//        protected override bool OnRemoving(ChartAxis axis)
//        {
//            bool hasAxis = Contains(axis);

//            if(hasAxis && _chartArea.Chart != null/*&& axis.IsDefault*/)
//            {
//                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
//            }

//            if((base.OnRemoving(axis) & hasAxis) == false)
//            {
//                return false;
//            }

//            axis.ChartArea = null;

//            return true;
//        }

//        protected override bool OnRemovingAt(int index)
//        {
//            ChartAxis chartAxis = this[index];

//            if(/*chartAxis.IsDefault &&*/_chartArea.Chart != null)
//            {
//                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
//            }

//            if(base.OnRemovingAt(index))
//            {
//                chartAxis.ChartArea = null;

//                return true;
//            }

//            return false;
//        }

//        protected override bool OnClearing()
//        {
//            if(_chartArea.Chart != null)
//            {
//                throw new InvalidOperationException(LocalizedStrings.ErrorRemovingDefaultAxis);
//            }

//            ChartAxis[ ] axises = ToArray();

//            if(base.OnClearing())
//            {
//                foreach(ChartAxis axis in axises)
//                {
//                    axis.ChartArea = null;
//                }

//                return true;
//            }

//            return false;
//        }
//    }

//    private class PropertiesNotifyList<T> : BaseList<T>, INotifyPropertyChanged, INotifyCollectionChanged
//    {
//        private int _index;
//        private T _property;

//        public event PropertyChangedEventHandler PropertyChangedEvent;

//        public event NotifyCollectionChangedEventHandler NotifyCollectionChangedEvent;

//        protected override bool OnRemove(T property)
//        {
//            _index = IndexOf(property);
//            return base.OnRemove(property);
//        }

//        protected override void OnRemoved(T property)
//        {
//            if(_index >= 0)
//            {
//                RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Remove, property, _index);
//            }
//            base.OnRemoved(property);
//        }

//        protected override void OnInserted(int index, T property)
//        {
//            RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Add, property, index);
//            base.OnInserted(index, property);
//        }


//        /* -------------------------------------------------------------------------------------------------------------------------------------------
//         *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
//         *  
//         *  Step A ----------> 2 Here we are subscribing to the Area Notifying PooledList OnAdded Event
//         * 
//         * ------------------------------------------------------------------------------------------------------------------------------------------- 
//         */
//        protected override void OnAdded(T property)
//        {
//            RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Add, property, Count - 1);
//            base.OnAdded(property);
//        }

//        protected override void OnRemoveAt(int index)
//        {
//            _property = this[index];
//            base.OnRemoveAt(index);
//        }

//        protected override void OnRemovedAt(int index)
//        {
//            if(_property != null)
//            {
//                RaiseCollectionChangedEvent(NotifyCollectionChangedAction.Remove, _property, index);
//            }
//            base.OnRemovedAt(index);
//        }

//        protected override void OnCleared()
//        {
//            RaiseCollectionChangedEvent();
//            base.OnCleared();
//        }

//        private void RaiseCollectionChangedEvent(
//            NotifyCollectionChangedAction notifyCollectionChangedAction_0,
//            T gparam_1,
//            int index)
//        {
//            RaisePropertyChangedEvent("Count");
//            RaisePropertyChangedEvent("Item[]");

//            NotifyCollectionChangedEvent?.Invoke(
//            this,
//            new NotifyCollectionChangedEventArgs(notifyCollectionChangedAction_0, gparam_1, index));
//        }

//        private void RaiseCollectionChangedEvent()
//        {
//            RaisePropertyChangedEvent("Count");
//            RaisePropertyChangedEvent("Item[]");

//            NotifyCollectionChangedEvent?.Invoke(
//            this,
//            new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//        }

//        private void RaisePropertyChangedEvent(string string_0)
//        {
//            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(string_0));
//        }

//        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
//        {
//            add
//            {
//                PropertyChangedEvent += value;
//            }

//            remove
//            {
//                PropertyChangedEvent -= value;
//            }
//        }

//        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
//        {
//            add
//            {
//                NotifyCollectionChangedEvent += value;
//            }
//            remove
//            {
//                NotifyCollectionChangedEvent -= value;
//            }
//        }
//    }

//    private sealed class ChartElementNotifyList : PropertiesNotifyList<IChartElement>
//    {
//        private readonly ChartArea _area;

//        public ChartElementNotifyList(ChartArea area)
//        {
//            ChartArea chartArea = area;
//            if(chartArea == null)
//            {
//                throw new ArgumentNullException("area");
//            }
//            _area = chartArea;
//        }

//        protected override bool OnAdding(IChartElement element)
//        {
//            var myChart = element.CheckOnNull(nameof(element)).ChartArea?.Chart;

//            if(myChart != null)
//            {
//                throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
//            }

//            if(Any(i => i.Id == element.Id))
//            {
//                throw new InvalidOperationException(LocalizedStrings.ElementAlreadyAttached);
//            }

//            var elementXY = element as IChartComponent;

//            Maybe.Do(
//                elementXY,
//                a =>
//                {
//                    var axis = _area.YAxises.FirstOrDefault(i => i.Id == a.YAxisId);
//                    if(!a.CheckAxesCompatible(new ChartAxisType?(_area.XAxisType), axis?.AxisType))
//                    {
//                        throw new InvalidOperationException(
//                            StringHelper.Put(
//                                LocalizedStrings.AxesTypesNotSupportedParams,
//                                new object[ ] { a.GetType().Name, _area.XAxisType, axis?.AxisType }));
//                    }
//                });

//            return base.OnAdding(element);
//        }
//    }
//}
