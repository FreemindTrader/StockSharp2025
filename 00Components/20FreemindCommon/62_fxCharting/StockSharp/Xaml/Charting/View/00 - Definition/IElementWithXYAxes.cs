using Ecng.Common;
using fx.Charting;
using System;
using System.ComponentModel;
using System.Windows.Media;

public interface IElementWithXYAxes : ICloneable< IfxChartElement >, INotifyPropertyChanged, ICloneable, INotifyPropertyChanging, IfxChartElement
{
    event Action< object, string, object > PropertyValueChanging;

    bool DontDraw
    {
        get;
        set;
    }

    ChartAxis XAxis
    {
        get;
    }

    ChartAxis YAxis
    {
        get;
    }

    new IfxChartElement ParentElement
    {
        get;
        set;
    }

    IElementWithXYAxes ElementWithXYAxes
    {
        get;
    }

    Func< IComparable, Color? > Colorer
    {
        get;
    }

    void AddAxisesAndEventHandler( ChartArea area );

    void RemoveAxisesEventHandler( );

    bool CheckAxesCompatible( ChartAxisType? xType, ChartAxisType? yType );

    void Clone( object obj );

    bool Draw( ChartDrawDataEx chartDrawData_0 );

    void Reset( );

    void ResetUI( );

    string GetGeneratedTitle( );

    string GetName( IfxChartElement element );

    bool AdditionalName( string string_0 );
}
