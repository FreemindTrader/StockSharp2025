using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

#nullable disable
internal interface IChartComponent : IChartElement, IChartPart<IChartElement>, INotifyPropertyChanging, INotifyPropertyChanged, IPersistable
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

    Func<IComparable, Color?> Colorer
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

    bool AdditionalName( string _param1 );
}
