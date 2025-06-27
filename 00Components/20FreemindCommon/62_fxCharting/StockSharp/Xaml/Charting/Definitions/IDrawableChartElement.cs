﻿using Ecng.Collections;
using Ecng.Common;
using StockSharp.Xaml;
using fx.Charting;
using System;
using System.ComponentModel;
using System.Windows.Media;

public interface IDrawableChartElement : ICloneable<IfxChartElement>, INotifyPropertyChanged, IElementWithXYAxes, ICloneable, INotifyPropertyChanging, IfxChartElement
{
    Color Color { get; }

    UIBaseVM CreateViewModel( IScichartSurfaceVM viewModel );

    bool StartDrawing( IEnumerableEx<ChartDrawDataEx.IDrawValue> data );

    //bool StartDrawing<T>( IEnumerableEx<ChartDrawDataEx.IDrawValue< T > > data );

    void StartDrawing( );
}
