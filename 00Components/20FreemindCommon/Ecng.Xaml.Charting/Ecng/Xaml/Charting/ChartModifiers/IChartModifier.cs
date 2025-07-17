// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.IChartModifier
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using Ecng.Xaml.Charting.Utility.Mouse;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public interface IChartModifier : IChartModifierBase, IReceiveMouseEvents, INotifyPropertyChanged
    {
        ISciChartSurface ParentSurface
        {
            get; set;
        }

        IAxis XAxis
        {
            get;
        }

        IEnumerable<IAxis> YAxes
        {
            get;
        }

        IAxis YAxis
        {
            get;
        }

        IAxis GetYAxis( string axisId );

        bool IsPointWithinBounds( Point mousePoint, IHitTestable hitTestable );

        void ResetInertia();

        void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args );

        void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args );

        void OnAnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args );
    }
}
