// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartGroup
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting
{
    internal class ChartGroup
    {
        public ChartGroup( ISciChartSurface ultraChartSurface )
        {
            Guard.NotNull( ( object ) ultraChartSurface, nameof( ultraChartSurface ) );
            this.UltrachartSurface = ultraChartSurface;
        }

        internal ISciChartSurface UltrachartSurface
        {
            get; private set;
        }

        public override bool Equals( object obj )
        {
            ChartGroup chartGroup = obj as ChartGroup;
            if ( chartGroup == null )
                return false;
            return chartGroup.UltrachartSurface == this.UltrachartSurface;
        }

        public override int GetHashCode()
        {
            return this.UltrachartSurface.GetHashCode();
        }

        internal void RestoreState()
        {
            StockSharp.Xaml.Charting.Visuals.UltrachartSurface ultrachartSurface = this.UltrachartSurface as StockSharp.Xaml.Charting.Visuals.UltrachartSurface;
            if ( ultrachartSurface == null )
                return;
            if ( ultrachartSurface.AxisAreaLeft != null )
                ultrachartSurface.AxisAreaLeft.Margin = new Thickness();
            if ( ultrachartSurface.AxisAreaRight == null )
                return;
            ultrachartSurface.AxisAreaRight.Margin = new Thickness();
        }
    }
}
