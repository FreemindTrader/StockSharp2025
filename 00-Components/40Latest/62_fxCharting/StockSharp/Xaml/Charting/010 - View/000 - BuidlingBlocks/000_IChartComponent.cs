using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Xaml.Charting;
using StockSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting;

public interface IChartComponent : IChartElement, IChartPart<IChartElement>, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable
{
    IChartElement ParentElement
    {
        get;
    }

    IEnumerable<IChartElement> ChildElements
    {
        get;
    }

    int Priority
    {
        get;
    }

    event Action<object, string, object> PropertyValueChanging;

    bool DontDraw
    {
        get; set;
    }

    void SetParent( IChartElement _param1 );

    IChartComponent RootElement
    {
        get;
    }

    Func<IComparable, Color?> WinColorer
    {
        get;
    }

    void AddAxisesAndEventHandler( ChartArea _param1 );

    void RemoveAxisesEventHandler();

    bool CheckAxesCompatible( ChartAxisType? _param1, ChartAxisType? _param2 );

    void Clone( object _param1 );

    bool Draw( ChartDrawData _param1 );

    void Reset();

    void ResetUI();

    string GetGeneratedTitle();

    string GetName( IChartElement _param1 );

    bool HasExtraName( string _param1 );
}
