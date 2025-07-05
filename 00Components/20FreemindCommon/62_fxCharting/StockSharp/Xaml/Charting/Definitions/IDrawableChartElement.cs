using Ecng.Collections;
using Ecng.Common;
using StockSharp.Xaml;
using fx.Charting;
using System;
using System.ComponentModel;
using System.Windows.Media;

public interface IDrawableChartElement : ICloneable<IChartElement>, INotifyPropertyChanged, IChartComponent, ICloneable, INotifyPropertyChanging, IChartElement
{
    Color Color { get; }

    UIChartBaseViewModel CreateViewModel( IScichartSurfaceVM viewModel );

    bool StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> data );

    //bool StartDrawing<T>( IEnumerableEx<ChartDrawData.IDrawValue< T > > data );

    void StartDrawing( );
}
