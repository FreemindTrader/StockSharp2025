// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ChartGroup
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
namespace fx.Xaml.Charting
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
            fx.Xaml.Charting.UltrachartSurface ultrachartSurface = this.UltrachartSurface as fx.Xaml.Charting.UltrachartSurface;
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
