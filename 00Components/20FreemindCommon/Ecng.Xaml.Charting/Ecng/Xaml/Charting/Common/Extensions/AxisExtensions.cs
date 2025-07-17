// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Extensions.AxisExtensions
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
namespace fx.Xaml.Charting
{
    internal static class AxisExtensions
    {
        internal static bool TrySetOrAnimateVisibleRange( this IAxis axis, IRange newRange, TimeSpan duration )
        {
            if ( axis.AutoRange == AutoRange.Always || !axis.IsValidRange( newRange ) || ( newRange == null || newRange.Equals( ( object ) axis.VisibleRange ) ) )
                return false;
            if ( duration.IsZero() )
                axis.VisibleRange = newRange;
            else
                axis.AnimateVisibleRangeTo( newRange, duration );
            return true;
        }

        internal static void SetVerticalOffset( this IAxis axis, FrameworkElement axisLabel, Point mousePoint )
        {
            axisLabel.SetValue( AxisCanvas.TopProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.CenterTopProperty, ( object ) double.NaN );
            if ( axis.IsHorizontalAxis )
            {
                DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Bottom ? AxisCanvas.TopProperty : AxisCanvas.BottomProperty;
                if ( axisLabel.ActualHeight >= axis.Height )
                    dp = dp == AxisCanvas.TopProperty ? AxisCanvas.BottomProperty : AxisCanvas.TopProperty;
                axisLabel.SetValue( dp, ( object ) 0.0 );
            }
            else
                AxisCanvas.SetCenterTop( ( UIElement ) axisLabel, mousePoint.Y );
        }

        internal static void SetHorizontalOffset( this IAxis axis, FrameworkElement axisLabel, Point mousePoint )
        {
            axisLabel.SetValue( AxisCanvas.LeftProperty, ( object ) double.NaN );
            axisLabel.SetValue( AxisCanvas.CenterLeftProperty, ( object ) double.NaN );
            if ( axis.IsHorizontalAxis )
            {
                AxisCanvas.SetCenterLeft( ( UIElement ) axisLabel, mousePoint.X );
            }
            else
            {
                DependencyProperty dp = axis.AxisAlignment == AxisAlignment.Right ? AxisCanvas.LeftProperty : AxisCanvas.RightProperty;
                if ( axisLabel.ActualWidth >= axis.ActualWidth )
                    dp = dp == AxisCanvas.LeftProperty ? AxisCanvas.RightProperty : AxisCanvas.LeftProperty;
                axisLabel.SetValue( dp, ( object ) 0.0 );
            }
        }
    }
}
