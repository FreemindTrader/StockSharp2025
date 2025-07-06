using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using System;
using System.ComponentModel;
using System.Windows.Media;

public interface IDrawableChartElement : IChartComponent,
  StockSharp.Xaml.Charting.IChartElement,
  IChartPart<StockSharp.Xaml.Charting.IChartElement>,
  INotifyPropertyChanging,
  INotifyPropertyChanged,
  IPersistable
{
    System.Windows.Media.Color Color { get; }

    UIChartBaseViewModel CreateViewModel( IScichartSurfaceVM viewModel );

    bool StartDrawing( IEnumerableEx<ChartDrawData.IDrawValue> data );

    //bool StartDrawing<T>( IEnumerableEx<ChartDrawData.IDrawValue< T > > data );

    void StartDrawing( );
}
