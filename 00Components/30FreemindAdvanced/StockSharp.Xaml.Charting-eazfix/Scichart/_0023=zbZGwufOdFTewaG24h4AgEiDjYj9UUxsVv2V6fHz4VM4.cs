using System;
using System.ComponentModel;
using System.Drawing;
using Ecng.Collections;
using Ecng.Serialization;
using StockSharp.Charting;


namespace StockSharp.Xaml.Charting;

/// <summary>
///     The interface is where the color to implement the logic to draw the indicators, active orders and stuff on the chart
/// </summary>
public interface IDrawableChartElement : IChartElement, IChartPart<IChartElement>, INotifyPropertyChanged, INotifyPropertyChanging, IPersistable, IChartComponent
{
    public Color Color
    {
        get;
    }

    //
    public UIBaseVM CreateViewModel( ScichartSurfaceMVVM _param1 );

    public bool StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> _param1 );

    void StartDrawing();
}


